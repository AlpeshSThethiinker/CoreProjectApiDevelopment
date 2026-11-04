using Lib.Core.Models;
using MediatR;

namespace App.Application.Features.Company.Queries.GetCompanyById
{
    public class GetCompanyByIdQuery : IRequest<ResponseModel>
    {
        public string? CompanyId { get; set; }
    }
}
