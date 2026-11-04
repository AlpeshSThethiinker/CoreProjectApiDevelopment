using App.Infrastructure.Messages;
using Lib.Core.Models.Response;
using App.Infrastructure.Helpers;

using MediatR;
using App.Infrastructure.Services.AccountLedgers;

namespace App.Application.Features.AccountLedgers.Queries.GetAllAccountLedger;    
public class GetAllAccountLedgerQueryHandler : IRequestHandler<GetAllAccountLedgerQuery, ResponseModel>
{
    private readonly IAccountLedgerService _accountLedgerService; 
    public GetAllAccountLedgerQueryHandler(IAccountLedgerService accountLedgerService)
    {
        _accountLedgerService = accountLedgerService ?? throw new ArgumentNullException(nameof(accountLedgerService));
    }

    public async Task<ResponseModel> Handle(GetAllAccountLedgerQuery request, CancellationToken cancellationToken)
    {
        ResponseModel response = new();
        try
        {
            var getAllAccountLedger = await _accountLedgerService.GetAll(new()
            {
                GetAll = request.GetAll,
                PageSize = request.PageSize,
                Page = request.Page,
                SearchText = request.SearchText,
                SortColumn = request.SortColumn,
                SortDirection = request.SortDirection,
                filters = request.filters,
                sortList = request.sortList,
                FromDate = request.FromDate,
                ToDate = request.ToDate

            });
            if(getAllAccountLedger.Count == 0)
            {
                response.StatusCode = (int)HttpStatusCode.NoContent;
                response.Errors.Add(ResponseMessages.NotFound);
                return response;
            }
            List<MasterAccountLedger> model = new();
            foreach(var accountledger in getAllAccountLedger)
            {
                MasterAccountLedger vm = new();
                accountledger.CopyPropertiesTo(vm);
                model.Add(vm);
            }
            GetAllAccountLedgerQueryResponse queryResponse = new()
            {
                Data = model
            };
            response.setSuccessResponse(queryResponse);
            return response;
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

