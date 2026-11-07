using Lib.Core.Models.Response;
using MediatR;

namespace App.Application.Features.Addresses.Commands.UpdateAddress;

public class UpdateAddressCommand : IRequest<ResponseModel>
{
    public long? Id { get; set; }
    public long? AccountGroupId { get; set; }
    public string? Line1 { get; set; }
    public string? Line2 { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }
    public string? ZipCode { get; set; }
}