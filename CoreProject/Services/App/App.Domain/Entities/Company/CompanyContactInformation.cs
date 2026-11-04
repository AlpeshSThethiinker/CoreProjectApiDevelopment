using Lib.Core.Domain;

namespace App.Domain.Entities.Company;

public class CompanyContactInformation : EntityBase
{
    public string? CompanyId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Title { get; set; }
    public string? Gender { get; set; }
    public string? DepartmentId { get; set; }
    public string? Location { get; set; }
    public string? TimeZoneId { get; set; }
    public string? Email { get; set; }
    public string? DirectionNumber { get; set; }
    public string? MainOfficePhone { get; set; }
    public string? MobileNumber { get; set; }
    public string? Fax { get; set; }
    public string? Xing { get; set; }
    public string? Linkedin { get; set; }
    public bool PrimaryContact { get; set; }
    public bool PrimaryAccounting { get; set; }
    public bool PrimaryPurchasing { get; set; }
    public bool DoNotEmail { get; set; }
    public bool DoNotCall { get; set; }
    public string? Skype { get; set; }
    public virtual Companies? Companies { get; set; }

}

