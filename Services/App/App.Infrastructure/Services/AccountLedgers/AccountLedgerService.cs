
using App.Infrastructure.Messages;
using App.Infrastructure.Services.AccountLedgers.ServiceModel;
using App.Persistence.DbContexts;
using App.Persistence.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using App.Domain.Entities.AccountLedger;
using App.Domain.ViewModels.AccountLedger;

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
        _accountLedgerRepository = accountLedgerRepository
            ?? throw new ArgumentNullException(nameof(accountLedgerRepository));

        _dbContext = appDbContext
            ?? throw new ArgumentNullException(nameof(appDbContext));
    }

    public async Task<AccountLedger> GetAccountLedgerById(long AccountLedgerId)
    {
        var entity = await _accountLedgerRepository.GetByIdAsync(AccountLedgerId);

        return entity!;
    }

    public async Task<List<AccountLedgerVM>> GetAll(GetAllAccountLedgerRequestModel request)
    {
        try
        {
            var orderBy = string.IsNullOrWhiteSpace(request.OrderBy)
                ? "AccountGroupId ASC"
                : request.OrderBy;

            var result = await _dbContext.AccountLedgerVMs
                .FromSqlInterpolated($@"
                    EXEC dbo.sp_GetAllAccountLedger
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
        catch (Exception ex)
        {
            throw new Exception(
                "Error while fetching AccountLedger records: " + ex.Message, ex);
        }
    }

    public async Task<List<AccountLedger>> GetAsync(
        Expression<Func<AccountLedger, bool>> predicate)
    {
        return await _dbContext.Set<AccountLedger>()
            .Where(predicate)
            .ToListAsync();
    }

    public async Task<AccountLedger> Delete(long AccountLedgerId)
    {
        try
        {
            var entity = await _accountLedgerRepository.GetByIdAsync(AccountLedgerId);

            if (entity == null)
                throw new Exception("AccountLedger record not found.");

            entity.Deleted = true;

            await _accountLedgerRepository.UpdateAsync(entity);

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task<AccountLedger> Insert(AccountLedger request)
    {
        return await _accountLedgerRepository.AddAsync(request);
    }

    public async Task<AccountLedger> Update(AccountLedger Data)
    {
        await _accountLedgerRepository.UpdateAsync(Data);
        return Data;
    }
}
