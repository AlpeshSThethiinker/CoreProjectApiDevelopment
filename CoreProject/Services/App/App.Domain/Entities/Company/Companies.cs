using Lib.Core.Domain;

namespace App.Domain.Entities.Company;
public class Companies : EntityBase
{
    public string? CompanyName { get; set; }
    public string? CompanyEmail { get; set; }
    public string? CompanyPhone { get; set; }
    public string? CompanyFax { get; set; }
    public string? StateId { get; set; } = string.Empty;
    public string? CountryId { get; set; } = string.Empty;
    public string? Website { get; set; }
    public string? TimeZoneId { get; set; }
    public string? SalesPersonId { get; set; } = string.Empty;
    public string? AltSalesPersonId { get; set; } = string.Empty;
    public string? BuyerRepId { get; set; } = string.Empty;
    public bool? ContactReminders { get; set; }
    public bool? DoNotCall { get; set; }
    public bool? MarkAsVendor { get; set; }
    public bool? ApprovedVendorList { get; set; }
    public decimal OverAllRating { get; set; }




}

