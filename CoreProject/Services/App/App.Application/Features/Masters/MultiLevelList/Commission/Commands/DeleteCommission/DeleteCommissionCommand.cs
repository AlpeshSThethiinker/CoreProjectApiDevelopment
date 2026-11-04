using Lib.Core.Models;
using MediatR;

namespace App.Application.Features.Masters.MultiLevelList.Commission.Commands.DeleteCommission;

public class DeleteCommissionCommand : IRequest<ResponseModel>
{
    public DeleteCommissionCommand()
    {
        Id = new List<string>();
    }
    public List<string> Id { get; set; }

}
