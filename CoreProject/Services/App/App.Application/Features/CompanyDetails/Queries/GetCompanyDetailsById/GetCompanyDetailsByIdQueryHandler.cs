using App.Infrastructure.Messages;
using App.Infrastructure.Services.Company_Contact;
using App.Infrastructure.Services.Company_Details;
using App.Infrastructure.Services.CompanyAddress;
using Lib.Core.Models;
using MediatR;
using static App.Infrastructure.Services.Company.CompanyServices;

namespace App.Application.Features.CompanyDetails.Queries.GetCompanyDetailsById
{
    public class GetCompanyDetailsByIdQueryHandler : IRequestHandler<GetCompanyDetailsByIdQuery, ResponseModel>
    {

        #region fields
        public readonly ICompanyService _companyService;
        private readonly ICompanyAddressService _companyAddressService;
        private readonly ICompanyDetailsService _companyDetailsService;
        private readonly ICompanyContactService _companyContactService;
        #endregion

        #region ctor
        public GetCompanyDetailsByIdQueryHandler(ICompanyService companyService,
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
        public async Task<ResponseModel> Handle(GetCompanyDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            ResponseModel response = new();
            try
            {
                var getResponse = await _companyDetailsService.GetCompanyDetailsByCompanyId(request.CompanyId!);

                if (getResponse != null)
                {
                    var assignedData = new GetCompanyDetailsByIdQueryResponse()
                    {
                        Id = getResponse.Id!,
                        CompanyId = getResponse.CompanyId,
                        DandBNumber = getResponse.DandBNumber,
                        IndustryId = getResponse.IndustryId,
                        SICCode = getResponse.SICCode,
                        NAICSCode = getResponse.NAICSCode,
                        ScopeId = getResponse.ScopeId,
                        EntityType = getResponse.EntityType,
                        Employees = getResponse.Employees,
                        Revenue = getResponse.Revenue,
                        ParentCompany = getResponse.ParentCompany,
                        CurrencyId = getResponse.CurrencyId,
                        TaxId = getResponse.TaxId,
                        VAT = getResponse.VAT,
                        ResellerId = getResponse.ResellerId,
                        ResellerIdExpDate = getResponse.ResellerIdExpDate,
                        Notes = getResponse.Notes
                    };

                    response.Response = assignedData;
                    response.IsSuccess = true;
                    response.Message = "Company Details Found";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Company Details Not found";
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
