using App.Domain.ViewModels.Commission;
using Lib.Core.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace App.Application.Features.Masters.MultiLevelList.Commission.Commands.UpdateCommission;

public class UpdateCommissionCommand : IRequest<ResponseModel>
{
    public UpdateMasterCommission MasterCommission { get; set; }
    public UpdateCommissionCommand()
    {
        MasterCommission = new UpdateMasterCommission();
    }
}
public class UpdateMasterCommission
{
    [Required]
    public string? Id { get; set; }
    [Required]
    public string? CommissionRole { get; set; }
    [Required]
    public string? CommissionType { get; set; }

    public bool? ActiveBtn { get; set; }

    public List<CommissionMappingModel>? CommissionMapping { get; set; }

}


