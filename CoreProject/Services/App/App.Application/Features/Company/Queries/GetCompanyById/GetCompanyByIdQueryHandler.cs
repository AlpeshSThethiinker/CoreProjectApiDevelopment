using App.Application.Features.Company.Queries.GetCompanyById;
using App.Infrastructure.Messages;
using App.Infrastructure.Services.Company_Contact;
using App.Infrastructure.Services.Company_Details;
using App.Infrastructure.Services.CompanyAddress;
using Lib.Core.Models;
using MediatR;
using static App.Infrastructure.Services.Company.CompanyServices;
namespace App.Application.Features.CompanyAddress.Queries.GetCompanyAddressById;
public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, ResponseModel>
{

    #region fields
    public readonly ICompanyService _companyService;
    private readonly ICompanyAddressService _companyAddressService;
    private readonly ICompanyDetailsService _companyDetailsService;
    private readonly ICompanyContactService _companyContactService;
    #endregion

    #region ctor
    public GetCompanyByIdQueryHandler(ICompanyService companyService,
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

    #region get company by id method
    public async Task<ResponseModel> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
    {
        ResponseModel response = new();
        try
        {
            //211b3fe8-5c3a-4318-ba20-cb3c02ca7ca8
            var getcompanybyid = await _companyService.GetCompanyId(request.CompanyId!);
            if (getcompanybyid != null)
            {
                List<GetCompanyByIdQueryResponse> queryResponse = new();

                GetCompanyByIdQueryResponse assignedGeneralInfoData = new();
                if (!getcompanybyid.IsDeleted)
                {
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
                    response.Response = queryResponse;
                    response.IsSuccess = true;
                    response.Message = "Company Found";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Company already deleted";
                }
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Company Not found";
            }
            return response;
        }
        catch (Exception ex)
        {
            response.Message = ResponseMessages.ExceptionOccured + ex.Message;
            return response;
        }
    }
    #endregion

}