using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lib.Core.Domain
{
    public class EntityBase : IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; } //AUTO Increament
        public long? FinancialYearId { get; set; } //B
        public long? BranchId { get; set; } //B
        public long? CounterId { get; set; } //B TODO logical implementation is pending
        public long? CreatedBy { get; set; } //B
        public long? ModifiedBy { get; set; } //B
        public long? DeletedBy { get; set; } //B
        public bool? Active { get; set; }
        public bool Deleted { get; set; }
        public DateTime? CreatedAt { get; set; } //B
        public DateTime? ModifiedAt { get; set; } //B
        public DateTime? DeletedAt { get; set; } //B
    }
}
