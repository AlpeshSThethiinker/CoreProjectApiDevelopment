using App.Domain.Entities.Masters.MultiLevelList.Commission;
using App.Domain.ViewModels;
using App.Infrastructure.Messages;
using App.Infrastructure.Services.Masters.MultiLevelList.Commission.RequestModel;
using App.Persistence.DbContexts;
using App.Persistence.Repositories;
using Lib.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Infrastructure.Services.Masters.MultiLevelList.Commission;


#region Interface
public interface ICommissionMappingService
{
    Task<CommissionMapping> InsertCommissionMapping(CommissionMapping model);
    Task<CommissionMapping> DeleteCommissionMappingById(DeleteCommissionMappingRequestModel command);
    Task<List<CommissionMapping>> GetAsync(Expression<Func<CommissionMapping, bool>> predicate);
    Task<CommissionMapping> UpdateCommissionMappingData(CommissionMapping request);
    Task<CommissionMapping> InsertCommissionMappingData(CommissionMapping request);

    Task<List<CommissionMapping>> GetCommissionMappingById(string id);
    Task<CommissionMapping> UpdateUserCommissionMappingStatus(UpdateCommissionStatusModel request);
}
#endregion

public class CommissionMappingService : ICommissionMappingService
{
    #region fields
    private readonly IBaseRepository<CommissionMapping> _commissionMappingRepository;
    private readonly AppDbContext _appDbContext;
    #endregion

    #region ctor
    public CommissionMappingService(IBaseRepository<CommissionMapping> commissionMappingRepository, AppDbContext appDbContext)
    {
        _commissionMappingRepository = commissionMappingRepository ?? throw new ArgumentException(nameof(commissionMappingRepository));
        _appDbContext = appDbContext ?? throw new ArgumentException(nameof(appDbContext));
    }
    #endregion

    #region deleteCommissionById
    public async Task<CommissionMapping> DeleteCommissionMappingById(DeleteCommissionMappingRequestModel request)
    {
        CommissionMapping entity = new();
        try
        {
            var getData = await _commissionMappingRepository.GetByIdAsync(request.Id!);
            if (getData is not null)
            {
                getData.IsDeleted = true;
                getData.DeletedAt = DateTime.UtcNow;
                await _commissionMappingRepository.UpdateAsync(getData);
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

    #region getCommissionMappingById
    public async Task<List<CommissionMapping>> GetCommissionMappingById(string id)
    {
        try
        {
            List<CommissionMapping>? entity = (await _commissionMappingRepository.GetAsync(x => x.MappingId == id && !x.IsDeleted)).ToList();
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

    #region insertCommissionMapping
    public async Task<CommissionMapping> InsertCommissionMapping(CommissionMapping model)
    {
        try
        {
            CommissionMapping entity = new();
            entity = await _commissionMappingRepository.AddAsync(model);
            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region update Commission Mapping Data
    public async Task<CommissionMapping> UpdateCommissionMappingData(CommissionMapping request)
    {
        try
        {

            var result = await _commissionMappingRepository.Update(request);
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region insertCommissionMappingData
    public async Task<CommissionMapping> InsertCommissionMappingData(CommissionMapping request)
    {
        try
        {
            var entity = await _commissionMappingRepository.AddAsync(request);
            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region getAsyncCommissionMapping
    public async Task<List<CommissionMapping>> GetAsync(Expression<Func<CommissionMapping, bool>> predicate)
    {
        return await _appDbContext.Set<CommissionMapping>().Where(predicate).ToListAsync();
    }
    #endregion

    #region Update Commission Mapping Status
    public async Task<CommissionMapping> UpdateUserCommissionMappingStatus(UpdateCommissionStatusModel request)
    {
        ResponseModel response = new();
        CommissionMapping entity = new();
        try
        {
            var getdata = (await _commissionMappingRepository.GetAsync(x => x.MappingId == request.Id && !x.IsDeleted)).ToList();
            if (getdata != null)
            {
                foreach (var item in getdata)
                {
                    item.ActiveBtn = request.IsActive;
                    item.UpdatedAt = DateTime.Now;
                    entity = await _commissionMappingRepository.Update(item);
                    string userCommissionStatus = request.IsActive ? "Active" : "In-Active";
                }
            }
            response.Response = request;
            response.IsSuccess = true;
            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ResponseMessages.ExceptionOccured + ex.Message);
        }
    }
    #endregion

}


