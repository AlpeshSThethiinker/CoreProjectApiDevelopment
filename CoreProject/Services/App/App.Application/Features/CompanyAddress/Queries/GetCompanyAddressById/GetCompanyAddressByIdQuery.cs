using Lib.Core.Models;
using MediatR;

namespace App.Application.Features.CompanyAddress.Queries.GetCompanyAddressById
{
    public class GetCompanyAddressByIdQuery : IRequest<ResponseModel>
    {
        public string? CompanyId { get; set; }
    }
}
