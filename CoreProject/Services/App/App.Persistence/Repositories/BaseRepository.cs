using App.Persistence.DbContexts;
using Lib.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Persistence.Repositories;
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : EntityBase
{
    protected readonly AppDbContext _dbContext;

    public BaseRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    #region Read Operations
    public IQueryable<TEntity> GetAsQueryable(Expression<Func<TEntity, bool>>? predicate = null)
    {
        return predicate == null ? _dbContext.Set<TEntity>().AsQueryable<TEntity>() : _dbContext.Set<TEntity>().Where(predicate);
    }

    public async Task<IReadOnlyList<TEntity>> GetAllAsync()
    {
        return await _dbContext.Set<TEntity>().ToListAsync();
    }

    public async Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
    }

    public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbContext.Set<TEntity>().Where(predicate).CountAsync();
    }

    public async Task<IReadOnlyList<TEntity>> GetWithDescendingOrderAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbContext.Set<TEntity>().Where(predicate).OrderByDescending(s => s.CreatedAt).ToListAsync();
    }

    public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbContext.Set<TEntity>().Where(predicate).SingleAsync();
    }

    public async Task<TEntity?> GetByIdAsync(string id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id);
    }
    #endregion

    #region Write Operations
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
        _ = await _dbContext.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Mark a single record as Deleted in the database by updating IsDeleted property as TRUE.
    /// This does not delete the record permanently from database.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task MarkAsDeletedAsync(TEntity entity)
    {
        _dbContext.Entry(entity).Entity.IsDeleted = true;
        _dbContext.Entry(entity).State = EntityState.Modified;
        _ = await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Mark multiple records as Deleted in the database by updating IsDeleted property of each record as TRUE.
    /// This does not delete the records permanently from database.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public async Task MarkAsDeletedAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
        entities.ForEach(x =>
        {
            x.IsDeleted = true;
            _dbContext.Entry(x).State = EntityState.Modified;
        });

        _ = await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Delete a single record permanently from database
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteByIdAsync(string id)
    {
        TEntity? entity = await _dbContext.Set<TEntity>().FindAsync(id);
        if (entity == null) return;

        _dbContext.Set<TEntity>().Remove(entity);
        _ = await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Delete multiple records permanently from database using predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public async Task DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
        _dbContext.Set<TEntity>().RemoveRange(entities);
        _ = await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        _ = await _dbContext.SaveChangesAsync();
    }

    public async Task<TEntity> Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
        _ = await _dbContext.SaveChangesAsync();
        return entity;
    }
    #endregion
}
