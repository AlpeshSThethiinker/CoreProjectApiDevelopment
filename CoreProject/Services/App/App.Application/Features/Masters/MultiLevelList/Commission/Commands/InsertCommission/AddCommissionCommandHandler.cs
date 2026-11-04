using App.Domain.ViewModels.Commission;
using App.Infrastructure.Services.Masters.MultiLevelList.Commission;
using Lib.Core.Models;
using MediatR;

namespace App.Application.Features.Masters.MultiLevelList.Commission.Commands.InsertCommission;

public class AddCommissionCommandHandler : IRequestHandler<AddCommissionCommand, ResponseModel>
{
    #region fields
    private readonly ICommissionService _commissionService;
    private readonly ICommissionMappingService _commissionMappingService;
    #endregion

    #region ctor
    public AddCommissionCommandHandler(ICommissionService commissionService, ICommissionMappingService commissionMappingService)
    {
        _commissionService = commissionService ?? throw new ArgumentNullException(nameof(commissionService));
        _commissionMappingService = commissionMappingService ?? throw new ArgumentException(nameof(commissionMappingService));

    }
    #endregion

    #region add Comission Data
    public async Task<ResponseModel> Handle(AddCommissionCommand request, CancellationToken cancellationToken)
    {
        List<RangeModel> _range = new();
        RangeModel newRange = new();
        ResponseModel responseModel = new();
        try
        {
            int result = 0;
            if (!string.IsNullOrEmpty(request.MasterCommission.Id))
            {
                if (request.MasterCommission.CommissionMapping is not null || request.MasterCommission.CommissionMapping.Count > 0)
                {
                    foreach (var item in request.MasterCommission.CommissionMapping)
                    {
                        if (_range.Any(x => (newRange.From <= item.FromRange && newRange.From >= item.ToRange) ||
                                            (newRange.To >= item.FromRange && newRange.To >= item.ToRange) ||
                                            (newRange.From >= item.FromRange && newRange.To <= item.ToRange)))
                        {
                            responseModel.IsSuccess = false;
                            responseModel.Message = "Cannot enter same or inbetween range twice";
                            result = 0;
                        }
                        else
                        {
                            newRange.From = item.FromRange;
                            newRange.To = item.ToRange;
                            _range.Add(newRange);
                            result = 1;
                        }
                    }
                }

                if (result == 1)
                {
                    var insertResponse = await _commissionService.InsertCommission(new()
                    {
                        CommissionRole = request.MasterCommission.CommissionRole,
                        CommissionType = request.MasterCommission.CommissionType,
                        ActiveBtn = request.MasterCommission.ActiveBtn
                    });
                    if (insertResponse is not null)
                    {
                        if (request.MasterCommission.CommissionMapping is not null && request.MasterCommission.CommissionMapping.Count > 0)
                        {
                            foreach (var item in request.MasterCommission.CommissionMapping)
                            {
                                var insertCommissionMapping = await _commissionMappingService.InsertCommissionMapping(new()
                                {
                                    MappingId = insertResponse.Id,
                                    FromRange = item.FromRange,
                                    Rate = item.Rate,
                                    ToRange = item.ToRange,
                                    ActiveBtn = item.ActiveBtn
                                });

                                responseModel.IsSuccess = true;
                                responseModel.Message = "Commission Details Inserted Successfully";
                            }
                        }
                    }
                }
            }
            else
            {
                responseModel.IsSuccess = false;
                responseModel.Message = "Commission Details Insertion Failed.";
            }
            return responseModel;
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    #endregion

}


