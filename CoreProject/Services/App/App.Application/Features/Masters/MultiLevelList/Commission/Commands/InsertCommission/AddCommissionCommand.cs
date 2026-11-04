using App.Domain.ViewModels.Commission;
using Lib.Core.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace App.Application.Features.Masters.MultiLevelList.Commission.Commands.InsertCommission;

public class AddCommissionCommand : IRequest<ResponseModel>
{
    public AddMasterCommission MasterCommission { get; set; }
    public AddCommissionCommand()
    {
        MasterCommission = new AddMasterCommission();
    }
}

public class AddMasterCommission
{
    public AddMasterCommission()
    {
        CommissionMapping = new List<CommissionMappingModel>();
    }
    public string? Id { get; set; }
    [Required]
    public string? CommissionRole { get; set; }
    [Required]
    public string? CommissionType { get; set; }

    public bool? ActiveBtn { get; set; }

    public List<CommissionMappingModel>? CommissionMapping { get; set; }

}