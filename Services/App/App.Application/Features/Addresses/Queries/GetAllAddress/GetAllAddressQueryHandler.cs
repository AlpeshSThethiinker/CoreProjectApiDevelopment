
using App.Infrastructure.Messages;
using Lib.Core.Models.Response;
using App.Infrastructure.Helpers;
using MediatR;

using App.Infrastructure.Services.Addresses;
using App.Application.Features.Addresses.Queries.GetAllAddress;

namespace App.Application.Features.Addresses.Queries.GetAllAddress;         

public class GetAllAddressQueryHandler
    : IRequestHandler<GetAllAddressQuery, ResponseModel>
{
    private readonly IAddressService _addressService;
    public GetAllAddressQueryHandler(
        IAddressService addressService)
    {
        _addressService = addressService
            ?? throw new ArgumentNullException(nameof(addressService));
    }

    public async Task<ResponseModel> Handle(
        GetAllAddressQuery request,
        CancellationToken cancellationToken)
    {
        ResponseModel response = new();

        try
        {
            string orderBy = !string.IsNullOrWhiteSpace(request.OrderBy)
                ? request.OrderBy
                : !string.IsNullOrWhiteSpace(request.SortColumn)
                    ? $"{request.SortColumn} {request.SortDirection}"
                    : "Id ASC";

            var getAllAddress =
                await _addressService.GetAll(new()
                {
                    GetAll = request.GetAll,
                    PageSize = request.PageSize,
                    Page = request.Page,
                    SearchText = request.SearchText,
                    OrderBy = orderBy,
                    SortColumn = request.SortColumn,
                    SortDirection = request.SortDirection,
                    filters = request.filters,
                    sortList = request.sortList,
                    FromDate = request.FromDate,
                    ToDate = request.ToDate
                });

            if (getAllAddress.Count == 0)
            {
                response.StatusCode =
                    (int)HttpStatusCode.NoContent;

                response.Errors.Add(ResponseMessages.NotFound);
                return response;
            }

            List<MasterAddress> model = new();

            foreach (var address in getAllAddress)
            {
                MasterAddress vm = new();
                address.CopyPropertiesTo(vm);
                model.Add(vm);
            }

            GetAllAddressQueryResponse queryResponse = new()
            {
                Data = model
            };

            response.setSuccessResponse(queryResponse);
            return response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}

