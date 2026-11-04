using App.Domain.Entities.Company;
using App.Domain.Entities.Masters.MultiLevelList.Commission;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        #region entity Tables
        public DbSet<CompanyAddressInformation> CompanyAddresses { get; set; }
        public DbSet<Companies> Companies { get; set; }
        public DbSet<CompanyContactInformation> CompanyContacts { get; set; }
        public DbSet<CompanyDetailsInformation> CompanyDetails { get; set; }
        public DbSet<MasterCommission> MasterCommissions { get; set; }
        public DbSet<CommissionMapping> CommissionMappings { get; set; }

        #endregion
    }

}
