using App.Application.Features.Products.Queries.GetAllProduct;
using App.Infrastructure.Helpers;
using App.Infrastructure.Messages;
using App.Infrastructure.Services.Products;
using Lib.Core.Models.Response;
using MediatR;

namespace App.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetByIdQuery, ResponseModel>
{

    #region field
    private readonly IProductService _productService;
    #endregion

    #region ctor
    public GetProductByIdQueryHandler(IProductService productService)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }
    #endregion

    #region method
    public async Task<ResponseModel> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        ResponseModel response = new();
        try
        {
            GetAllProductQueryResponse queryResponse = new();
            var getProduct = (await _productService.GetAsync(x => x.Id == request.Id && !x.Deleted)).FirstOrDefault();
            if (getProduct != null)
            {
                getProduct.CopyPropertiesTo(queryResponse);

            }
            response.setSuccessResponse(queryResponse, "Record Found.");
            return response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

}
