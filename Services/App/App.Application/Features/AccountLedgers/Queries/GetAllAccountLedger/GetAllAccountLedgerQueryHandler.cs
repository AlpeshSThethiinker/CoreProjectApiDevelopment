
using App.Infrastructure.Messages;
using Lib.Core.Models.Response;
using App.Infrastructure.Helpers;
using MediatR;
using App.Infrastructure.Services.AccountLedgers;
//add name space address in the accountledger queryhandler
using App.Infrastructure.Services.Addresses;
using App.Domain.ViewModels.Addresses;

namespace App.Application.Features.AccountLedgers.Queries.GetAllAccountLedger;

public class GetAllAccountLedgerQueryHandler
    : IRequestHandler<GetAllAccountLedgerQuery, ResponseModel>
{
    private readonly IAccountLedgerService _accountLedgerService;

    //add address service in the private filed
    private readonly IAddressService _addressService;


    //that is the constructor this is alos update for the address se
    public GetAllAccountLedgerQueryHandler(IAccountLedgerService accountLedgerService, IAddressService addressService)
    {
        _accountLedgerService = accountLedgerService ?? throw new ArgumentNullException(nameof(accountLedgerService));
        _addressService = addressService ?? throw new ArgumentNullException(nameof(addressService));
    }

    public async Task<ResponseModel> Handle(GetAllAccountLedgerQuery request, CancellationToken cancellationToken)
    {
        ResponseModel response = new();

        try
        {
            string orderBy = !string.IsNullOrWhiteSpace(request.OrderBy) ? request.OrderBy : !string.IsNullOrWhiteSpace(request.SortColumn) ? $"{request.SortColumn} {request.SortDirection}" : "AccountGroupId ASC";

            var getAllAccountLedger =
                await _accountLedgerService.GetAll(new()
                {
                    GetAll = request.GetAll,
                    PageSize = request.PageSize,
                    Page = request.Page,
                    SearchText = request.SearchText,
                    OrderBy = orderBy,
                    SortColumn = request.SortColumn,
                    SortDirection = request.SortDirection,
                    filters = request.filters,
                    sortList = request.sortList,
                    FromDate = request.FromDate,
                    ToDate = request.ToDate
                });

            if (getAllAccountLedger.Count == 0)
            {
                response.StatusCode = (int)HttpStatusCode.NoContent;
                response.Errors.Add(ResponseMessages.NotFound);
                return response;
            }

            List<MasterAccountLedger> model = new();
            // Step 2: Collect AccountLedger IDs
            var accountLedgerids = getAllAccountLedger.Select(x => x.AccountGroupId).Where(id => id.HasValue).Select(id => id!.Value).ToList();
            // Step 3: Fetch matching Addresses
            var addresses = await _addressService.GetAsync(x => x.AccountGroupId.HasValue && accountLedgerids.Contains(x.AccountGroupId.Value) && !x.Deleted);

            foreach (var accountledger in getAllAccountLedger)
            {
                MasterAccountLedger vm = new();
                accountledger.CopyPropertiesTo(vm);
                vm.Addresses = addresses.Where(x => x.AccountGroupId == accountledger.AccountGroupId).Select(x => new AddressVM
                {
                    Id = x.Id,
                    AccountGroupId = x.AccountGroupId,
                    Line1 = x.Line1,
                    Line2 = x.Line2,
                    Country = x.Country,
                    State = x.State,
                    City = x.City,
                    ZipCode = x.ZipCode
                }).ToList();

                model.Add(vm);
            }

            GetAllAccountLedgerQueryResponse queryResponse = new()
            {
                Data = model
            };

            response.setSuccessResponse(queryResponse);
            return response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}

