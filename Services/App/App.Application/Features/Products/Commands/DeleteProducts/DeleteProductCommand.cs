using Lib.Core.Models.Response;
using MediatR;

namespace App.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest<ResponseModel>
{
    public long Id { get; set; }
}
