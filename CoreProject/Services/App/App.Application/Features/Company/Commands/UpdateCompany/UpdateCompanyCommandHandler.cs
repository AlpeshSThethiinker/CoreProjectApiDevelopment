using App.Domain.Entities.Company;
using App.Infrastructure.Messages;
using App.Infrastructure.Services.Company_Contact;
using App.Infrastructure.Services.Company_Details;
using App.Infrastructure.Services.CompanyAddress;
using Lib.Core.Models;
using MediatR;
using static App.Infrastructure.Services.Company.CompanyServices;

namespace App.Application.Features.Company.Commands.UpdateCompany
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, ResponseModel>
    {
        #region fields
        public readonly ICompanyService _companyService;
        private readonly ICompanyAddressService _companyAddressService;
        private readonly ICompanyDetailsService _companyDetailsService;
        private readonly ICompanyContactService _companyContactService;
        #endregion

        #region ctor
        public UpdateCompanyCommandHandler(ICompanyService companyService,
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

        public async Task<ResponseModel> Handle(UpdateCompanyCommand command, CancellationToken cancellationToken)
        {
            ResponseModel responseModel = new();
            try
            {

                if (!string.IsNullOrEmpty(command.GeneralInfo.Id))
                {

                    #region update company
                    var updatecompanyresponse = await _companyService.UpdateCompany(new()
                    {
                        Id = command.GeneralInfo.Id,
                        CompanyName = command.GeneralInfo.CompanyName,
                        CompanyEmail = command.GeneralInfo.CompanyEmail,
                        CompanyPhone = command.GeneralInfo.CompanyPhone,
                        CompanyFax = command.GeneralInfo.CompanyFax,
                        StateId = command.GeneralInfo.StateId,
                        CountryId = command.GeneralInfo.CountryId,
                        Website = command.GeneralInfo.Website,
                        TimeZoneId = command.GeneralInfo.TimeZoneId,
                        SalesPersonId = command.GeneralInfo.SalesPersonId,
                        AltSalesPersonId = command.GeneralInfo.AltSalesPersonId,
                        BuyerRepId = command.GeneralInfo.BuyerRepId,
                        ContactReminders = command.GeneralInfo.ContactReminders,
                        DoNotCall = command.GeneralInfo.DoNotCall,
                        MarkAsVendor = command.GeneralInfo.MarkAsVendor,
                        ApprovedVendorList = command.GeneralInfo.ApprovedVendorList,
                        OverAllRating = command.GeneralInfo.OverAllRating

                    });
                    #endregion

                    #region update company Address
                    if (command.GeneralInfo.AddressInfo.Count != 0)
                    {
                        foreach (var address in command.GeneralInfo.AddressInfo)
                        {
                            address.CompanyId = command.GeneralInfo.Id;
                            var updateaddressdetails = await _companyAddressService.UpdateCompanyAddress(new()
                            {
                                CompanyId = address.Id,
                                AddressName = address.AddressName,
                                AddressTypeId = address.AddressTypeId,
                                isDefault = address.isDefault,
                                Address1 = address.Address1,
                                Address2 = address.Address2,
                                City = address.City,
                                State = address.State,
                                PostalCode = address.PostalCode,
                                CountryId = address.CountryId,
                                Phone = address.Phone,
                                AddressVAT = address.AddressVAT,
                                isActive = address.isActive
                            });
                        }
                    }
                    #endregion

                    #region update company details
                    if (command.GeneralInfo.CompanyDetailsInfo is not null)
                    {
                        if (string.IsNullOrEmpty(command.GeneralInfo.CompanyDetailsInfo.Id))
                        {
                            CompanyDetailsInformation insertCompanyDetails = await _companyDetailsService.InsertCompanyDetailsData(new()
                            {
                                CompanyId = command.GeneralInfo.CompanyDetailsInfo.CompanyId,
                                DandBNumber = command.GeneralInfo.CompanyDetailsInfo.DandBNumber,
                                IndustryId = command.GeneralInfo.CompanyDetailsInfo.IndustryId,
                                SICCode = command.GeneralInfo.CompanyDetailsInfo.SICCode,
                                NAICSCode = command.GeneralInfo.CompanyDetailsInfo.NAICSCode,
                                ScopeId = command.GeneralInfo.CompanyDetailsInfo.ScopeId,
                                EntityType = command.GeneralInfo.CompanyDetailsInfo.EntityType,
                                Employees = command.GeneralInfo.CompanyDetailsInfo.Employees,
                                Revenue = command.GeneralInfo.CompanyDetailsInfo.Revenue,
                                ParentCompany = command.GeneralInfo.CompanyDetailsInfo.ParentCompany,
                                CurrencyId = command.GeneralInfo.CompanyDetailsInfo.CurrencyId,
                                TaxId = command.GeneralInfo.CompanyDetailsInfo.TaxId,
                                VAT = command.GeneralInfo.CompanyDetailsInfo.VAT,
                                ResellerId = command.GeneralInfo.CompanyDetailsInfo.ResellerId,
                                ResellerIdExpDate = command.GeneralInfo.CompanyDetailsInfo.ResellerIdExpDate,
                                Notes = command.GeneralInfo.CompanyDetailsInfo.Notes
                            });
                        }
                        else
                        {
                            var getCompanyDetailsData = (await _companyDetailsService.GetAsync(x => x.Id == command.GeneralInfo.CompanyDetailsInfo.Id && !x.IsDeleted)).FirstOrDefault();
                            if (getCompanyDetailsData != null)
                            {
                                getCompanyDetailsData.CompanyId = command.GeneralInfo.CompanyDetailsInfo.CompanyId;
                                getCompanyDetailsData.DandBNumber = command.GeneralInfo.CompanyDetailsInfo.DandBNumber;
                                getCompanyDetailsData.IndustryId = command.GeneralInfo.CompanyDetailsInfo.IndustryId;
                                getCompanyDetailsData.SICCode = command.GeneralInfo.CompanyDetailsInfo.SICCode;
                                getCompanyDetailsData.NAICSCode = command.GeneralInfo.CompanyDetailsInfo.NAICSCode;
                                getCompanyDetailsData.ScopeId = command.GeneralInfo.CompanyDetailsInfo.ScopeId;
                                getCompanyDetailsData.EntityType = command.GeneralInfo.CompanyDetailsInfo.EntityType;
                                getCompanyDetailsData.Employees = command.GeneralInfo.CompanyDetailsInfo.Employees;
                                getCompanyDetailsData.Revenue = command.GeneralInfo.CompanyDetailsInfo.Revenue;
                                getCompanyDetailsData.ParentCompany = command.GeneralInfo.CompanyDetailsInfo.ParentCompany;
                                getCompanyDetailsData.CurrencyId = command.GeneralInfo.CompanyDetailsInfo.CurrencyId;
                                getCompanyDetailsData.TaxId = command.GeneralInfo.CompanyDetailsInfo.TaxId;
                                getCompanyDetailsData.VAT = command.GeneralInfo.CompanyDetailsInfo.VAT;
                                getCompanyDetailsData.ResellerId = command.GeneralInfo.CompanyDetailsInfo.ResellerId;
                                getCompanyDetailsData.ResellerIdExpDate = command.GeneralInfo.CompanyDetailsInfo.ResellerIdExpDate;
                                getCompanyDetailsData.Notes = command.GeneralInfo.CompanyDetailsInfo.Notes;
                                await _companyDetailsService.UpdateCompanyDetailsData(getCompanyDetailsData);
                            }
                        }
                    }
                    #endregion

                    #region update company contact
                    if (command.GeneralInfo.ContactInfo?.Count is not null)
                    {
                        foreach (var request in command.GeneralInfo.ContactInfo)
                        {
                            request.CompanyId = command.GeneralInfo.Id;
                            var updatecompanycontact = await _companyContactService.UpdateCompanyContact(new()
                            {
                                CompanyId = request.CompanyId,
                                FirstName = request.FirstName,
                                LastName = request.LastName,
                                Title = request.Title,
                                Gender = request.Gender,
                                DepartmentId = request.DepartmentId,
                                Location = request.Location,
                                TimeZoneId = request.TimeZoneId,
                                Email = request.Email,
                                DirectionNumber = request.DirectionNumber,
                                MainOfficePhone = request.MainOfficePhone,
                                MobileNumber = request.MobileNumber,
                                Fax = request.Fax,
                                Xing = request.Xing,
                                Linkedin = request.Linkedin,
                                PrimaryAccounting = request.PrimaryAccounting,
                                PrimaryContact = request.PrimaryContact,
                                PrimaryPurchasing = request.PrimaryPurchasing,
                                DoNotCall = request.DoNotCall,
                                DoNotEmail = request.DoNotEmail,
                                Skype = request.Skype
                            });
                        }
                    }
                    #endregion


                    #region Update Response Body
                    var getcompanybyid = await _companyService.GetCompanyId(command.GeneralInfo.Id);
                    if (getcompanybyid != null)
                    {
                        List<UpdateCompanyCommandResponse> queryResponse = new();

                        UpdateCompanyCommandResponse assignedGeneralInfoData = new();

                        assignedGeneralInfoData.GeneralInfo.Id = getcompanybyid.Id;
                        assignedGeneralInfoData.GeneralInfo.CompanyName = getcompanybyid.CompanyName;
                        assignedGeneralInfoData.GeneralInfo.CompanyEmail = getcompanybyid.CompanyEmail;
                        assignedGeneralInfoData.GeneralInfo.CompanyPhone = getcompanybyid.CompanyPhone;
                        assignedGeneralInfoData.GeneralInfo.CompanyFax = getcompanybyid.CompanyFax;
                        assignedGeneralInfoData.GeneralInfo.StateId = getcompanybyid.StateId;
                        assignedGeneralInfoData.GeneralInfo.CountryId = getcompanybyid.CountryId;
                        assignedGeneralInfoData.GeneralInfo.Website = getcompanybyid.Website;
                        assignedGeneralInfoData.GeneralInfo.TimeZoneId = getcompanybyid.TimeZoneId;
                        assignedGeneralInfoData.GeneralInfo.SalesPersonId = getcompanybyid.SalesPersonId;
                        assignedGeneralInfoData.GeneralInfo.AltSalesPersonId = getcompanybyid.AltSalesPersonId;
                        assignedGeneralInfoData.GeneralInfo.BuyerRepId = getcompanybyid.BuyerRepId;
                        assignedGeneralInfoData.GeneralInfo.ContactReminders = getcompanybyid.ContactReminders;
                        assignedGeneralInfoData.GeneralInfo.DoNotCall = getcompanybyid.DoNotCall;
                        assignedGeneralInfoData.GeneralInfo.MarkAsVendor = getcompanybyid.MarkAsVendor;
                        assignedGeneralInfoData.GeneralInfo.ApprovedVendorList = getcompanybyid.ApprovedVendorList;
                        assignedGeneralInfoData.GeneralInfo.OverAllRating = getcompanybyid.OverAllRating;
                        assignedGeneralInfoData.GeneralInfo.AddressInfo = await _companyAddressService.GetAddressesByCompanyId(getcompanybyid.Id);
                        assignedGeneralInfoData.GeneralInfo.ContactInfo = await _companyContactService.GetContactsByCompanyId(getcompanybyid.Id);
                        assignedGeneralInfoData.GeneralInfo.CompanyDetailsInfo = await _companyDetailsService.GetCompanyDetailsByCompanyId(getcompanybyid.Id);
                        queryResponse.Add(assignedGeneralInfoData);
                        #endregion

                        responseModel.Response = queryResponse;
                        responseModel.IsSuccess = true;
                        responseModel.Message = "Company Updated successfully";
                        responseModel.Response = command;
                    }
                    else
                    {
                        responseModel.IsSuccess = false;
                        responseModel.Message = "Company Not Updated";
                    }
                }
                return responseModel;
            }
            catch (Exception ex)
            {
                responseModel.Message = ResponseMessages.ExceptionOccured + ex.Message;
                return responseModel;
            }
        }
    }
}
