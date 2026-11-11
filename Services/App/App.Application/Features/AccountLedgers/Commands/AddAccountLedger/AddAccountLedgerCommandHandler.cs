
using App.Application.Features.AccountLedgers.Commands.AddAccountLedger;
using App.Domain.Entities.AccountLedger;
using App.Domain.Entities.Addresses;
using App.Infrastructure.Helpers;
using App.Infrastructure.Messages;
using App.Infrastructure.Services.AccountLedgers;
using App.Infrastructure.Services.Addresses;
using Lib.Core.Models.Response;
using MediatR;

namespace App.Application.Features.AccountLedgers.Commands.AddAccountLedgers;

public class AddAccountLedgerCommandHandler : IRequestHandler<AddAccountLedgerCommand, ResponseModel>
{
    private readonly IAccountLedgerService _accountLedgerService;
    private readonly IAddressService _addressService;

    public AddAccountLedgerCommandHandler(
        IAccountLedgerService accountLedgerService,
        IAddressService addressService)
    {
        _accountLedgerService = accountLedgerService
            ?? throw new ArgumentNullException(nameof(accountLedgerService));

        _addressService = addressService
            ?? throw new ArgumentNullException(nameof(addressService));
    }

    public async Task<ResponseModel> Handle(AddAccountLedgerCommand request, CancellationToken cancellationToken)
    {
        ResponseModel response = new();

        try
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            // Step 1: Create AccountLedger entity
            AccountLedger accountLedger = new();

            request.CopyPropertiesTo(accountLedger);

            // Step 2: Insert AccountLedger
            var insertAccountLedger =
                await _accountLedgerService.Insert(accountLedger);

            if (insertAccountLedger != null)
            {
                // Step 3: Insert Address records
                if (request.Addresses != null && request.Addresses.Any())
                {
                    foreach (var item in request.Addresses)
                    {
                        Address address = new()
                        {
                            // AccountLedger generated ID
                            AccountGroupId = insertAccountLedger.Id,

                            Line1 = item.Line1,
                            Line2 = item.Line2,
                            Country = item.Country,
                            State = item.State,
                            City = item.City,
                            ZipCode = item.ZipCode
                        };

                        // Step 4: Save Address
                        await _addressService.Insert(address);
                    }
                }

                // Step 5: Success response
                response.setSuccessResponse(
                    null!,
                    "Account Ledger " + ResponseMessages.InsertSuccess);
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new Exception(
                "Error while inserting AccountLedger and Address: " + ex.Message,
                ex);
        }
    }
}
