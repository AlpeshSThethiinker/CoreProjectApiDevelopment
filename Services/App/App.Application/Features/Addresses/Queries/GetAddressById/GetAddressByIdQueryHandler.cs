
using App.Infrastructure.Helpers;
using App.Infrastructure.Messages;
using App.Infrastructure.Services.Addresses;
using Lib.Core.Models.Response;
using MediatR;


namespace App.Application.Features.Addresses.Queries.GetAddressById;

public class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQuery, ResponseModel>
{
    private readonly IAddressService _addressService;
    public GetAddressByIdQueryHandler(IAddressService addressService)
    {
        _addressService = addressService
            ?? throw new ArgumentNullException(nameof(addressService));
    }

    public async Task<ResponseModel> Handle(
        GetAddressByIdQuery request,
        CancellationToken cancellationToken)
    {
        ResponseModel response = new();

        try
        {
            var getAddress = (
                await _addressService.GetAsync(
                    x => x.Id == request.Id && !x.Deleted
                )
            ).FirstOrDefault();

            if (getAddress == null)
            {
                response.StatusCode = 404;
                response.Errors.Add("Address not found.");
                return response;
            }

            GetAddressByIdQueryResponse queryResponse = new();

            getAddress.CopyPropertiesTo(queryResponse);

            // Map database Id to AccountGroupId
            queryResponse.Id = getAddress.Id;

            response.setSuccessResponse(
                queryResponse,
                "Address Found."
            );

            return response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }


}
