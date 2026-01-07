using Microsoft.Extensions.DependencyInjection;
using NetCloak.Application.Interfaces.Infrastructure;
using NetCloak.Infrastructure.Services;

namespace NetCloak.Infrastructure;

/// <summary>
/// Registration of infrastructure services.
/// </summary>
public static class InfrastructureServiceConfigurator
{
    /// <summary>
    /// Definition of service sets that are being used by Infrastructure project.
    /// </summary>
    /// <param name="services">Service collection fo Dependency Injection Container.</param>
    /// <returns>Collection of DI services.</returns>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}
