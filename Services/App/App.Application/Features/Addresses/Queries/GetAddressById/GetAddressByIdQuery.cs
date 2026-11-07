using Lib.Core.Models.Response;
using MediatR;

namespace App.Application.Features.Addresses.Queries.GetAddressById;

public class GetAddressByIdQuery : IRequest<ResponseModel>
{
    //public long? Id { get; set; }
    public long? Id { get; set; }

}