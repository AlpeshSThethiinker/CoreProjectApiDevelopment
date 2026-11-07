using App.Domain.Entities.AccountLedger;
using App.Domain.Entities.NewProduct;
using App.Domain.ViewModels.AccountLedger;
using App.Domain.ViewModels.Products;
using Microsoft.EntityFrameworkCore;
using App.Domain.Entities.Addresses;
using App.Domain.ViewModels.Addresses;
//using App.Domain.ViewModels.AccountLedgers;

namespace App.Persistence.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    #region entity Tables
    public DbSet<Product> Products { get; set; }
    public DbSet<AccountLedger> AccountLedgers { get; set; }
    public DbSet<Address> Addresses { get; set; }

    #endregion



    public DbSet<ProductVM> ProductVMs { get; set; }
    public DbSet<AccountLedgerVM> AccountLedgerVMs { get; set; }
    public DbSet<AddressVM> AddressVM { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductVM>().HasNoKey().ToView(nameof(ProductVM));
        modelBuilder.Entity<AccountLedgerVM>().HasNoKey().ToView(nameof(AccountLedgerVM));
        modelBuilder.Entity<AddressVM>().HasNoKey().ToView(nameof(AddressVM));
        //modelBuilder.Entity<AddressVM>().HasNoKey().ToView(nameof(AddressVM));
    }
}
