using App.Infrastructure.Services.Masters.MultiLevelList.Commission;
using Lib.Core.Models;
using MediatR;

namespace App.Application.Features.Masters.MultiLevelList.Commission.Queries.GetByCommissionType;

public class GetByCommissionTypeQueryHandler : IRequestHandler<GetByCommissionTypeQuery, ResponseModel>
{
    #region fields
    private readonly ICommissionService _commissionService;
    private readonly ICommissionMappingService _commissionMappingService;
    #endregion

    #region ctor
    public GetByCommissionTypeQueryHandler(ICommissionService commissionService, ICommissionMappingService commissionMappingService)
    {
        _commissionService = commissionService ?? throw new ArgumentNullException(nameof(commissionService));
        _commissionMappingService = commissionMappingService ?? throw new ArgumentException(nameof(commissionMappingService));
    }
    #endregion

    #region Get By Commission Type
    public async Task<ResponseModel> Handle(GetByCommissionTypeQuery request, CancellationToken cancellationToken)
    {
        ResponseModel responseModel = new();
        try
        {
            var getCommissionInfo = await _commissionService.GetAsync(x => x.CommissionType == request.CommissionType && !x.IsDeleted);
            if (getCommissionInfo != null)
            {
                List<GetByCommissionTypeQueryResponse> queryResponse = new();
                foreach (var item in getCommissionInfo)
                {
                    if (!item.IsDeleted)
                    {
                        GetByCommissionTypeQueryResponse assignedCommissionData = new();
                        assignedCommissionData.MasterCommission.Id = item.Id;
                        assignedCommissionData.MasterCommission.CommissionRole = item.CommissionRole;
                        assignedCommissionData.MasterCommission.CommissionType = item.CommissionType;
                        assignedCommissionData.MasterCommission.CommissionMapping = await _commissionMappingService.GetCommissionMappingById(item.Id);
                        queryResponse.Add(assignedCommissionData);

                    }
                }
                responseModel.IsSuccess = true;
                responseModel.Message = "Commission Data Found";
                responseModel.Response = queryResponse;
            }
            else
            {
                responseModel.IsSuccess = false;
                responseModel.Message = "No Commissions Found";
            }
        }
        catch (Exception)
        {

            responseModel.IsSuccess = false;
            responseModel.Message = "No Commission Details Found";
        }
        return responseModel;
    }
    #endregion

}
