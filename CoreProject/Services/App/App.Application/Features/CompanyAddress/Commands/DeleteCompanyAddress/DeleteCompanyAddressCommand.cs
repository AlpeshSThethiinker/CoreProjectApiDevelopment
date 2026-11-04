using Lib.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Features.CompanyAddress.Commands.DeleteCompanyAddress
{
    public class DeleteCompanyAddressCommand : IRequest<ResponseModel>
    {
        public List<string>? Ids { get; set; }
    }
}
