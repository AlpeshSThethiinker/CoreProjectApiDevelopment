using App.Domain.Entities.NewProduct;
using App.Infrastructure.Messages;
using App.Infrastructure.Services.Products;
using Lib.Core.Models.Response;
using MediatR;

namespace App.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ResponseModel>
{

    #region field
    private readonly IProductService _productService;
    #endregion

    #region ctor
    public DeleteProductCommandHandler(IProductService productService)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }
    #endregion

    #region method
    public async Task<ResponseModel> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        ResponseModel response = new();
        try
        {
            Product? getProductData = (await _productService.GetAsync(x => x.Id == request.Id && !x.Deleted)).FirstOrDefault();
            if (getProductData is null)
            {
                response.Errors.Add(ResponseMessages.NotFound);
                response.StatusCode = (int)HttpStatusCode.NoContent;
                return response;
            }
            var deleteProduct = await _productService.Delete(request.Id);
            if (deleteProduct is not null)
            {
                response.setSuccessResponse(null!, "Product " + ResponseMessages.DeleteSuccess);
            }
            return response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion

}
