//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace App.Application.Features.AccountLedger.Commands.AddAccountLedger
//{
//    internal class AddAccountLedgerCommand
//    {
//    }
//}

using Lib.Core.Models.Response;
using MediatR;

namespace App.Application.Features.AccountLedgers.Commands.AddAccountLedger;
public class AddAccountLedgerCommand : IRequest<ResponseModel>
{
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


