
using Lib.Core.Models.Response;
using MediatR;
using App.Infrastructure.Messages;
using App.Infrastructure.Services.Addresses;
using App.Infrastructure.Helpers;
using System.Net;

namespace App.Application.Features.Addresses.Commands.UpdateAddress;

public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, ResponseModel>
{
    private readonly IAddressService _addressService;

    public UpdateAddressCommandHandler(IAddressService addressService)
    {
        _addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
    }

    public async Task<ResponseModel> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        ResponseModel response = new();
        try
        {
            if (request is not null && request.Id != 0)
            {
                var getAddressData = (await _addressService.GetAsync(x => x.Id == request.Id && !x.Deleted)).FirstOrDefault();
                if (getAddressData != null)
                {
                    request.CopyPropertiesTo(getAddressData);
                    var updateAddress = await _addressService.Update(getAddressData);
                    if (updateAddress != null)
                    {
                        response.setSuccessResponse(null!, "Address updated successfully" + ResponseMessages.UpdateSuccess);

                    }
                }
            }
            else
            {
                response.StatusCode = 400;
                response.Errors.Add("Invalid address ID provided.");
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