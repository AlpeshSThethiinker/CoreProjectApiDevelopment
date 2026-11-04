using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Lib.Core.Config.MediatR;

namespace App.Application
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Add MediatR services
            _ = services.AddMediatRServices(Assembly.GetExecutingAssembly());
            // Register the required hub context

            return services;
        }
    }
}
