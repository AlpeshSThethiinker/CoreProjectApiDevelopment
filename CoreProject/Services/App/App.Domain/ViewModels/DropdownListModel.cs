namespace App.Domain.ViewModels;
public class DropdownListModel
{
    public DropdownListModel()
    {
        CompanyDropdown = new List<DropdownModel>();
        ContactDropdown = new List<DropdownModel>();
        SalesPersonDropdown = new List<DropdownModel>();
        AltSalesPersonDropdown = new List<DropdownModel>();
        StateDropdown = new List<DropdownModel>();
        CountryDropdown = new List<DropdownModel>();
        BuyerRepDropdown = new List<DropdownModel>();
        SaleStageDropdown = new List<DropdownModel>();
        TypeDropdown = new List<DropdownModel>();
        IndustryDropdown = new List<DropdownModel>();
        ScopeDropdown = new List<DropdownModel>();
        CurrencyDropdown = new List<DropdownModel>();
        GenderDropdown = new List<DropdownModel>();
        DepartmentDropdown = new List<DropdownModel>();
        TimezoneDropdown = new List<DropdownModel>();
    }
    public List<DropdownModel>? CompanyDropdown { get; set; }
    public List<DropdownModel>? ContactDropdown { get; set; }
    public List<DropdownModel>? SalesPersonDropdown { get; set; }
    public List<DropdownModel>? AltSalesPersonDropdown { get; set; }
    public List<DropdownModel>? StateDropdown { get; set; }
    public List<DropdownModel>? CountryDropdown { get; set; }
    public List<DropdownModel>? BuyerRepDropdown { get; set; }
    public List<DropdownModel>? SaleStageDropdown { get; set; }
    public List<DropdownModel>? TypeDropdown { get; set; }
    public List<DropdownModel>? IndustryDropdown { get; set; }
    public List<DropdownModel>? ScopeDropdown { get; set; }
    public List<DropdownModel>? CurrencyDropdown { get; set; }
    public List<DropdownModel>? GenderDropdown { get; set; }
    public List<DropdownModel>? DepartmentDropdown { get; set; }
    public List<DropdownModel>? TimezoneDropdown { get; set; }
    public List<DropdownModel>? TagsDropdown { get; set; }
    public List<DropdownModel>? TypesDropdown { get; set; }
}
public class DropdownModel
{
    public string? Id { get; set; }
    public string? Name { get; set; }
}
