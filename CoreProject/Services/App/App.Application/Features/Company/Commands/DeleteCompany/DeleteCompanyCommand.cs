using Lib.Core.Models;
using MediatR;

namespace App.Application.Features.Company.Commands.DeleteCompany
{
    public class DeleteCompanyCommand : IRequest<ResponseModel>
    {
        public DeleteCompanyCommand()
        {
            Id = new List<string>();
        }
        public List<string> Id { get; set; }
    }
}
