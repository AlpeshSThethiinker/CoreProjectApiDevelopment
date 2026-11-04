using App.Application.Features.Company.Commands.DeleteCompany;
using App.Infrastructure.Messages;
using App.Infrastructure.Services.Company_Contact;
using App.Infrastructure.Services.Company_Details;
using App.Infrastructure.Services.CompanyAddress;
using Lib.Core.Models;
using MediatR;
using static App.Infrastructure.Services.Company.CompanyServices;

namespace App.Application.Features.CompanyAddress.Commands.DeleteCompanyAddress
{
    public class DeleteCompanyAddressCommandHandler : IRequestHandler<DeleteCompanyAddressCommand, ResponseModel>
    {
        #region fields
        public readonly ICompanyService _companyService;
        private readonly ICompanyAddressService _companyAddressService;
        private readonly ICompanyDetailsService _companyDetailsService;
        private readonly ICompanyContactService _companyContactService;
        #endregion

        #region ctor
        public DeleteCompanyAddressCommandHandler(ICompanyService companyService,
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
        public async Task<ResponseModel> Handle(DeleteCompanyAddressCommand request, CancellationToken cancellationToken)
        {
            ResponseModel responseModel = new();

            try
            {
                if (request.Ids!.Count != 0)
                {
                    List<string> Ids = [];
                    foreach (var id in request.Ids)
                    {
                        var deleteResponse = await _companyAddressService.DeleteCompanyAddressById(new()
                        {
                            Id = id
                        });
                        if (deleteResponse != null)
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
}
