using App.Persistence.Repositories.BaseRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace App.Persistence
{
    public static class PersistenceServices
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            // Register repositories
            _ = services.AddRepositories();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            // Register Base repository services
            _ = services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            return services;
        }
    }
}
