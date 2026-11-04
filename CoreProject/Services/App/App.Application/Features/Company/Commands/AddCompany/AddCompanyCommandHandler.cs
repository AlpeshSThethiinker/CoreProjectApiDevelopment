using App.Infrastructure.Services.Company_Contact;
using App.Infrastructure.Services.Company_Details;
using App.Infrastructure.Services.CompanyAddress;
using Lib.Core.Models;
using MediatR;
using static App.Infrastructure.Services.Company.CompanyServices;

namespace App.Application.Features.Company.Commands.AddCompany
{
    public class AddCompanyCommandHandler : IRequestHandler<AddCompanyCommand, ResponseModel>
    {
        #region fields
        public readonly ICompanyService _companyService;
        private readonly ICompanyAddressService _companyAddressService;
        private readonly ICompanyDetailsService _companyDetailsService;
        private readonly ICompanyContactService _companyContactService;
        #endregion

        #region ctor
        public AddCompanyCommandHandler(ICompanyService companyService,
                ICompanyAddressService companyAddressService,
                ICompanyDetailsService companyDetailsService,
                ICompanyContactService companyContactService)
        {
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
            _companyAddressService = companyAddressService ?? throw new ArgumentNullException(nameof(companyAddressService));
            _companyDetailsService = companyDetailsService ?? throw new ArgumentNullException(nameof(companyDetailsService));
            _companyContactService = companyContactService ?? throw new ArgumentNullException(nameof(companyContactService));
        }
        #endregion

        #region Insert Company, address, details, contacts
        public async Task<ResponseModel> Handle(AddCompanyCommand request, CancellationToken cancellationToken)
        {
            ResponseModel response = new();
            try
            {
                if (!string.IsNullOrEmpty(request.GeneralInfo.Id))
                {
                    var insertcompany = await _companyService.InsertCompany(new()
                    {

                        CompanyName = request.GeneralInfo.CompanyName,
                        CompanyEmail = request.GeneralInfo.CompanyEmail,
                        CompanyPhone = request.GeneralInfo.CompanyPhone,
                        CompanyFax = request.GeneralInfo.CompanyFax,
                        StateId = request.GeneralInfo.StateId,
                        CountryId = request.GeneralInfo.CountryId,
                        Website = request.GeneralInfo.Website,
                        TimeZoneId = request.GeneralInfo.TimeZoneId,
                        SalesPersonId = request.GeneralInfo.SalesPersonId,
                        AltSalesPersonId = request.GeneralInfo.AltSalesPersonId,
                        BuyerRepId = request.GeneralInfo.BuyerRepId,
                        ContactReminders = request.GeneralInfo.ContactReminders,
                        DoNotCall = request.GeneralInfo.DoNotCall,
                        MarkAsVendor = request.GeneralInfo.MarkAsVendor,
                        ApprovedVendorList = request.GeneralInfo.ApprovedVendorList,
                        OverAllRating = request.GeneralInfo.OverAllRating

                    });

                    if (insertcompany is not null)
                    {
                        if (request.GeneralInfo.AddressInfo is not null && request.GeneralInfo.AddressInfo.Count > 0)
                        {
                            foreach (var data in request.GeneralInfo.AddressInfo)
                            {
                                var insertcompanyaddress = await _companyAddressService.InsertCompanyAddress(new()
                                {
                                    CompanyId = insertcompany.Id,
                                    AddressName = data.AddressName,
                                    AddressTypeId = data.AddressTypeId,
                                    isDefault = data.isDefault,
                                    Address1 = data.Address1,
                                    Address2 = data.Address2,
                                    City = data.City,
                                    State = data.State,
                                    PostalCode = data.PostalCode,
                                    CountryId = data.CountryId,
                                    Phone = data.Phone,
                                    AddressVAT = data.AddressVAT,
                                    isActive = data.isActive
                                });
                            }
                        }

                        if (request.GeneralInfo.CompanyDetailsInfo is not null)
                        {
                            var insertcompanydetails = await _companyDetailsService.InsertCompanyDetails(new()
                            {
                                CompanyId = insertcompany.Id,
                                DandBNumber = request.GeneralInfo.CompanyDetailsInfo.DandBNumber,
                                IndustryId = request.GeneralInfo.CompanyDetailsInfo.IndustryId,
                                SICCode = request.GeneralInfo.CompanyDetailsInfo.SICCode,
                                NAICSCode = request.GeneralInfo.CompanyDetailsInfo.NAICSCode,
                                ScopeId = request.GeneralInfo.CompanyDetailsInfo.ScopeId,
                                EntityType = request.GeneralInfo.CompanyDetailsInfo.EntityType,
                                Employees = request.GeneralInfo.CompanyDetailsInfo.Employees,
                                Revenue = request.GeneralInfo.CompanyDetailsInfo.Revenue,
                                ParentCompany = request.GeneralInfo.CompanyDetailsInfo.ParentCompany,
                                CurrencyId = request.GeneralInfo.CompanyDetailsInfo.CurrencyId,
                                TaxId = request.GeneralInfo.CompanyDetailsInfo.TaxId,
                                VAT = request.GeneralInfo.CompanyDetailsInfo.VAT,
                                ResellerId = request.GeneralInfo.CompanyDetailsInfo.ResellerId,
                                ResellerIdExpDate = request.GeneralInfo.CompanyDetailsInfo.ResellerIdExpDate,
                                Notes = request.GeneralInfo.CompanyDetailsInfo.Notes

                            });
                        }

                        if (request.GeneralInfo.ContactInfo is not null && request.GeneralInfo.ContactInfo.Count > 0)
                        {
                            foreach (var data in request.GeneralInfo.ContactInfo)
                            {
                                var insertcontactinfo = await _companyContactService.InsertCompanyContact(new()
                                {
                                    CompanyId = insertcompany.Id,
                                    FirstName = data.FirstName,
                                    LastName = data.LastName,
                                    Title = data.Title,
                                    Gender = data.Gender,
                                    DepartmentId = data.DepartmentId,
                                    Location = data.Location,
                                    TimeZoneId = data.TimeZoneId,
                                    Email = data.Email,
                                    DirectionNumber = data.DirectionNumber,
                                    MainOfficePhone = data.MainOfficePhone,
                                    MobileNumber = data.MobileNumber,
                                    Fax = data.Fax,
                                    Xing = data.Xing,
                                    Linkedin = data.Linkedin,
                                    Skype = data.Skype,
                                    PrimaryContact = data.PrimaryContact,
                                    PrimaryAccounting = data.PrimaryAccounting,
                                    PrimaryPurchasing = data.PrimaryPurchasing,
                                    DoNotCall = data.DoNotCall,
                                    DoNotEmail = data.DoNotEmail
                                });
                            }
                        }
                    }
                    response.IsSuccess = true;
                    response.Message = "Company Added Successfully";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Insertion of details failed";
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

    }
}
