using Lib.Core.Models.Request;
using Lib.Core.Models.Response;
using MediatR;


namespace App.Application.Features.Addresses.Queries.GetAllAddress;

public class GetAllAddressQuery : PaginationRequestModel, IRequest<ResponseModel>
{
}
