using Lib.Core.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Features.AccountLedgers.Commands.DeleteAccountLedger;

public class DeleteAccountLedgerCommand : IRequest<ResponseModel>
{
    public long Id { get; set; }
}
