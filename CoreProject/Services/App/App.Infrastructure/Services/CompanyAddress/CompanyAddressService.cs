using App.Domain.Entities.Company;
using App.Infrastructure.Messages;
using App.Infrastructure.Services.RequestModels;
using App.Persistence.Repositories;

namespace App.Infrastructure.Services.CompanyAddress;

#region fields 
public interface ICompanyAddressService
{
    Task<List<CompanyAddressInformation>> GetAddressesByCompanyId(string companyId);
    Task<CompanyAddressInformation> InsertCompanyAddress(CompanyAddressInformation model);

    //Task<List<Student>> GetAsync(Expression<Func<Student, bool>> predicate);
    Task<CompanyAddressInformation> UpdateCompanyAddress(CompanyAddressInformation model);
    Task<CompanyAddressInformation> DeleteCompanyAddressById(DeleteCompanyAddressRequestModel request);

}
#endregion



public class CompanyAddressService : ICompanyAddressService
{

    #region ctor
    private readonly IBaseRepository<CompanyAddressInformation> _addressRepository;
    public CompanyAddressService(IBaseRepository<CompanyAddressInformation> addressRepository)
    {
        _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
    }
    #endregion

    #region deletecompanyaddress
    public async Task<CompanyAddressInformation> DeleteCompanyAddressById(DeleteCompanyAddressRequestModel request)
    {
        CompanyAddressInformation entity = new();
        try
        {
            var getData = await _addressRepository.GetByIdAsync(request.Id!);
            if (getData is not null)
            {
                getData.IsDeleted = true;
                getData.DeletedAt = DateTime.UtcNow;
                await _addressRepository.UpdateAsync(getData);
                return entity;
            }
            else
                throw new Exception(ResponseMessages.ExceptionOccured + ResponseMessages.NotFound);

        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region get addresses by company id
    public async Task<List<CompanyAddressInformation>> GetAddressesByCompanyId(string companyId)
    {
        try
        {
            List<CompanyAddressInformation>? entity = (await _addressRepository.GetAsync(x => x.CompanyId == companyId && !x.IsDeleted)).ToList();
            if (entity is null)
                return null!;

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region insertcompanyaddress
    public async Task<CompanyAddressInformation> InsertCompanyAddress(CompanyAddressInformation model)
    {
        try
        {
            CompanyAddressInformation entity = new();
            entity = await _addressRepository.AddAsync(model);
            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region updatecompanyaddress
    public async Task<CompanyAddressInformation> UpdateCompanyAddress(CompanyAddressInformation request)
    {
        CompanyAddressInformation? entity = null;
        try
        {
            if (string.IsNullOrEmpty(request.Id))
            {
                entity = new();
                entity = await InsertCompanyAddress(new()
                {
                    CompanyId = request.Id,
                    AddressName = request.AddressName,
                    AddressTypeId = request.AddressTypeId,
                    isDefault = request.isDefault,
                    Address1 = request.Address1,
                    Address2 = request.Address2,
                    City = request.City,
                    State = request.State,
                    PostalCode = request.PostalCode,
                    CountryId = request.CountryId,
                    Phone = request.Phone,
                    AddressVAT = request.AddressVAT,
                    isActive = request.isActive

                });
            }
            else if (!string.IsNullOrEmpty(request.Id))
            {
                entity = await _addressRepository.GetByIdAsync(request.Id);
                entity!.Id = request.Id;
                entity.CountryId = request.CountryId;
                entity.AddressName = request.AddressName;
                entity.AddressTypeId = request.AddressTypeId;
                entity.isDefault = request.isDefault;
                entity.Address1 = request.Address1;
                entity.Address2 = request.Address2;
                entity.City = request.City;
                entity.State = request.State;
                entity.PostalCode = request.PostalCode;
                entity.CountryId = request.CountryId;
                entity.Phone = request.Phone;
                entity.AddressVAT = request.AddressVAT;
                entity.isActive = request.isActive;
                await _addressRepository.UpdateAsync(entity);
            }
            return entity!;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion
}


