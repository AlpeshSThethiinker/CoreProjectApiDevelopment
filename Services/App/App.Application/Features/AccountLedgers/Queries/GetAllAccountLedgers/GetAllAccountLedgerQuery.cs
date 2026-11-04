using Lib.Core.Models.Request;
using Lib.Core.Models.Response;
using MediatR;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace App.Application.Features.AccountLedgers.Queries.GetAllAccountLedger;

public class GetAllAccountLedgerQuery : PaginationRequestModel, IRequest<ResponseModel>
{

}

