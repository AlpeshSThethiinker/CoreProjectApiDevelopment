namespace App.Domain.ViewModels.Products;

public class ProductVM
{
    public long Id { get; set; }
    public string? SKU { get; set; }
    public string? Name { get; set; }
    public string? ShortName { get; set; }
    public long Price { get; set; }
    public int Category { get; set; }
    public DateTime DeliveryTimeSpan { get; set; }
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
}
