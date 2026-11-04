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

#region interface
public interface ICommissionService
{
    Task<MasterCommission> InsertCommission(MasterCommission model);
    Task<MasterCommission> DeleteCommissionById(DeleteCommissionRequestModel command);
    Task<List<MasterCommission>> GetAsync(Expression<Func<MasterCommission, bool>> predicate);
    Task<MasterCommission> UpdateCommissionData(MasterCommission request);
    Task<MasterCommission> InsertCommissionData(MasterCommission request);
    Task<List<MasterCommission>> GetAllCommission();
    Task<MasterCommission> GetCommissionById(string id);
    Task<MasterCommission> UpdateUserCommissionStatus(UpdateCommissionStatusModel request);
    //Task<List<MasterCommission>> GetByComissionType(MasterCommission request);
}
#endregion

public class CommissionService : ICommissionService
{
    #region fields
    private readonly IBaseRepository<MasterCommission> _masterCommissionRepository;
    private readonly AppDbContext _appDbContext;
    private readonly IBaseRepository<CommissionMapping> _commissionMappingRepository;
    #endregion

    #region ctor
    public CommissionService(IBaseRepository<MasterCommission> masterCommissionRepository, IBaseRepository<CommissionMapping> commissionMappingRepository,
        AppDbContext appDbContext)
    {
        _masterCommissionRepository = masterCommissionRepository ?? throw new ArgumentException(nameof(masterCommissionRepository));
        _appDbContext = appDbContext ?? throw new ArgumentException(nameof(appDbContext));
        _commissionMappingRepository = commissionMappingRepository ?? throw new ArgumentException(nameof(commissionMappingRepository));
    }
    #endregion

    #region deleteCommissionById
    public async Task<MasterCommission> DeleteCommissionById(DeleteCommissionRequestModel request)
    {

        MasterCommission entity = new();
        try
        {
            var getData = await _masterCommissionRepository.GetByIdAsync(request.Id!);
            if (getData is not null)
            {
                getData.IsDeleted = true;
                getData.DeletedAt = DateTime.UtcNow;
                await _masterCommissionRepository.UpdateAsync(getData);
                var getMapData = (await _commissionMappingRepository.GetAsync(x => x.MappingId == request.Id)).ToList();
                if (getMapData is not null)
                {
                    foreach (var map in getMapData)
                    {
                        map.IsDeleted = true;
                        map.DeletedAt = DateTime.UtcNow;
                        await _commissionMappingRepository.UpdateAsync(map);
                    }
                }
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

    #region getCommissionById
    public async Task<MasterCommission> GetCommissionById(string id)
    {
        try
        {
            MasterCommission? entity = await _masterCommissionRepository.GetByIdAsync(id);
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

    #region insertCommission
    public async Task<MasterCommission> InsertCommission(MasterCommission model)
    {
        try
        {
            MasterCommission entity = new();
            entity = await _masterCommissionRepository.AddAsync(model);
            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region updateCommissionData
    public async Task<MasterCommission> UpdateCommissionData(MasterCommission request)
    {
        try
        {
            var entity = await _masterCommissionRepository.Update(request);
            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region insertCommissionData
    public async Task<MasterCommission> InsertCommissionData(MasterCommission request)
    {
        try
        {
            var entity = await _masterCommissionRepository.AddAsync(request);
            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region getAsyncCommission
    public async Task<List<MasterCommission>> GetAsync(Expression<Func<MasterCommission, bool>> predicate)
    {
        return await _appDbContext.Set<MasterCommission>().Where(predicate).ToListAsync();
    }
    #endregion

    #region get all Commission Info
    public async Task<List<MasterCommission>> GetAllCommission()
    {
        try
        {
            var GetAll = await _masterCommissionRepository.GetAllAsync();
            return GetAll.ToList();
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region Update Commission Status
    public async Task<MasterCommission> UpdateUserCommissionStatus(UpdateCommissionStatusModel request)
    {
        ResponseModel response = new();
        MasterCommission entity = new();
        try
        {
            var getData = await _masterCommissionRepository.GetByIdAsync(request.Id!);
            if (getData is null)
                throw new Exception(ResponseMessages.ExceptionOccured + ResponseMessages.NotFound);
            getData.ActiveBtn = request.IsActive;
            getData.UpdatedAt = DateTime.UtcNow;
            entity = await _masterCommissionRepository.Update(getData);
            string userCommissionStatus = request.IsActive ? "Active" : "In-Active";

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
