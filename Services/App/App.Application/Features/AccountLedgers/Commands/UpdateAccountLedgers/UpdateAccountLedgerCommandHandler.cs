using App.Infrastructure.Helpers;
using App.Infrastructure.Messages;
using App.Infrastructure.Services.AccountLedgers;

using Lib.Core.Models.Response;
using MediatR;

namespace App.Application.Features.AccountLedgers.Commands.UpdateAccountLedger;

public class UpdateAccountLedgerCommandHandler : IRequestHandler<UpdateAccountLedgerCommand, ResponseModel>
{
    private readonly IAccountLedgerService _accountLedgerService;

    public UpdateAccountLedgerCommandHandler(IAccountLedgerService accountLedgerService)
    {
        _accountLedgerService = accountLedgerService ?? throw new ArgumentNullException(nameof(accountLedgerService));
    }

    public async Task<ResponseModel> Handle(UpdateAccountLedgerCommand request, CancellationToken cancellationToken)
    {
        ResponseModel response = new();
        try
        {
            if (request is not null && request.AccountGroupId != 0)
            {
                var getAccountLedgerData = (await _accountLedgerService.GetAsync(x => x.Id == request.AccountGroupId && !x.Deleted)).FirstOrDefault();
                if (getAccountLedgerData != null)
                {
                    request.CopyPropertiesTo(getAccountLedgerData);
                    var updateAccountLedger = await _accountLedgerService.Update(getAccountLedgerData);
                    if (updateAccountLedger != null)
                    {
                        response.setSuccessResponse(null!, "AccountLedger updated successfully" + ResponseMessages.UpdateSuccess);

                    }
                }
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Errors.Add("Invalid AccountGroupId provided.");
                return response;
            }
            return response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
