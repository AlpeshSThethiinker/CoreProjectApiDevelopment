
using App.Infrastructure.Helpers;
using App.Infrastructure.Messages;
using Lib.Core.Models.Response;
using MediatR;
using App.Infrastructure.Services.AccountLedgers;
using App.Application.Features.AccountLedgers.Commands.AddAccountLedger;
using App.Domain.Entities.AccountLedger;



namespace App.Application.Features.AccountLedgers.Commands.AddAccountLedgers;
public class AddAccountLedgerCommandHandler : IRequestHandler<AddAccountLedgerCommand, ResponseModel>
{
    private readonly IAccountLedgerService _accountLedgerService;

    public AddAccountLedgerCommandHandler(IAccountLedgerService accountLedgerService)
    {
        _accountLedgerService = accountLedgerService ?? throw new ArgumentNullException(nameof(accountLedgerService));
    }

    public async Task<ResponseModel> Handle(AddAccountLedgerCommand request, CancellationToken cancellationToken)
    {
        ResponseModel response = new();
        try
        {
            if(request is not null)
            {
                AccountLedger accountLedger = new();
                request.CopyPropertiesTo(accountLedger);
                var insertAccountLedger = await _accountLedgerService.Insert(accountLedger);
                if (insertAccountLedger != null)
                {
                    response.setSuccessResponse(null!, "Account Ledger " + ResponseMessages.InsertSuccess);
                }
            }
            return response;
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);

        }
    }

}

