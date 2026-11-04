using Lib.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.Core.Domain;

namespace App.Domain.Entities.AccountLedger
{
    //public class AccountLedger : EntityBase
    //{
    //}
    public class AccountLedger : EntityBase
    {
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
}
