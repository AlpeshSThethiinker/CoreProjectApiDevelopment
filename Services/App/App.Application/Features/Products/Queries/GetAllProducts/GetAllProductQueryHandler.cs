using App.Infrastructure.Helpers;
using App.Infrastructure.Messages;
using App.Infrastructure.Services.Products;
using Lib.Core.Models.Response;
using MediatR;

namespace App.Application.Features.Products.Queries.GetAllProduct;

public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, ResponseModel>
{

    #region field
    private readonly IProductService _productService;
    #endregion

    #region ctor
    public GetAllProductQueryHandler(IProductService productService)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
    }
    #endregion

    #region method
    public async Task<ResponseModel> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        ResponseModel response = new();
        try
        {
            var getAllProduct = await _productService.GetAll(new()
            {
                GetAll = request.GetAll,
                PageSize = request.PageSize,
                Page = request.Page,
                SearchText = request.SearchText,
                SortColumn = request.SortColumn,
                SortDirection = request.SortDirection,
                filters = request.filters,
                sortList = request.sortList,
                FromDate = request.FromDate,
                ToDate = request.ToDate
            });

            if (getAllProduct.Count == 0)
            {
                response.StatusCode = (int)HttpStatusCode.NoContent;
                response.Errors.Add(ResponseMessages.NotFound);
                return response;
            }
            List<MasterProduct> model = new();
            foreach (var product in getAllProduct)
            {
                MasterProduct vm = new();

                product.CopyPropertiesTo(vm);
                model.Add(vm);
            }
            GetAllProductQueryResponse queryResponse = new()
            {
                Data = model
            };

            response.setSuccessResponse(queryResponse);
            return response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion
}
