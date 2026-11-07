
using App.Infrastructure.Messages;
using App.Infrastructure.Services.AccountLedgers;
using Lib.Core.Models.Response;
using MediatR;
using App.Domain.Entities.AccountLedger;

namespace App.Application.Features.AccountLedgers.Commands.DeleteAccountLedger;

public class DeleteAccountLedgerCommandHandler : IRequestHandler<DeleteAccountLedgerCommand, ResponseModel>
{
    private readonly IAccountLedgerService _AccountLedgerService;

    public DeleteAccountLedgerCommandHandler(IAccountLedgerService AccountLedgerService)
    {
        _AccountLedgerService = AccountLedgerService ?? throw new ArgumentNullException(nameof(AccountLedgerService));
    }

    public async Task<ResponseModel> Handle(DeleteAccountLedgerCommand request, CancellationToken cancellationToken)
    {
        ResponseModel response = new();
        try
        {
            AccountLedger? getAccountLedgerData = (await _AccountLedgerService.GetAsync(x => x.Id == request.Id && !x.Deleted)).FirstOrDefault();
            if (getAccountLedgerData is null)
            {
                response.Errors.Add(ResponseMessages.NotFound);
                response.StatusCode = (int)HttpStatusCode.NoContent;
                return response;
            }
            var deleteAccountLedger = await _AccountLedgerService.Delete(request.Id);
            if (deleteAccountLedger is not null)
            {
                response.setSuccessResponse(null!, "Account Ledger " + ResponseMessages.DeleteSuccess);
            }
            return response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
