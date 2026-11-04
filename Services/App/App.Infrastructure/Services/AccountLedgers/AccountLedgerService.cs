
using App.Infrastructure.Messages;
using App.Infrastructure.Services.AccountLedgers.ServiceModel;
using App.Persistence.DbContexts;
using App.Persistence.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using App.Domain.Entities.AccountLedger;
using App.Domain.ViewModels.AccountLedger;
//using App.Infrastructure.Services.AccountLedgers.ServiceModel;

namespace App.Infrastructure.Services.AccountLedgers;

public interface IAccountLedgerService
{
    Task<AccountLedger> Insert(AccountLedger request);
    Task<AccountLedger> Delete(long AccountLedgerId);
    Task<AccountLedger> Update(AccountLedger Data);
    Task<AccountLedger> GetAccountLedgerById(long AccountLedgerId);
    Task<List<AccountLedgerVM>> GetAll(GetAllAccountLedgerRequestModel request);
    Task<List<AccountLedger>> GetAsync(Expression<Func<AccountLedger, bool>> predicate);
}

public class AccountLedgerService : IAccountLedgerService
{
    private readonly IBaseRepository<AccountLedger> _accountLedgerRepository;
    private readonly AppDbContext _dbContext;

   public AccountLedgerService(IBaseRepository<AccountLedger> accountLedgerRepository, AppDbContext appDbContext)
   {
        _accountLedgerRepository = accountLedgerRepository ?? throw new ArgumentNullException(nameof(accountLedgerRepository));
        _dbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));

    }

    public async Task<AccountLedger> Delete(long AccountLedgerId)
    {
        try
        {
            AccountLedger? entity = await _accountLedgerRepository.GetByIdAsync(AccountLedgerId);
            entity.Deleted = true;
            await _accountLedgerRepository.UpdateAsync(entity);
            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ResponseMessages.ExceptionOccurred + ex.Message);
        }
    }

    public Task<AccountLedger> GetAccountLedgerById(long AccountLedgerId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<AccountLedgerVM>> GetAll(GetAllAccountLedgerRequestModel request)
    {
        try
        {
            var sql = $"Exec sp_GetAllAccountLedger " +
                $"@GetAll='{request.GetAll}', " +
                $"@page_size='{request.PageSize}', " +
                $"@page_num='{request.Page}', " +
                $"@SearchText='{request.SearchText ?? ""}'," +
                $"@fromDate='{request.FromDate}'," +
                $"@toDate='{request.ToDate}'," +
                $"@totalRows='{request.total_rows}'," +
                $"@totalPages='{request.total_pages}'," +
                $"@OrderBy='{(!string.IsNullOrEmpty(request.OrderBy) ? "" + request.OrderBy + "" : "id asc")}'";
            var value = await _dbContext.AccountLedgerVMs.FromSqlRaw(sql).ToListAsync();
            if (value == null) { throw new InvalidOperationException(" Value Not found."); }
            return value.ToList();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public async Task<List<AccountLedger>> GetAsync(Expression<Func<AccountLedger, bool>> predicate)
    {
        return await _dbContext.Set<AccountLedger>().Where(predicate).ToListAsync();
    }

    public async Task<AccountLedger> GetById(long AccountLedgerId)
    {
        try
        {
            AccountLedger? entity = await _accountLedgerRepository.GetByIdAsync(AccountLedgerId);
            if (entity is null)
                return null!;

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<AccountLedger> Insert(AccountLedger request)
    {
        try
        {
            AccountLedger entity = new();
            entity = await _accountLedgerRepository.AddAsync(request);
            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public async Task<AccountLedger> Update(AccountLedger Data)
    {
        try
        {
            await _accountLedgerRepository.UpdateAsync(Data);
            return Data;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
