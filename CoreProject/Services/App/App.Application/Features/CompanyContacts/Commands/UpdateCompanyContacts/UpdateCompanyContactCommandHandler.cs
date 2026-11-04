using App.Application.Features.CompanyAddress.Commands.UpdateCompanyAddress;
using App.Infrastructure.Services.Company_Contact;
using App.Infrastructure.Services.Company_Details;
using App.Infrastructure.Services.CompanyAddress;
using Lib.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static App.Infrastructure.Services.Company.CompanyServices;

namespace App.Application.Features.CompanyContacts.Commands.UpdateCompanyContacts
{
    public  class UpdateCompanyContactCommandHandler : IRequestHandler<UpdateCompanyContactCommand, ResponseModel>
    {

        #region fields
        public readonly ICompanyService _companyService;
        private readonly ICompanyAddressService _companyAddressService;
        private readonly ICompanyDetailsService _companyDetailsService;
        private readonly ICompanyContactService _companyContactService;
        #endregion

        #region ctor
        public UpdateCompanyContactCommandHandler(ICompanyService companyService,
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

        #region addcompanyaddress
        public async Task<ResponseModel> Handle(UpdateCompanyContactCommand request, CancellationToken cancellationToken)
        {
            ResponseModel responseModel = new();
            try
            {

                if (string.IsNullOrEmpty(request.Id))
                {


                    var updateresponse = await _companyContactService.UpdateCompanyContact(new()
                    {
                        Id = request.Id,
                        CompanyId = request.CompanyId,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Title = request.Title,
                        Gender = request.Gender,
                        DepartmentId = request.DepartmentId,
                        Location = request.Location,
                        TimeZoneId = request.TimeZoneId,
                        Email = request.Email,
                        DirectionNumber=request.DirectionNumber,
                        MainOfficePhone=request.MainOfficePhone,
                        MobileNumber=request.MobileNumber,
                        Fax=request.Fax,
                        Xing=request.Xing,
                        Linkedin=request.Linkedin,
                        PrimaryAccounting=request.PrimaryAccounting,
                        PrimaryPurchasing=request.PrimaryPurchasing,
                        PrimaryContact=request.PrimaryContact,
                        DoNotCall=request.DoNotCall,
                        DoNotEmail=request.DoNotEmail,
                        Skype=request.Skype

                    });
                    if(updateresponse != null )
                    {
                        UpdateCompanyContactResponse commandresponse = new()
                        {
                            Id = updateresponse.Id,
                        CompanyId = updateresponse.CompanyId,
                        FirstName = updateresponse.FirstName,
                        LastName = updateresponse.LastName,
                        Title = updateresponse.Title,
                        Gender = updateresponse.Gender,
                        DepartmentId = updateresponse.DepartmentId,
                        Location = updateresponse.Location,
                        TimeZoneId = updateresponse.TimeZoneId,
                        Email = updateresponse.Email,
                        DirectionNumber= updateresponse.DirectionNumber,
                        MainOfficePhone= updateresponse.MainOfficePhone,
                        MobileNumber= updateresponse.MobileNumber,
                        Fax= updateresponse.Fax,
                        Xing= updateresponse.Xing,
                        Linkedin= updateresponse.Linkedin,
                        PrimaryAccounting= updateresponse.PrimaryAccounting,
                        PrimaryPurchasing= updateresponse.PrimaryPurchasing,
                        PrimaryContact= updateresponse.PrimaryContact,
                        DoNotCall= updateresponse.DoNotCall,
                        DoNotEmail= updateresponse.DoNotEmail,
                        Skype= updateresponse.Skype
                        };

                    }

                    
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
