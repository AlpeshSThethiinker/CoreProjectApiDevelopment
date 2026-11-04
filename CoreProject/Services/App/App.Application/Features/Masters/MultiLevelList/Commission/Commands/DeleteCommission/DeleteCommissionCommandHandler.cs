using App.Application.Features.Company.Commands.DeleteCompany;
using App.Infrastructure.Messages;
using App.Infrastructure.Services.Masters.MultiLevelList.Commission;
using Lib.Core.Models;
using MediatR;

namespace App.Application.Features.Masters.MultiLevelList.Commission.Commands.DeleteCommission;

public class DeleteCommissionCommandHandler : IRequestHandler<DeleteCommissionCommand, ResponseModel>
{

    #region fields
    private readonly ICommissionService _commissionService;
    private readonly ICommissionMappingService _commissionMappingService;
    #endregion

    #region ctor
    public DeleteCommissionCommandHandler(ICommissionService commissionService, ICommissionMappingService commissionMappingService)
    {
        _commissionService = commissionService ?? throw new ArgumentNullException(nameof(commissionService));
        _commissionMappingService = commissionMappingService ?? throw new ArgumentException(nameof(commissionMappingService));
    }
    #endregion

    #region delete Commission Method
    public async Task<ResponseModel> Handle(DeleteCommissionCommand request, CancellationToken cancellationToken)
    {
        ResponseModel responseModel = new();

        try
        {
            if (request.Id.Count != 0)
            {
                List<string> Ids = [];
                foreach (var id in request.Id)
                {
                    var deleteResponse = await _commissionService.DeleteCommissionById(new()
                    {
                        Id = id
                    });
                    if (deleteResponse != null)
                    {
                        string deleteId = deleteResponse.Id;
                        Ids.Add(deleteId);
                    }
                }
                DeleteCompanyCommandResponse commandResponse = new()
                {
                    Id = Ids
                };
                responseModel.IsSuccess = true;
                responseModel.Message = "Commission Deleted successfully";

            }
            else
            {
                responseModel.IsSuccess = false;
                responseModel.Message = "Commission Not deleted";
            }

            return responseModel;
        }
        catch (Exception ex)
        {
            responseModel.Message = ResponseMessages.ExceptionOccured + ex.Message;
            return responseModel;
        }
    }
    #endregion
}
