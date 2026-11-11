
using App.Infrastructure.Helpers;
using App.Infrastructure.Messages;
using App.Infrastructure.Services.AccountLedgers;
using App.Infrastructure.Services.Addresses;
using App.Domain.ViewModels.Addresses;
using Lib.Core.Models.Response;
using MediatR;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.VisualBasic;

namespace App.Application.Features.AccountLedgers.Queries.GetAccountLedgerById;

public class GetAccountLedgerByIdQueryHandler : IRequestHandler<GetAccountLedgerByIdQuery, ResponseModel>
{
    private readonly IAccountLedgerService _accountLedgerService;
    private readonly IAddressService _addressService;

    public GetAccountLedgerByIdQueryHandler(IAccountLedgerService accountLedgerService, IAddressService addressService)
    {
        _accountLedgerService = accountLedgerService ?? throw new ArgumentNullException(nameof(accountLedgerService));
        _addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
    }

    public async Task<ResponseModel> Handle(GetAccountLedgerByIdQuery request,CancellationToken cancellationToken)
    {
        ResponseModel response = new();

        try
        {
            var getAccountLedger = ( await _accountLedgerService.GetAsync(x => x.Id == request.AccountGroupId && !x.Deleted)).FirstOrDefault();

            if (getAccountLedger == null)
            {
                //response.StatusCode = 404;
                response.StatusCode = (int)HttpStatusCode.NoContent;
                response.Errors.Add("Account Ledger not found.");
                return response;
            }

            GetAccountLedgerByIdQueryResponse queryResponse = new();

            getAccountLedger.CopyPropertiesTo(queryResponse);

            // fetch addresses associated with the account ledger
            var addresses = await _addressService.GetAsync(x => x.AccountGroupId == getAccountLedger.Id && !x.Deleted);

            // map the addresses to the response model
            queryResponse.Addresses = addresses.Select(x => new AddressVM
            {
                Id=x.Id,
                AccountGroupId=x.AccountGroupId,
                Line1=x.Line1,
                Line2=x.Line2,
                Country = x.Country,
                State = x.State,
                City=x.City,
                ZipCode = x.ZipCode,
            }).ToList();
            // Map database Id to AccountGroupId
            //queryResponse.AccountGroupId = getAccountLedger.Id;
            response.setSuccessResponse(queryResponse,"Account Ledger Found.");
            return response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}

