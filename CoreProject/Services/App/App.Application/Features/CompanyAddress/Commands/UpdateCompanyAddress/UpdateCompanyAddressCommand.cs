using Lib.Core.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace App.Application.Features.CompanyAddress.Commands.UpdateCompanyAddress
{
    public class UpdateCompanyAddressCommand : IRequest<ResponseModel>
    {
        public List<UpdateCompanyAddressCommands>? AddressDetail { get; set; }
    }
    public class UpdateCompanyAddressCommands : IRequest<ResponseModel>
    {
        public string? Id { get; set; }
        public string? CompanyId { get; set; }
        public string? AddressName { get; set; }
        public string? AddressTypeId { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? CountryId { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? Phone { get; set; }
        public string? AddressVAT { get; set; }
        public bool isDefault { get; set; }
        public bool? isActive { get; set; }

    }
}
