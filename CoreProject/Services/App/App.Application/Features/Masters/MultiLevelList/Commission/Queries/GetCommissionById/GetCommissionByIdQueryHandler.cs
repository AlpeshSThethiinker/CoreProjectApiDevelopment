using App.Infrastructure.Services.Masters.MultiLevelList.Commission;
using Lib.Core.Models;
using MediatR;

namespace App.Application.Features.Masters.MultiLevelList.Commission.Queries.GetCommissionById;

public class GetCommissionByIdQueryHandler : IRequestHandler<GetCommissionByIdQuery, ResponseModel>
{
    #region fields
    private readonly ICommissionService _commissionService;
    private readonly ICommissionMappingService _commissionMappingService;
    #endregion

    #region ctor
    public GetCommissionByIdQueryHandler(ICommissionService commissionService, ICommissionMappingService commissionMappingService)
    {
        _commissionService = commissionService ?? throw new ArgumentNullException(nameof(commissionService));
        _commissionMappingService = commissionMappingService ?? throw new ArgumentException(nameof(commissionMappingService));
    }
    #endregion

    #region Get Commission By Id 
    public async Task<ResponseModel> Handle(GetCommissionByIdQuery request, CancellationToken cancellationToken)
    {
        ResponseModel responseModel = new();
        try
        {
            var getCommissionInfo = await _commissionService.GetCommissionById(request.MappingId!);
            if (getCommissionInfo != null)
            {
                List<GetCommissionByIdQueryResponse> queryResponse = new();
                if (!getCommissionInfo.IsDeleted)
                {
                    GetCommissionByIdQueryResponse assignedCommissionData = new();
                    assignedCommissionData.MasterCommission.Id = getCommissionInfo.Id;
                    assignedCommissionData.MasterCommission.CommissionRole = getCommissionInfo.CommissionRole;
                    assignedCommissionData.MasterCommission.CommissionType = getCommissionInfo.CommissionType;
                    assignedCommissionData.MasterCommission.CommissionMapping = await _commissionMappingService.GetCommissionMappingById(getCommissionInfo.Id);
                    queryResponse.Add(assignedCommissionData);
                    responseModel.IsSuccess = true;
                    responseModel.Message = "Commission Data Found";
                    responseModel.Response = queryResponse;
                }
                else
                {
                    responseModel.IsSuccess = false;
                    responseModel.Message = "Commission Already Deleted";
                }
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
