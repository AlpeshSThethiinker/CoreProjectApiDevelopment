using App.Infrastructure.Helpers;
using App.Infrastructure.Messages;
using App.Infrastructure.Services.Products;
using Lib.Core.Models.Response;
using MediatR;

namespace App.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ResponseModel>
{

    #region field
    private readonly IProductService _productService;
    #endregion

    #region ctor
    public UpdateProductCommandHandler(IProductService productService)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }
    #endregion

    #region method
    public async Task<ResponseModel> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        ResponseModel response = new();
        try
        {
            if (request is not null && request.Id != 0)
            {
                var getProduct = (await _productService.GetAsync(x => x.Id == request.Id && !x.Deleted)).FirstOrDefault();
                if (getProduct != null)
                {
                    request.CopyPropertiesTo(getProduct);
                    var updateProduct = await _productService.Update(getProduct);
                    if (updateProduct != null)
                    {
                        response.setSuccessResponse(null!, "Product " + ResponseMessages.UpdateSuccess);
                    }
                }
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Errors.Add("Id is required for updating data");
                return response;
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
