using App.Application.Features.AccountLedgers.Queries.GetAllAccountLedger;
using App.Infrastructure.Messages;
using Lib.Core.Models.Response;
using App.Infrastructure.Helpers;
using MediatR;
using App.Infrastructure.Services.AccountLedgers;

namespace App.Application.Features.AccountLedgers.Queries.GetAccountLedgerById;

public class  GetAccountLedgerByIdQueryHandler : IRequestHandler<GetAccountLedgerByIdQuery, ResponseModel>
{
    private readonly IAccountLedgerService _accountLedgerService;
    public GetAccountLedgerByIdQueryHandler(IAccountLedgerService accountLedgerService)
    {
        _accountLedgerService = accountLedgerService ?? throw new ArgumentNullException(nameof(accountLedgerService));
    }
    public async Task<ResponseModel> Handle(GetAccountLedgerByIdQuery request, CancellationToken cancellationToken)
    {
        ResponseModel response = new();
        try
        {
            GetAllAccountLedgerQueryResponse queryResponse = new();
            var getAccountLedger = (await _accountLedgerService.GetAsync(x => x.Id == request.AccountGroupId && !x.Deleted)).FirstOrDefault();
            if(getAccountLedger != null)
            {
                getAccountLedger.CopyPropertiesTo(queryResponse);
            }
            response.setSuccessResponse(queryResponse, "Account Ledger ");
            return response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
