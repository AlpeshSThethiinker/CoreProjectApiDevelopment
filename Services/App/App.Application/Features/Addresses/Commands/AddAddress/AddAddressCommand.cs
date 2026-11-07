using Lib.Core.Models.Response;
using App.Domain.ViewModels.Addresses;
using MediatR;

namespace App.Application.Features.Addresses.Commands.AddAddress;


public class AddAddressCommand : IRequest<ResponseModel>
{
    public long? AccountGroupId { get; set; }
    public string? Line1 { get; set; }
    public string? Line2 { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }
    public string? ZipCode { get; set; }

    //add address propertie
    public List<AddressVM> Addresses { get; set; } = new();

}