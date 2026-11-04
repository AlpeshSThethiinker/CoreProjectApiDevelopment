using App.Domain.Entities.Masters.MultiLevelList.Commission;
using App.Domain.ViewModels.Commission;
using App.Infrastructure.Services.Masters.MultiLevelList.Commission;
using Lib.Core.Models;
using MediatR;

namespace App.Application.Features.Masters.MultiLevelList.Commission.Commands.UpdateCommission;

public class UpdateCommissionCommandHandler : IRequestHandler<UpdateCommissionCommand, ResponseModel>
{

    #region fields
    private readonly ICommissionService _commissionService;
    private readonly ICommissionMappingService _commissionMappingService;
    #endregion

    #region ctor
    public UpdateCommissionCommandHandler(ICommissionService commissionService, ICommissionMappingService commissionMappingService)
    {
        _commissionService = commissionService ?? throw new ArgumentNullException(nameof(commissionService));
        _commissionMappingService = commissionMappingService ?? throw new ArgumentException(nameof(commissionMappingService));
    }
    #endregion

    #region update Commission Method
    public async Task<ResponseModel> Handle(UpdateCommissionCommand request, CancellationToken cancellationToken)
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
                    var updateCommissionData = await _commissionService.UpdateCommissionData(new()
                    {
                        Id = request.MasterCommission.Id,
                        CommissionRole = request.MasterCommission.CommissionRole,
                        CommissionType = request.MasterCommission.CommissionType
                    });

                    if (request.MasterCommission.CommissionMapping is not null)
                    {
                        foreach (var item in request.MasterCommission.CommissionMapping)
                        {
                            if (!string.IsNullOrEmpty(item.Id))
                            {
                                var getMappingDataById = (await _commissionMappingService.GetAsync(x => x.Id == item.Id && x.MappingId == item.MappingId && !x.IsDeleted)).FirstOrDefault();
                                if (getMappingDataById != null)
                                {
                                    getMappingDataById.FromRange = item.FromRange;
                                    getMappingDataById.ToRange = item.ToRange;
                                    getMappingDataById.Rate = item.Rate;
                                    await _commissionMappingService.UpdateCommissionMappingData(getMappingDataById);
                                }
                            }
                            else
                            {
                                CommissionMapping commissionMapping = await _commissionMappingService.InsertCommissionMappingData(new()
                                {
                                    MappingId = item.MappingId,
                                    FromRange = item.FromRange,
                                    ToRange = item.ToRange,
                                    Rate = item.Rate,
                                    ActiveBtn = item.ActiveBtn
                                });
                            }
                        }
                        responseModel.IsSuccess = true;
                        responseModel.Message = "Data updated";
                        return responseModel;
                    }
                }
            }
            return responseModel;
        }
        catch (Exception)
        {
            responseModel.IsSuccess = false;
            responseModel.Message = "Updation Failed";
        }
        return responseModel;
    }

    #endregion

}
