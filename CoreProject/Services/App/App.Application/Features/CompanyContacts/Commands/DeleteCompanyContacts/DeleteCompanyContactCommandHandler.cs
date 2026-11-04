using App.Infrastructure.Messages;
using App.Infrastructure.Services.Company_Contact;
using App.Infrastructure.Services.Company_Details;
using App.Infrastructure.Services.CompanyAddress;
using Lib.Core.Models;
using static App.Infrastructure.Services.Company.CompanyServices;

namespace App.Application.Features.CompanyContacts.Commands.DeleteCompanyContacts;
public class DeleteCompanyContactCommandHandler
{
    #region fields
    public readonly ICompanyService _companyService;
    private readonly ICompanyAddressService _companyAddressService;
    private readonly ICompanyDetailsService _companyDetailsService;
    private readonly ICompanyContactService _companyContactService;
    #endregion

    #region ctor
    public DeleteCompanyContactCommandHandler(ICompanyService companyService,
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

    #region method
    public async Task<ResponseModel> Handle(DeleteCompanyContactCommand request, CancellationToken cancellationToken)
    {
        ResponseModel responseModel = new();

        try
        {
            if (string.IsNullOrEmpty(request.Id))
            {
                var deleteResponse = await _companyContactService.DeleteCompanyContactById(new()
                {
                    Id = request.Id
                });
                responseModel.IsSuccess = true;
                responseModel.Message = "Company Address Deleted successfully";
            }
            else
            {
                responseModel.IsSuccess = false;
                responseModel.Message = "Company Address  Not deleted";
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

