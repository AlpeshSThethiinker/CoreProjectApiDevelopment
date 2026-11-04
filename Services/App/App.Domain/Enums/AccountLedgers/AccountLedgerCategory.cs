using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Enums.AccountLedgers;

public enum AccountLedgerCategory
{
    [Description("Savings")]
    Savings = 1,
    [Description("Current")]
    Current = 2,
    [Description("Business")]
    Business = 3,
    [Description("Daily")]
    Daily = 4,
    [Description("Online")]
    Online = 5


}
