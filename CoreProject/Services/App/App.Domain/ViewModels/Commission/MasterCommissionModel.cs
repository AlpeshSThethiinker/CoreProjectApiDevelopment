using Lib.Core.Domain;

namespace App.Domain.ViewModels.Commission;

public class MasterCommissionModel : EntityBase
{
    public string? CommissionRole { get; set; }
    public string? CommissionType { get; set; }
    public bool? ActiveBtn { get; set; }

}
