using Lib.Core.Domain;
using System.Linq.Expressions;

namespace App.Persistence.Repositories.BaseRepositories;

public interface IBaseRepository<TEntity> where TEntity : EntityBase
{
    #region Read Operations
    Task<IReadOnlyList<TEntity>> GetAllAsync();
    IQueryable<TEntity> GetAsQueryable(Expression<Func<TEntity, bool>>? predicate = null);
    Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> GetByIdAsync(long id);
    Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IReadOnlyList<TEntity>> GetWithDescendingOrderAsync(Expression<Func<TEntity, bool>> predicate);
    #endregion

    #region Write Operations
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> Update(TEntity entity);
    Task DeleteByIdAsync(long id);
    Task DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate);
    Task MarkAsDeletedAsync(Expression<Func<TEntity, bool>> predicate);
    Task MarkAsDeletedAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    #endregion
}
