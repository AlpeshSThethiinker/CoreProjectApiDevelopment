using App.Domain.Entities.NewProduct;
using App.Infrastructure.Helpers;
using App.Infrastructure.Messages;
using App.Infrastructure.Services.Products;
using Lib.Core.Models.Response;
using MediatR;

namespace App.Application.Features.Products.Commands.AddProduct;

public class AddProductCommandHandler : IRequestHandler<AddProductCommand, ResponseModel>
{

    #region field
    private readonly IProductService _productService;
    #endregion

    #region ctor
    public AddProductCommandHandler(IProductService productService)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }
    #endregion

    #region method
    public async Task<ResponseModel> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        ResponseModel response = new();
        try
        {
            if (request is not null)
            {
                Product products = new();
                request.CopyPropertiesTo(products);
                var insertProduct = await _productService.Insert(products);
                if (insertProduct != null)
                {
                    response.setSuccessResponse(null!, "Product " + ResponseMessages.InsertSuccess);
                }
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
