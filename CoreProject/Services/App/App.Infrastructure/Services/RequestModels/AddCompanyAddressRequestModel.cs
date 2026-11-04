namespace App.Infrastructure.Services.InsertRequestModel
{
    public class AddCompanyAddressRequestModel
    {
        public string? CompanyId { get; set; }
        public string? AddressName { get; set; }
        public string? AddressTypeId { get; set; }
        public bool isDefault { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? CountryId { get; set; }
        public string? Phone { get; set; }
        public string? AddressVAT { get; set; }
        public bool isActive { get; set; }
    }
}
