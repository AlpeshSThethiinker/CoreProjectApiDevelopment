using Lib.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Features.CompanyContacts.Queries.GetCompanyContactsById
{
    public class GetCompanyContactByIdQuery : IRequest<ResponseModel>
    {
        public string? CompanyId { get; set; }
    }
}
