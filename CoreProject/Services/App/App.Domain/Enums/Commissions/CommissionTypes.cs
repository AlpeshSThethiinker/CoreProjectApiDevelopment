using System.ComponentModel;

namespace App.Domain.Enums.Commissions
{
    public enum CommissionTypes
    {
        [Description("Sale Commission")]
        SaleCommission = 1,

        [Description("Purchase Commission")]
        PurchaseCommission = 2,
    }
}
