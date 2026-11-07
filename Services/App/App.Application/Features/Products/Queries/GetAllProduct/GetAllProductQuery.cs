using Lib.Core.Models.Request;
using Lib.Core.Models.Response;
using MediatR;

namespace App.Application.Features.Products.Queries.GetAllProduct;

public class GetAllProductQuery : PaginationRequestModel, IRequest<ResponseModel>
{

}
