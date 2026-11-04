using App.Infrastructure.Services.AccountLedgers;
using App.Infrastructure.Services.Products;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace App.Infrastructure
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Service contracts
            _ = services.AddServiceContracts();

            return services;
        }

        private static IServiceCollection AddServiceContracts(this IServiceCollection services)
        {
            //services.RegisterAssemblyPublicNonGenericClasses().AsPublicImplementedInterfaces();
            //services.RegisterAssemblyPublicNonGenericClasses().AsPublicImplementedInterfaces(ServiceLifetime.Transient);
            //_ = services.AddScoped<ICompanyService, CompanyService>();

            _ = services.AddScoped<IProductService, ProductService>();
            _ = services.AddScoped<IAccountLedgerService, AccountLedgerService>();

            return services;
        }

    }
}
