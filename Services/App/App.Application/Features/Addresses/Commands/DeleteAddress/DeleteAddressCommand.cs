

using Lib.Core.Models.Response;
using MediatR;

namespace App.Application.Features.Addresses.Commands.DeleteAddress;

public class DeleteAddressCommand : IRequest<ResponseModel>
{
    public long Id { get; set; }
}