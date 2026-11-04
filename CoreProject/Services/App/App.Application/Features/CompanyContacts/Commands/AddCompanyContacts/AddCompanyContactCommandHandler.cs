using App.Infrastructure.Services.Company_Contact;
using App.Infrastructure.Services.Company_Details;
using App.Infrastructure.Services.CompanyAddress;
using Lib.Core.Models;
using MediatR;
using static App.Infrastructure.Services.Company.CompanyServices;

namespace App.Application.Features.CompanyAddress.Commands.AddCompanyContacts
{
    public class AddCompanyContactsCommandHandler : IRequestHandler<AddCompanyContactCommand, ResponseModel>
    {
        #region fields
        public readonly ICompanyService _companyService;
        private readonly ICompanyAddressService _companyAddressService;
        private readonly ICompanyDetailsService _companyDetailsService;
        private readonly ICompanyContactService _companyContactService;
        #endregion

        #region ctor
        public AddCompanyContactsCommandHandler(ICompanyService companyService,
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

        #region addcompanyContact
        public async Task<ResponseModel> Handle(AddCompanyContactCommand request, CancellationToken cancellationToken)
        {
            ResponseModel responseModel = new();
            try
            {

                if (string.IsNullOrEmpty(request.Id))
                {

                    var insertResponse = await _companyContactService.InsertCompanyContact(new()
                    {
                        CompanyId = request.Id,
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
                    if (insertResponse != null)
                    {
                        AddCompanyContactResponse response = new()
                        {
                            CompanyId = insertResponse.Id,
                            FirstName = insertResponse.FirstName,
                            LastName = insertResponse.LastName,
                            Title = insertResponse.Title,
                            Gender = insertResponse.Gender,
                            DepartmentId = insertResponse.DepartmentId,
                            Location = insertResponse.Location,
                            TimeZoneId = insertResponse.TimeZoneId,
                            Email = insertResponse.Email,
                            DirectionNumber = insertResponse.DirectionNumber,
                            MainOfficePhone = insertResponse.MainOfficePhone,
                            Fax = insertResponse.Fax,
                            Xing = insertResponse.Xing,
                            Linkedin = insertResponse.Linkedin,
                            PrimaryAccounting = insertResponse.PrimaryAccounting,
                            PrimaryContact = insertResponse.PrimaryContact,
                            PrimaryPurchasing = insertResponse.PrimaryPurchasing,
                            DoNotCall = insertResponse.DoNotCall,
                            DoNotEmail = insertResponse.DoNotEmail,
                            Skype = insertResponse.Skype
                        };
                    }
                    responseModel.IsSuccess = true;
                    responseModel.Message = "Company Address Added Successfully";
                }



                else
                {
                    responseModel.IsSuccess = false;
                    responseModel.Message = "Insertion of Address details failed";
                }
                return responseModel;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


        }
        #endregion
    }
}
