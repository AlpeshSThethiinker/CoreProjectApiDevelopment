using App.Infrastructure.Messages;
using App.Infrastructure.Services.CompanyAddress;
using Lib.Core.Models;
using MediatR;

namespace App.Application.Features.CompanyAddress.Queries.GetCompanyAddressById;
public class GetCompanyAddressByIdQueryHandler : IRequestHandler<GetCompanyAddressByIdQuery, ResponseModel>
{

    #region fields
    private readonly ICompanyAddressService _companyAddressService;
    #endregion

    #region ctor
    public GetCompanyAddressByIdQueryHandler(ICompanyAddressService companyAddressService)
    {
        _companyAddressService = companyAddressService ?? throw new ArgumentNullException(nameof(companyAddressService));
    }
    #endregion

    #region get company Address by  company id
    public async Task<ResponseModel> Handle(GetCompanyAddressByIdQuery request, CancellationToken cancellationToken)
    {
        ResponseModel response = new();
        try
        {
            List<GetCompanyAddressByIdQueryResponse> commandResponse = new();
            var getResponse = await _companyAddressService.GetAddressesByCompanyId(request.CompanyId!);

            if (getResponse != null)
            {
                foreach (var item in getResponse)
                {
                    var assignedData = new GetCompanyAddressByIdQueryResponse()
                    {
                        CompanyId = item.CompanyId,
                        AddressName = item.AddressName,
                        AddressTypeId = item.AddressTypeId,
                        isDefault = item.isDefault,
                        Address1 = item.Address1,
                        Address2 = item.Address2,
                        City = item.City,
                        State = item.State,
                        PostalCode = item.PostalCode,
                        CountryId = item.CountryId,
                        Phone = item.Phone,
                        AddressVAT = item.AddressVAT,
                        isActive = item.isActive
                    };
                    commandResponse.Add(assignedData);
                }

                response.IsSuccess = true;
                response.Message = "Company Address Found";
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Company Address Not found";
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
