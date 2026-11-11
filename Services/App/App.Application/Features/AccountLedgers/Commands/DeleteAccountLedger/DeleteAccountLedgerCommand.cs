using Lib.Core.Models.Response;
using MediatR;

namespace App.Application.Features.AccountLedgers.Commands.DeleteAccountLedger;

public class DeleteAccountLedgerCommand : IRequest<ResponseModel>
{
    public long Id { get; set; }
}