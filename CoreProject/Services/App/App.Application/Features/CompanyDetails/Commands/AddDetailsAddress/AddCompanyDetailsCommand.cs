using Lib.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Features.CompanyDetails.Commands.AddDetailsAddress
{
    public class AddCompanyDetailsCommand : IRequest<ResponseModel>
    {
        public string? Id { get; set; }
        public string? CompanyId { get; set; }
        public string? DandBNumber { get; set; }
        public string? IndustryId { get; set; } = string.Empty;
        public string? SICCode { get; set; }
        public string? NAICSCode { get; set; }
        public string? ScopeId { get; set; } = string.Empty;
        public string? EntityType { get; set; }
        public string? Employees { get; set; }
        public string? Revenue { get; set; }
        public string? ParentCompany { get; set; }
        public string? CurrencyId { get; set; } = string.Empty;
        public string? TaxId { get; set; }
        public string? VAT { get; set; }
        public string? ResellerId { get; set; }
        public DateTime? ResellerIdExpDate { get; set; }
        public string? Notes { get; set; }
    }
}
