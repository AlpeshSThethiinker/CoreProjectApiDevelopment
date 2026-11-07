using Lib.Core.Models.Response;
using MediatR;

namespace App.Application.Features.AccountLedgers.Queries.GetAccountLedgerById;

public class GetAccountLedgerByIdQuery : IRequest<ResponseModel>
{
        //public long? Id { get; set; }
        public long? AccountGroupId { get; set; }

}
