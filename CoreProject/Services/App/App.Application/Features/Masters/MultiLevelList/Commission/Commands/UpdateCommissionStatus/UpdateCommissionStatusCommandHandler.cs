using App.Infrastructure.Services.Masters.MultiLevelList.Commission;
using Lib.Core.Models;
using MediatR;

namespace App.Application.Features.Masters.MultiLevelList.Commission.Commands.UpdateCommissionStatus;

public class UpdateCommissionStatusCommandHandler : IRequestHandler<UpdateCommissionStatusCommand, ResponseModel>
{
    #region fields
    private readonly ICommissionService _commissionService;
    private readonly ICommissionMappingService _commissionMappingService;
    #endregion

    #region ctor
    public UpdateCommissionStatusCommandHandler(ICommissionService commissionService, ICommissionMappingService commissionMappingService)
    {
        _commissionService = commissionService ?? throw new ArgumentNullException(nameof(commissionService));
        _commissionMappingService = commissionMappingService ?? throw new ArgumentException(nameof(commissionMappingService));
    }
    #endregion

    #region Update Commission status
    public async Task<ResponseModel> Handle(UpdateCommissionStatusCommand request, CancellationToken cancellationToken)
    {
        ResponseModel responseModel = new();
        try
        {
            if (request.UpdateStatusTypeId == 1)
            {
                var response = await _commissionService.UpdateUserCommissionStatus(new()
                {
                    Id = request.Id,
                    IsActive = request.Active
                });
                responseModel.IsSuccess = true;
                responseModel.Message = "Status Type updated";
            }
            else if (request.UpdateStatusTypeId == 2)
            {
                var response = await _commissionMappingService.UpdateUserCommissionMappingStatus(new()
                {
                    Id = request.Id,
                    IsActive = request.Active
                });
                responseModel.IsSuccess = true;
                responseModel.Message = "Status Type updated";
            }
            else
            {
                responseModel.IsSuccess = false;
                responseModel.Message = "Invalid UpdateStatusTypeId";
            }
        }
        catch (Exception)
        {
            responseModel.IsSuccess = false;
            responseModel.Message = "Status Updation Failed";
        }
        return responseModel;
    }
    #endregion

}