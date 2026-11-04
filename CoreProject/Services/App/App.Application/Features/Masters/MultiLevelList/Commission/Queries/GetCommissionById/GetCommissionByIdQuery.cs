using Lib.Core.Models;
using MediatR;

namespace App.Application.Features.Masters.MultiLevelList.Commission.Queries.GetCommissionById;

public class GetCommissionByIdQuery : IRequest<ResponseModel>
{
    public string? MappingId { get; set; }
}
