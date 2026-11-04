using App.Application.Features.CompanyContacts.Commands.UpdateCompanyContacts;
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

namespace App.Application.Features.CompanyDetails.Commands.UpdateDetailsAddress
{
    public class UpdateCompanyDetailsCommandHandler : IRequestHandler<UpdateCompanyDetailsCommand, ResponseModel>
    {
        #region fields
        public readonly ICompanyService _companyService;
        private readonly ICompanyAddressService _companyAddressService;
        private readonly ICompanyDetailsService _companyDetailsService;
        private readonly ICompanyContactService _companyContactService;
        #endregion

        #region ctor
        public UpdateCompanyDetailsCommandHandler(ICompanyService companyService,
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

        #region Updatescompanydetails
        public async Task<ResponseModel> Handle(UpdateCompanyDetailsCommand request, CancellationToken cancellationToken)
        {
            ResponseModel responseModel = new();
            try
            {

                if (!string.IsNullOrEmpty(request.Id))
                {
                    var updateresponse = await _companyDetailsService.UpdateCompanyDetails(new()
                    {
                        Id= request.Id!,
                        CompanyId= request.CompanyId,
                        DandBNumber=request.DandBNumber,
                        IndustryId=request.IndustryId,
                        SICCode=request.SICCode,
                        NAICSCode=request.NAICSCode,
                        ScopeId=request.ScopeId,
                        EntityType=request.EntityType,
                        Employees=request.Employees,
                        Revenue=request.Revenue,
                        ParentCompany=request.ParentCompany,
                        CurrencyId=request.CurrencyId,
                        TaxId=request.TaxId,
                        VAT=request.VAT,
                        ResellerId=request.ResellerId,
                        ResellerIdExpDate=request.ResellerIdExpDate,
                        Notes=request.Notes

                    });
                    if (updateresponse != null)
                    {
                        UpdateCompanyDetailsCommandResponse commandresponse = new()
                        {
                            Id = request.Id!,
                            CompanyId = request.CompanyId,
                            DandBNumber = request.DandBNumber,
                            IndustryId = request.IndustryId,
                            SICCode = request.SICCode,
                            NAICSCode = request.NAICSCode,
                            ScopeId = request.ScopeId,
                            EntityType = request.EntityType,
                            Employees = request.Employees,
                            Revenue = request.Revenue,
                            ParentCompany = request.ParentCompany,
                            CurrencyId = request.CurrencyId,
                            TaxId = request.TaxId,
                            VAT = request.VAT,
                            ResellerId = request.ResellerId,
                            ResellerIdExpDate = request.ResellerIdExpDate,
                            Notes = request.Notes
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
