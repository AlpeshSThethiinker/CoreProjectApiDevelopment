using Lib.Core.Models;
using MediatR;

namespace App.Application.Features.Masters.MultiLevelList.Commission.Queries.GetByCommissionType;

public class GetByCommissionTypeQuery : IRequest<ResponseModel>
{
    public string? CommissionType { get; set; }
}
