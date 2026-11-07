using App.Infrastructure.Services.Products;
using App.Infrastructure.Services.Addresses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using App.Infrastructure.Services.AccountLedgers;


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
            _ = services.AddScoped<IAddressService, AddressService>();

            return services;
        }

    }
}
