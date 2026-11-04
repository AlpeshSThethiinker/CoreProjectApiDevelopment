using Lib.Core.Domain;
using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities.Masters.MultiLevelList.Commission;

public class MasterCommission : EntityBase
{

    public string? CommissionRole { get; set; }
    [Required]
    public string? CommissionType { get; set; }

    public bool? ActiveBtn { get; set; }

}
