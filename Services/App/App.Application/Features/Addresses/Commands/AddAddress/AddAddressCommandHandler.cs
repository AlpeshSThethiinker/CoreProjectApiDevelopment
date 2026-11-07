using App.Domain.Entities.Addresses;
using App.Infrastructure.Helpers;
using App.Infrastructure.Messages;
using App.Infrastructure.Services.Addresses;
using Lib.Core.Models.Response;
using MediatR;

namespace App.Application.Features.Addresses.Commands.AddAddress;

public class AddAddressCommandHandler : IRequestHandler<AddAddressCommand, ResponseModel>
{
    private readonly IAddressService _addressService;
    public AddAddressCommandHandler(IAddressService addressService)
    {
        _addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
    }
    public async Task<ResponseModel> Handle(AddAddressCommand request, CancellationToken cancellationToken)
    {
        ResponseModel response = new();
        try
        {
            if(request is not null)
            {
                Address address = new();
                request.CopyPropertiesTo(address);
                var insertAddress = await _addressService.Insert(address);
                if(insertAddress != null)
                {
                    response.setSuccessResponse(null!, "Address" + ResponseMessages.InsertSuccess);
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
