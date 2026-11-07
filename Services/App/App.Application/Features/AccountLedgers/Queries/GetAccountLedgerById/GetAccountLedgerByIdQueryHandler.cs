
using App.Infrastructure.Helpers;
using App.Infrastructure.Messages;
using App.Infrastructure.Services.AccountLedgers;
using Lib.Core.Models.Response;
using MediatR;

namespace App.Application.Features.AccountLedgers.Queries.GetAccountLedgerById;

public class GetAccountLedgerByIdQueryHandler
    : IRequestHandler<GetAccountLedgerByIdQuery, ResponseModel>
{
    private readonly IAccountLedgerService _accountLedgerService;

    public GetAccountLedgerByIdQueryHandler(
        IAccountLedgerService accountLedgerService)
    {
        _accountLedgerService = accountLedgerService
            ?? throw new ArgumentNullException(nameof(accountLedgerService));
    }

    public async Task<ResponseModel> Handle(
        GetAccountLedgerByIdQuery request,
        CancellationToken cancellationToken)
    {
        ResponseModel response = new();

        try
        {
            var getAccountLedger = (
                await _accountLedgerService.GetAsync(
                    x => x.Id == request.AccountGroupId && !x.Deleted
                )
            ).FirstOrDefault();

            if (getAccountLedger == null)
            {
                response.StatusCode = 404;
                response.Errors.Add("Account Ledger not found.");
                return response;
            }

            GetAccountLedgerByIdQueryResponse queryResponse = new();

            getAccountLedger.CopyPropertiesTo(queryResponse);

            // Map database Id to AccountGroupId
            queryResponse.AccountGroupId = getAccountLedger.Id;

            response.setSuccessResponse(
                queryResponse,
                "Account Ledger Found."
            );

            return response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}

