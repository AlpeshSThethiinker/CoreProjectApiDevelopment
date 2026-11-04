using Lib.Core.Domain;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities.Masters.MultiLevelList.Commission;

public class CommissionMapping : EntityBase
{

    public string? MappingId { get; set; }

    [Required]
    public int? FromRange { get; set; }
    [Required]
    public int? ToRange { get; set; }
    [Required]
    public string? Rate { get; set; }
    public bool? ActiveBtn { get; set; }

    public virtual MasterCommission? MasterCommission { get; set; }

}
