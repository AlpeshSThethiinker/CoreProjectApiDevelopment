using App.Infrastructure.Messages;
using App.Infrastructure.Services.Company_Contact;
using App.Infrastructure.Services.Company_Details;
using App.Infrastructure.Services.CompanyAddress;
using Lib.Core.Models;
using MediatR;
using static App.Infrastructure.Services.Company.CompanyServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace App.Application.Features.Company.Commands.DeleteCompany
{
    public  class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, ResponseModel>
    {
        #region fields
        public readonly ICompanyService _companyService;
        private readonly ICompanyAddressService _companyAddressService;
        private readonly ICompanyDetailsService _companyDetailsService;
        private readonly ICompanyContactService _companyContactService;
        #endregion

        #region ctor
        public DeleteCompanyCommandHandler(ICompanyService companyService,
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

        #region delete company method
        public async Task<ResponseModel> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            ResponseModel responseModel = new();

            try
            {
                if(request.Id.Count != 0 )
                {
                    List<string> Ids = [];
                    foreach(var id in request.Id)
                    {
                        var deleteResponse = await _companyService.DeleteCompanyById(new()
                        {
                           Id = id
                        });
                        if(deleteResponse != null)
                        {
                            string deleteId = deleteResponse.Id;
                            Ids.Add(deleteId);
                        }
                    }
                    DeleteCompanyCommandResponse commandResponse = new()
                    {
                        Id = Ids
                    };
                    responseModel.IsSuccess = true;
                    responseModel.Message = "Company Deleted successfully";

                }
                else
                {
                    responseModel.IsSuccess = false;
                    responseModel.Message = "Company Not deleted";
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
}
