using App.Infrastructure.Messages;
using App.Infrastructure.Services.Company_Contact;
using App.Infrastructure.Services.Company_Details;
using App.Infrastructure.Services.CompanyAddress;
using Lib.Core.Models;
using MediatR;
using static App.Infrastructure.Services.Company.CompanyServices;

namespace App.Application.Features.Company.Queries.GetAllCompany;
public class GetAllCompanyQueryHandler : IRequestHandler<GetAllCompanyQuery, ResponseModel>
{

    #region fields
    public readonly ICompanyService _companyService;
    private readonly ICompanyAddressService _companyAddressService;
    private readonly ICompanyDetailsService _companyDetailsService;
    private readonly ICompanyContactService _companyContactService;
    #endregion

    #region ctor
    public GetAllCompanyQueryHandler(ICompanyService companyService,
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

    #region get all companies
    public async Task<ResponseModel> Handle(GetAllCompanyQuery request, CancellationToken cancellationToken)
    {
        ResponseModel responseModel = new();
        try
        {
            var getcompanyinfo = await _companyService.GetAllCompany();
            if (getcompanyinfo != null && getcompanyinfo.Count > 0)
            {
                List<GetAllCompanyQueryResponse> queryResponse = new();
                foreach (var item in getcompanyinfo)
                {
                    if (!item.IsDeleted)
                    {
                        GetAllCompanyQueryResponse assignedGeneralInfoData = new();
                        assignedGeneralInfoData.GeneralInfo.Id = item.Id;
                        assignedGeneralInfoData.GeneralInfo.CompanyName = item.CompanyName;
                        assignedGeneralInfoData.GeneralInfo.CompanyEmail = item.CompanyEmail;
                        assignedGeneralInfoData.GeneralInfo.CompanyPhone = item.CompanyPhone;
                        assignedGeneralInfoData.GeneralInfo.CompanyFax = item.CompanyFax;
                        assignedGeneralInfoData.GeneralInfo.StateId = item.StateId;
                        assignedGeneralInfoData.GeneralInfo.CountryId = item.CountryId;
                        assignedGeneralInfoData.GeneralInfo.Website = item.Website;
                        assignedGeneralInfoData.GeneralInfo.TimeZoneId = item.TimeZoneId;
                        assignedGeneralInfoData.GeneralInfo.SalesPersonId = item.SalesPersonId;
                        assignedGeneralInfoData.GeneralInfo.AltSalesPersonId = item.AltSalesPersonId;
                        assignedGeneralInfoData.GeneralInfo.BuyerRepId = item.BuyerRepId;
                        assignedGeneralInfoData.GeneralInfo.ContactReminders = item.ContactReminders;
                        assignedGeneralInfoData.GeneralInfo.DoNotCall = item.DoNotCall;
                        assignedGeneralInfoData.GeneralInfo.MarkAsVendor = item.MarkAsVendor;
                        assignedGeneralInfoData.GeneralInfo.ApprovedVendorList = item.ApprovedVendorList;
                        assignedGeneralInfoData.GeneralInfo.OverAllRating = item.OverAllRating;
                        assignedGeneralInfoData.GeneralInfo.AddressInfo = await _companyAddressService.GetAddressesByCompanyId(item.Id);
                        assignedGeneralInfoData.GeneralInfo.ContactInfo = await _companyContactService.GetContactsByCompanyId(item.Id);
                        assignedGeneralInfoData.GeneralInfo.CompanyDetailsInfo = await _companyDetailsService.GetCompanyDetailsByCompanyId(item.Id);
                        queryResponse.Add(assignedGeneralInfoData);
                    }
                }
                responseModel.Response = queryResponse;
                responseModel.IsSuccess = true;
                responseModel.Message = "Companies Found";
            }
            else
            {
                responseModel.IsSuccess = false;
                responseModel.Message = "No Companies Found";
            }
            return responseModel;
        }
        catch (Exception ex)
        {
            responseModel.Message = ResponseMessages.ExceptionOccured + ex.Message;
            return responseModel;
        }
    }
    #endregion

}

