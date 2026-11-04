using App.Infrastructure.Messages;
using App.Infrastructure.Services.Company_Contact;
using App.Infrastructure.Services.Company_Details;
using App.Infrastructure.Services.CompanyAddress;
using Lib.Core.Models;
using MediatR;
using static App.Infrastructure.Services.Company.CompanyServices;

namespace App.Application.Features.CompanyContacts.Queries.GetCompanyContactsById
{
    public class GetCompanyContactByIdQueryHandler : IRequestHandler<GetCompanyContactByIdQuery, ResponseModel>
    {

        #region fields
        public readonly ICompanyService _companyService;
        private readonly ICompanyAddressService _companyAddressService;
        private readonly ICompanyDetailsService _companyDetailsService;
        private readonly ICompanyContactService _companyContactService;
        #endregion

        #region ctor
        public GetCompanyContactByIdQueryHandler(ICompanyService companyService,
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

        #region get company by id
        public async Task<ResponseModel> Handle(GetCompanyContactByIdQuery request, CancellationToken cancellationToken)
        {
            ResponseModel response = new();
            try
            {
                List<GetCompanyContactByIdQueryResponse> commandResponse = [];
                var getResponse = await _companyContactService.GetContactsByCompanyId(request.CompanyId!);

                if (getResponse != null)
                {
                    foreach (var item in getResponse)
                    {
                        var assignedData = new GetCompanyContactByIdQueryResponse()
                        {
                            CompanyId = item.CompanyId,
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            Title = item.Title,
                            Gender = item.Gender,
                            DepartmentId = item.DepartmentId,
                            Location = item.Location,
                            TimeZoneId = item.TimeZoneId,
                            Email = item.Email,
                            DirectionNumber = item.DirectionNumber,
                            MainOfficePhone = item.MainOfficePhone,
                            MobileNumber = item.MobileNumber,
                            Fax = item.Fax,
                            Xing = item.Xing,
                            Linkedin = item.Linkedin,
                            PrimaryAccounting = item.PrimaryAccounting,
                            PrimaryPurchasing = item.PrimaryPurchasing,
                            PrimaryContact = item.PrimaryContact,
                            DoNotCall = item.DoNotCall,
                            DoNotEmail = item.DoNotEmail,
                            Skype = item.Skype
                        };
                        commandResponse.Add(assignedData);
                    }
                    response.IsSuccess = true;
                    response.Message = "Company Contact Found";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Company Contact Not found";
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
}
