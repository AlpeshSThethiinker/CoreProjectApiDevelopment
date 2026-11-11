
using App.Infrastructure.Messages;
using App.Infrastructure.Services.AccountLedgers;
using App.Infrastructure.Services.Addresses;
using App.Domain.Entities.Addresses;
using Lib.Core.Models.Response;
using MediatR;

namespace App.Application.Features.AccountLedgers.Commands.UpdateAccountLedger;

public class UpdateAccountLedgerCommandHandler
    : IRequestHandler<UpdateAccountLedgerCommand, ResponseModel>
{
    private readonly IAccountLedgerService _accountLedgerService;
    private readonly IAddressService _addressService;

    public UpdateAccountLedgerCommandHandler(
        IAccountLedgerService accountLedgerService,
        IAddressService addressService)
    {
        _accountLedgerService = accountLedgerService;
        _addressService = addressService;
    }

    public async Task<ResponseModel> Handle(
        UpdateAccountLedgerCommand request,
        CancellationToken cancellationToken)
    {
        ResponseModel response = new();

        try
        {
            // Step 1: Validate AccountGroupId
            if (request == null ||
                !request.AccountGroupId.HasValue ||
                request.AccountGroupId.Value <= 0)
            {
                response.StatusCode =
                    (int)System.Net.HttpStatusCode.BadRequest;

                response.Errors.Add("Invalid AccountGroupId provided.");
                return response;
            }

            // Step 2: Find AccountLedger
            var accountLedger = (await _accountLedgerService.GetAsync(
                x => x.Id == request.AccountGroupId.Value && !x.Deleted
            )).FirstOrDefault();

            if (accountLedger == null)
            {
                response.StatusCode =
                    (int)System.Net.HttpStatusCode.NotFound;

                response.Errors.Add("AccountLedger record not found.");
                return response;
            }

            // Step 3: Validate all existing Addresses first
            var addresses = request.Addresses ?? new();

            foreach (var address in addresses)
            {
                if (address.Id.HasValue && address.Id.Value > 0)
                {
                    var existingAddress = (await _addressService.GetAsync(
                        x => x.Id == address.Id.Value && !x.Deleted
                    )).FirstOrDefault();

                    if (existingAddress == null)
                    {
                        response.StatusCode =
                            (int)System.Net.HttpStatusCode.NotFound;

                        response.Errors.Add(
                            $"Address ID {address.Id} not found.");

                        return response;
                    }

                    if (existingAddress.AccountGroupId != accountLedger.Id)
                    {
                        response.StatusCode =
                            (int)System.Net.HttpStatusCode.BadRequest;

                        response.Errors.Add(
                            $"Address ID {address.Id} does not belong to this AccountLedger.");

                        return response;
                    }
                }
            }

            // Step 4: Update AccountLedger properties
            accountLedger.AccountNo = request.AccountNo;
            accountLedger.LedgerTypeId = request.LedgerTypeId;
            accountLedger.LedgerName = request.LedgerName;
            accountLedger.FirstName = request.FirstName;
            accountLedger.LastName = request.LastName;
            accountLedger.DateOfBirth = request.DateOfBirth;
            accountLedger.Mobile = request.Mobile;
            accountLedger.MobileNoDialCode = request.MobileNoDialCode;
            accountLedger.Email = request.Email;

            await _accountLedgerService.Update(accountLedger);

            // Step 5: Process Addresses
            foreach (var address in addresses)
            {
                if (address.Id.HasValue && address.Id.Value > 0)
                {
                    // Update existing Address
                    var existingAddress = (await _addressService.GetAsync(
                        x => x.Id == address.Id.Value && !x.Deleted
                    )).FirstOrDefault();

                    existingAddress!.Line1 = address.Line1;
                    existingAddress.Line2 = address.Line2;
                    existingAddress.Country = address.Country;
                    existingAddress.State = address.State;
                    existingAddress.City = address.City;
                    existingAddress.ZipCode = address.ZipCode;

                    await _addressService.Update(existingAddress);
                }
                else
                {
                    // Insert new Address
                    Address newAddress = new()
                    {
                        AccountGroupId = accountLedger.Id,
                        Line1 = address.Line1,
                        Line2 = address.Line2,
                        Country = address.Country,
                        State = address.State,
                        City = address.City,
                        ZipCode = address.ZipCode
                    };

                    await _addressService.Insert(newAddress);
                }
            }

            // Step 6: Success response
            response.setSuccessResponse(
                null!,
                "AccountLedger and Addresses updated successfully."
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
