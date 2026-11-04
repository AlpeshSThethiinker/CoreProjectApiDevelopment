using FluentValidation;
using Lib.Core.Config.MediatR.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Lib.Core.Config.MediatR;

public static class MediatRConfigurations
{
    public static IServiceCollection AddMediatRServices(this IServiceCollection services, Assembly executingAssembly)
    {
        var assembly = executingAssembly;

        // add Fluent Validations
        _ = services.AddValidatorsFromAssembly(assembly);

        // add MediatR support
        _ = services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
        _ = services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        _ = services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        return services;
    }
}
