using App.Domain.Entities.Masters.MultiLevelList.Commission;
using System.ComponentModel.DataAnnotations;

namespace App.Application.Features.Masters.MultiLevelList.Commission.Queries.GetByCommissionType;

public class GetByCommissionTypeQueryResponse
{
    public AddMasterCommission MasterCommission { get; set; }
    public GetByCommissionTypeQueryResponse()
    {
        MasterCommission = new AddMasterCommission();
    }
}
public class AddMasterCommission
{
    public string? Id { get; set; }
    [Required]
    public string? CommissionRole { get; set; }
    [Required]
    public string? CommissionType { get; set; }

    public bool? ActiveBtn { get; set; }

    public List<CommissionMapping>? CommissionMapping { get; set; }

}

