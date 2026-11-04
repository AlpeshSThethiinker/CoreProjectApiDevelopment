using Lib.Core.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace App.Application.Features.Masters.MultiLevelList.Commission.Commands.UpdateCommissionStatus;

public class UpdateCommissionStatusCommand : IRequest<ResponseModel>
{

    [Required]
    public string? Id { get; set; }
    [Required]
    public int UpdateStatusTypeId { get; set; }
    public bool Active { get; set; }
}

public enum UpdateStatusType
{
    UserCommission = 1,
    UserCommissionRange = 2
}
