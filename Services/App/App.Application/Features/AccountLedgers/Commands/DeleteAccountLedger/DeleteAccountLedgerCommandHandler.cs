
using App.Infrastructure.Messages;
using App.Infrastructure.Services.AccountLedgers;
using App.Infrastructure.Services.Addresses;
using Lib.Core.Models.Response;
using MediatR;

namespace App.Application.Features.AccountLedgers.Commands.DeleteAccountLedger;

public class DeleteAccountLedgerCommandHandler
    : IRequestHandler<DeleteAccountLedgerCommand, ResponseModel>
{
    private readonly IAccountLedgerService _accountLedgerService;
    private readonly IAddressService _addressService;

    public DeleteAccountLedgerCommandHandler(
        IAccountLedgerService accountLedgerService,
        IAddressService addressService)
    {
        _accountLedgerService = accountLedgerService
            ?? throw new ArgumentNullException(nameof(accountLedgerService));

        _addressService = addressService
            ?? throw new ArgumentNullException(nameof(addressService));
    }

    public async Task<ResponseModel> Handle(
        DeleteAccountLedgerCommand request,
        CancellationToken cancellationToken)
    {
        ResponseModel response = new();

        try
        {
            // Step 1: Validate AccountLedger ID
            if (request == null || request.Id <= 0)
            {
                response.StatusCode =
                    (int)System.Net.HttpStatusCode.BadRequest;

                response.Errors.Add("Invalid AccountLedger Id.");
                return response;
            }

            // Step 2: Find AccountLedger
            var accountLedger = (await _accountLedgerService.GetAsync(
                x => x.Id == request.Id && !x.Deleted
            )).FirstOrDefault();

            if (accountLedger == null)
            {
                response.StatusCode =
                    (int)System.Net.HttpStatusCode.NotFound;

                response.Errors.Add("AccountLedger record not found.");
                return response;
            }

            // Step 3: Find all linked Addresses
            var addresses = await _addressService.GetAsync(
                x => x.AccountGroupId == accountLedger.Id && !x.Deleted
            );

            // Step 4: Soft delete all linked Addresses
            foreach (var address in addresses)
            {
                await _addressService.Delete(address.Id);
            }

            // Step 5: Soft delete AccountLedger
            await _accountLedgerService.Delete(accountLedger.Id);

            // Step 6: Success response
            response.setSuccessResponse(
                null!,
                "AccountLedger and linked Addresses deleted successfully."
            );

            return response;
        }
        catch (Exception ex)
        {
            response.StatusCode =
                (int)System.Net.HttpStatusCode.InternalServerError;

            response.Errors.Add(ex.Message);
            return response;
        }
    }
}
