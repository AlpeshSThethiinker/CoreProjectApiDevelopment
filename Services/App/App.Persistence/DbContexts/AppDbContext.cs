using App.Domain.Entities.AccountLedger;
using App.Domain.Entities.NewProduct;
using App.Domain.ViewModels.Products;
using Microsoft.EntityFrameworkCore;
using App.Domain.ViewModels.AccountLedger;

namespace App.Persistence.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    #region entity Tables
    public DbSet<Product> Products { get; set; }
    public DbSet<AccountLedger> AccountLedgers { get; set; }

    #endregion



    public DbSet<ProductVM> ProductVMs { get; set; }
    public DbSet<AccountLedgerVM> AccountLedgerVMs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductVM>().HasNoKey().ToView(nameof(ProductVM));
        modelBuilder.Entity<AccountLedgerVM>().HasNoKey().ToView(nameof(AccountLedgerVM));
    }
}
