using System.ComponentModel;

namespace App.Domain.Enums.Products;

public enum ProductCategory
{
    [Description("Clothing")]
    Clothing = 1,
    [Description("Jewellery")]
    Jewellery = 2,
    [Description("Automobiles")]
    Automobiles = 3,
    [Description("Kitchen")]
    Kitchen = 4,
    [Description("Electronic")]
    Electronic = 5,


}
