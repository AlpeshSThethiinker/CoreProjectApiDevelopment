
using App.Infrastructure.Services.Addresses.ServiceModel;
using App.Persistence.DbContexts;
using App.Persistence.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using App.Domain.Entities.Addresses;
using App.Domain.ViewModels.Addresses;
using AddressEntity = App.Domain.Entities.Addresses.Address;


namespace App.Infrastructure.Services.Addresses;

public interface IAddressService
{
    Task<AddressEntity> Insert(AddressEntity request);
    Task<AddressEntity> Delete(long Id);
    Task<AddressEntity> Update(AddressEntity Data);
    Task<AddressEntity> GetAddressById(long Id);
    //Task<List<AddressEntity>> GetAll(GetAllAddressRequestModel request);
    //Task<List<AddressEntity>> GetAll(GetAllAddressRequestModel request);
    Task<List<AddressVM>> GetAll(GetAllAddressRequestModel request);
    Task<List<AddressEntity>> GetAsync(Expression<Func<AddressEntity, bool>> predicate);

}

public class AddressService : IAddressService
{
    private readonly IBaseRepository<AddressEntity> _addressRepository;
    private readonly AppDbContext _dbContext;
    public AddressService(IBaseRepository<AddressEntity> addressRepositry, AppDbContext appDbContext)
    {
        _addressRepository = addressRepositry ?? throw new ArgumentNullException(nameof(addressRepositry));
        _dbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
    }


    public async Task<AddressEntity> GetAddressById(long Id)
    {
        var entity = await _addressRepository.GetByIdAsync(Id);
        if(entity == null)
            throw new Exception("Address record not found");
        return entity;
    }

    public async Task<List<AddressVM>> GetAll(GetAllAddressRequestModel request)
    {
        try
        {
            var orderBy = string.IsNullOrWhiteSpace(request.OrderBy)
                ? "Id ASC"
                : request.OrderBy;
            var result = await _dbContext.AddressVM
               .FromSqlInterpolated($@"
                    EXEC dbo.sp_GetAllAddress
                        @GetAll = {request.GetAll},
                        @page_size = {request.PageSize},
                        @page_num = {request.Page},
                        @totalRows = {request.total_rows},
                        @totalPages = {request.total_pages},
                        @SearchText = {request.SearchText ?? ""},
                        @OrderBy = {orderBy},
                        @fromDate = {request.FromDate ?? ""},
                        @toDate = {request.ToDate ?? ""}
                ")
               .AsNoTracking()
               .ToListAsync();

            return result;
        }
        catch(Exception ex)
        {
            throw new Exception("Error while fetching recordes: " + ex.Message, ex);

        }
    }
    public async Task<List<AddressEntity>> GetAsync(Expression<Func<AddressEntity, bool>> predicate)
    {
        return await _dbContext.Set<AddressEntity>().Where(predicate).ToListAsync();
    }

    public async Task<AddressEntity> Delete(long Id)
    {
        try
        {
            var entity = await _addressRepository.GetByIdAsync(Id);
            if (entity == null)
                throw new Exception("Address record not found");
            entity.Deleted = true;
            await _addressRepository.UpdateAsync(entity);
            return entity;
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task<AddressEntity> Insert(AddressEntity request)
    {
        return await _addressRepository.AddAsync(request);
    }
    public async Task<AddressEntity> Update(AddressEntity Data)
    {
        await _addressRepository.UpdateAsync(Data);
        return Data;
    }

    //Task<List<Address>> IAddressService.GetAll(GetAllAddressRequestModel request)
    //{
    //    throw new NotImplementedException();
    //}
}

