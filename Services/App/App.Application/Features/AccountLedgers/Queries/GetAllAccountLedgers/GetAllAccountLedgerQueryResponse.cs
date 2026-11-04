using App.Application.Features.Products.Queries.GetAllProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Features.AccountLedgers.Queries.GetAllAccountLedger;
public class GetAllAccountLedgerQueryResponse
{
    public List<MasterAccountLedger> Data { get; set; }
    public GetAllAccountLedgerQueryResponse()
    {
        Data = new List<MasterAccountLedger>();
    }
}

public class MasterAccountLedger
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
