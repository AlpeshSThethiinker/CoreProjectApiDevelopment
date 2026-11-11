using Lib.Core.Models.Response;
using MediatR;
using App.Domain.ViewModels.Addresses;

namespace App.Application.Features.AccountLedgers.Queries.GetAccountLedgerById;

public class GetAccountLedgerByIdQuery : IRequest<ResponseModel>
{
        public long? AccountGroupId { get; set; }

}
