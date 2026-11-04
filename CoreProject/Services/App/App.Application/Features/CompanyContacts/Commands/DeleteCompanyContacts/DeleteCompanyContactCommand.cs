using Lib.Core.Models;
using MediatR;

namespace App.Application.Features.CompanyContacts.Commands.DeleteCompanyContacts;
public class DeleteCompanyContactCommand : IRequest<ResponseModel>
{
    public string? Id { get; set; }
}

