using Lib.Core.Models.Response;
using MediatR;

namespace App.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdQuery : IRequest<ResponseModel>
{
    public long Id { get; set; }
}
