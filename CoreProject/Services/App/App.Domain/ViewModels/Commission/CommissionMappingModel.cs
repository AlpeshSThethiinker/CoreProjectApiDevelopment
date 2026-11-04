using Lib.Core.Domain;

namespace App.Domain.ViewModels.Commission;

public class CommissionMappingModel : EntityBase
{
    public string? MappingId { get; set; }
    public int? FromRange { get; set; }
    public int? ToRange { get; set; }
    public string? Rate { get; set; }
    public bool? ActiveBtn { get; set; }
}
