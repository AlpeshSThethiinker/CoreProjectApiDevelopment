using App.Infrastructure.Services.Masters.MultiLevelList.Commission;
using Lib.Core.Models;
using MediatR;

namespace App.Application.Features.Masters.MultiLevelList.Commission.Queries.GetAllCommission;

public class GetAllCommissionQueryHandler : IRequestHandler<GetAllCommissionQuery, ResponseModel>
{
    #region fields
    private readonly ICommissionService _commissionService;
    private readonly ICommissionMappingService _commissionMappingService;
    #endregion

    #region ctor
    public GetAllCommissionQueryHandler(ICommissionService commissionService, ICommissionMappingService commissionMappingService)
    {
        _commissionService = commissionService ?? throw new ArgumentNullException(nameof(commissionService));
        _commissionMappingService = commissionMappingService ?? throw new ArgumentException(nameof(commissionMappingService));
    }
    #endregion


    #region Get all commission Method
    public async Task<ResponseModel> Handle(GetAllCommissionQuery request, CancellationToken cancellationToken)
    {
        ResponseModel responseModel = new();
        try
        {
            var getCommissionInfo = await _commissionService.GetAllCommission();
            if (getCommissionInfo != null && getCommissionInfo.Count > 0)
            {
                List<GetAllCommissionQueryResponse> queryResponse = new();
                foreach (var item in getCommissionInfo)
                {
                    if (!item.IsDeleted)
                    {
                        GetAllCommissionQueryResponse assignedCommissionData = new();
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
