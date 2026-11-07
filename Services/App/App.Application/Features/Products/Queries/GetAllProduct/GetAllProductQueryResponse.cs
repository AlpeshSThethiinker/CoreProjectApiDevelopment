using App.Domain.Enums.Products;
using Lib.Core.Extensions.Enums;

namespace App.Application.Features.Products.Queries.GetAllProduct;

public class GetAllProductQueryResponse
{
    public List<MasterProduct> Data { get; set; }
    public GetAllProductQueryResponse()
    {
        Data = new List<MasterProduct>();
    }
}

public class MasterProduct
{
    public long Id { get; set; }
    public string? SKU { get; set; }
    public string? Name { get; set; }
    public string? ShortName { get; set; }
    public long Price { get; set; }
    public int Category { get; set; }
    public string? CategoryName => Enum.IsDefined(typeof(ProductCategory), Convert.ToInt32(Category)) ? ((ProductCategory)Convert.ToInt32(Category)).GetEnumDescription() : string.Empty;
    public DateTime DeliveryTimeSpan { get; set; }
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
}