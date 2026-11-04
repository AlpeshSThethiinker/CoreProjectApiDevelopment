using App.Domain.Entities.Masters.MultiLevelList.Commission;
using System.ComponentModel.DataAnnotations;

namespace App.Application.Features.Masters.MultiLevelList.Commission.Queries.GetAllCommission;

public class GetAllCommissionQueryResponse
{
    public GetMasterCommission MasterCommission { get; set; }
    public GetAllCommissionQueryResponse()
    {
        MasterCommission = new GetMasterCommission();
    }
}
public class GetMasterCommission
{


    public string? Id { get; set; }
    [Required]
    public string? CommissionRole { get; set; }
    [Required]
    public string? CommissionType { get; set; }

    public bool? ActiveBtn { get; set; }

    public List<CommissionMapping>? CommissionMapping { get; set; }

}

