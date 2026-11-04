using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.Core.Models.Response;

namespace App.Application.Features.AccountLedgers.Commands.UpdateAccountLedger;

public class UpdateAccountLedgerCommand : IRequest<ResponseModel>
{
    public long? AccountGroupId { get; set; }
    public string? AccountNo { get; set; }
    public int? LedgerTypeId { get; set; }
    public string? LedgerName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Mobile { get; set; }
    public string? MobileNoDialCode { get; set; }
    public string? Email { get; set; }
}
