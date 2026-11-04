using App.Domain.Entities.NewProduct;
using App.Domain.ViewModels.Products;
using App.Infrastructure.Messages;
using App.Infrastructure.Services.Products.ServiceModel;
using App.Persistence.DbContexts;
using App.Persistence.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Infrastructure.Services.Products;

public interface IProductService
{
    Task<Product> Insert(Product request);
    Task<Product> Delete(long Id);
    Task<Product> Update(Product data);
    Task<Product> GetById(long Id);
    Task<List<ProductVM>> GetAll(GetAllProductRequestModel request);
    Task<List<Product>> GetAsync(Expression<Func<Product, bool>> predicate);
}
public class ProductService : IProductService
{
    #region field
    private readonly IBaseRepository<Product> _productRepository;
    private readonly AppDbContext _dbContext;
    #endregion

    #region ctor
    public ProductService(IBaseRepository<Product> productRepository,
                          AppDbContext appDbContext)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _dbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
    }
    #endregion

    #region Delete
    public async Task<Product> Delete(long Id)
    {
        try
        {
            Product? entity = await _productRepository.GetByIdAsync(Id);
            entity.Deleted = true;
            await _productRepository.UpdateAsync(entity);

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ResponseMessages.ExceptionOccurred + ex.Message);
        }
    }
    #endregion

    #region Getall
    public async Task<List<ProductVM>> GetAll(GetAllProductRequestModel request)
    {
        try
        {
            var sql = $"Exec sp_GetAllProduct " +
                $"@GetAll='{request.GetAll}', " +
                $"@page_size='{request.PageSize}', " +
                $"@page_num='{request.Page}', " +
                $"@SearchText='{request.SearchText ?? ""}'," +
                $"@fromDate='{request.FromDate}'," +
                $"@toDate='{request.ToDate}'," +
                $"@totalRows='{request.total_rows}'," +
                $"@totalPages='{request.total_pages}'," +
                $"@OrderBy='{(!string.IsNullOrEmpty(request.OrderBy) ? "" + request.OrderBy + "" : "id asc")}'";
            var value = await _dbContext.ProductVMs.FromSqlRaw(sql).ToListAsync();
            if (value == null) { throw new InvalidOperationException(" Value Not found."); }
            return value.ToList();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region getasync
    public async Task<List<Product>> GetAsync(Expression<Func<Product, bool>> predicate)
    {
        return await _dbContext.Set<Product>().Where(predicate).ToListAsync();
    }
    #endregion

    #region getbyid
    public async Task<Product> GetById(long Id)
    {
        try
        {
            Product? entity = await _productRepository.GetByIdAsync(Id);
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

    #region Insert
    public async Task<Product> Insert(Product request)
    {
        try
        {
            Product entity = new();
            entity = await _productRepository.AddAsync(request);
            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region update
    public async Task<Product> Update(Product data)
    {
        try
        {
            await _productRepository.UpdateAsync(data);
            return data;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

}
