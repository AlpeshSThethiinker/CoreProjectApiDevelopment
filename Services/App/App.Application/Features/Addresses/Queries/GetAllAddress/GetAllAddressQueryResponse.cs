

namespace App.Application.Features.Addresses.Queries.GetAllAddress;

public class GetAllAddressQueryResponse
{
    public List<MasterAddress> Data { get; set; }
    public GetAllAddressQueryResponse()
    {
        Data = new List<MasterAddress>();
    }
}

public class MasterAddress
{
    public long? Id { get; set; }
    public string? Line1 { get; set; }
    public string? Line2 { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }
    public string? ZipCode { get; set; }

}