using Lib.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Features.CompanyDetails.Queries.GetCompanyDetailsById
{
    public class GetCompanyDetailsByIdQuery : IRequest<ResponseModel>
    {
        public string? CompanyId { get; set; }
    }
}
