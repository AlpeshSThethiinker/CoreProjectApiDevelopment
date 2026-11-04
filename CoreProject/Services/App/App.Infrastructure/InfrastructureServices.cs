using App.Infrastructure.Services.Common.Dropdown;
using App.Infrastructure.Services.Company_Contact;
using App.Infrastructure.Services.Company_Details;
using App.Infrastructure.Services.CompanyAddress;
using App.Infrastructure.Services.Masters.MultiLevelList.Commission;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static App.Infrastructure.Services.Company.CompanyServices;


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
            _ = services.AddScoped<ICompanyService, CompanyService>();
            _ = services.AddScoped<ICompanyAddressService, CompanyAddressService>();
            _ = services.AddScoped<ICompanyDetailsService, CompanyDetailsService>();
            _ = services.AddScoped<ICompanyContactService, CompanyContactService>();
            _ = services.AddScoped<IDropdownService, DropdownService>();
            _ = services.AddScoped<ICommissionService, CommissionService>();
            _ = services.AddScoped<ICommissionMappingService, CommissionMappingService>();

            return services;
        }

    }
}
