
using App.Infrastructure.Messages;
using App.Infrastructure.Services.Addresses;
using Lib.Core.Models.Response;
using MediatR;
using AddressEntity = App.Domain.Entities.Addresses.Address;

namespace App.Application.Features.Addresses.Commands.DeleteAddress;

public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, ResponseModel>
{
    private readonly IAddressService _addressService;

    //public DeleteAddressCommandHandler(IAddressService AddressService)
    //{
    //    _addressService = addressService ?? throw new ArgumentNullException(nameof(AddressService));
    //}

    public DeleteAddressCommandHandler(IAddressService addressService)
    {
        _addressService = addressService
            ?? throw new ArgumentNullException(nameof(addressService));
    }

    public async Task<ResponseModel> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        ResponseModel response = new();
        try
        {
            AddressEntity? getAddressData = (await _addressService.GetAsync(x => x.Id == request.Id && !x.Deleted)).FirstOrDefault();
            if (getAddressData is null)
            {
                response.StatusCode = 404;
                response.Errors.Add(ResponseMessages.NotFound);
                return response;
            }
            var deleteAddress = await _addressService.Delete(request.Id);
            if (deleteAddress != null)
            {
                response.setSuccessResponse(null!, "Address " + ResponseMessages.DeleteSuccess);
            }
            return response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
