using App.Infrastructure.Services.Company_Contact;
using App.Infrastructure.Services.Company_Details;
using App.Infrastructure.Services.CompanyAddress;
using Lib.Core.Models;
using MediatR;
using static App.Infrastructure.Services.Company.CompanyServices;

namespace App.Application.Features.CompanyDetails.Commands.AddDetailsAddress
{
    public class AddCompanyDetailsCommandHandler : IRequestHandler<AddCompanyDetailsCommand, ResponseModel>
    {
        #region fields
        public readonly ICompanyService _companyService;
        private readonly ICompanyAddressService _companyAddressService;
        private readonly ICompanyDetailsService _companyDetailsService;
        private readonly ICompanyContactService _companyContactService;
        #endregion

        #region ctor
        public AddCompanyDetailsCommandHandler(ICompanyService companyService,
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

        #region addcompanydetails
        public async Task<ResponseModel> Handle(AddCompanyDetailsCommand request, CancellationToken cancellationToken)
        {
            ResponseModel responseModel = new();
            try
            {

                if (string.IsNullOrEmpty(request.Id))
                {

                    var insertResponse = await _companyDetailsService.InsertCompanyDetails(new()
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
                    });
                    if (insertResponse != null)
                    {
                        AddCompanyDetailsCommandResponse response = new()
                        {
                            Id = insertResponse.Id!,
                            CompanyId = insertResponse.CompanyId,
                            DandBNumber = insertResponse.DandBNumber,
                            IndustryId = insertResponse.IndustryId,
                            SICCode = insertResponse.SICCode,
                            NAICSCode = insertResponse.NAICSCode,
                            ScopeId = insertResponse.ScopeId,
                            EntityType = insertResponse.EntityType,
                            Employees = insertResponse.Employees,
                            Revenue = insertResponse.Revenue,
                            ParentCompany = insertResponse.ParentCompany,
                            CurrencyId = insertResponse.CurrencyId,
                            TaxId = insertResponse.TaxId,
                            VAT = insertResponse.VAT,
                            ResellerId = insertResponse.ResellerId,
                            ResellerIdExpDate = insertResponse.ResellerIdExpDate,
                            Notes = insertResponse.Notes
                        };
                    }
                    responseModel.IsSuccess = true;
                    responseModel.Message = "Company Details Added Successfully";
                }



                else
                {
                    responseModel.IsSuccess = false;
                    responseModel.Message = "Insertion of Company Details failed";
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
