using App.Application.Features.CompanyAddress.Commands.AddCompanyAddress;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace App.Application.Features.CompanyAddress.Commands.UpdateCompanyAddress
{
    public class UpdateCompanyAddressCommandHandler : IRequestHandler<UpdateCompanyAddressCommand, ResponseModel>
    {
        #region fields
        public readonly ICompanyService _companyService;
        private readonly ICompanyAddressService _companyAddressService;
        private readonly ICompanyDetailsService _companyDetailsService;
        private readonly ICompanyContactService _companyContactService;
        #endregion

        #region ctor
        public UpdateCompanyAddressCommandHandler(ICompanyService companyService,
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
        public async Task<ResponseModel> Handle(UpdateCompanyAddressCommand request, CancellationToken cancellationToken)
        {
            ResponseModel responseModel = new();
            try
            {

                if (request.AddressDetail.Count != null)
                {
                    foreach (var address in request.AddressDetail)
                    {
                        
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
                            isActive = (bool)address.isActive!
                        });
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
