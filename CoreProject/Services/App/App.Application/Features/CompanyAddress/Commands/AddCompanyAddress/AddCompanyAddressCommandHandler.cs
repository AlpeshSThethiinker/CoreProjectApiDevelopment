using App.Infrastructure.Services.Company_Contact;
using App.Infrastructure.Services.Company_Details;
using App.Infrastructure.Services.CompanyAddress;
using Azure;
using Lib.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static App.Infrastructure.Services.Company.CompanyServices;

namespace App.Application.Features.CompanyAddress.Commands.AddCompanyAddress
{
    public class AddCompanyAddressCommandHandler :IRequestHandler<AddCompanyAddressCommand, ResponseModel>
    {
        #region fields
        public readonly ICompanyService _companyService;
        private readonly ICompanyAddressService _companyAddressService;
        private readonly ICompanyDetailsService _companyDetailsService;
        private readonly ICompanyContactService _companyContactService;
        #endregion

        #region ctor
        public AddCompanyAddressCommandHandler(ICompanyService companyService,
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
        public async Task<ResponseModel> Handle(AddCompanyAddressCommand request, CancellationToken cancellationToken)
        {
            ResponseModel responseModel = new();
            try
            {
                
                if (request.AddressDetail!.Count != 0)
                {
                    foreach (var data in request.AddressDetail)
                    {
                        var insertcompanyaddress = await _companyAddressService.InsertCompanyAddress(new()
                        {
                            CompanyId = data.Id,
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
                        responseModel.IsSuccess = true;
                        responseModel.Message = "Company Address Added Successfully";
                    }
                    AddCompanyContactResponse response = new();

                
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
