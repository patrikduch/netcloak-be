namespace NetCloak.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using NetCloak.Application.Interfaces.Infrastructure;
using Microsoft.Extensions.Configuration;
using NetCloak.Infrastructure.Options;
using NetCloak.Infrastructure.Services;

/// <summary>
/// Registration of infrastructure services.
/// </summary>
public static class InfrastructureServiceConfigurator
{
    /// <summary>
    /// Definition of service sets that are being used by Infrastructure project.
    /// </summary>
    /// <param name="services">Service collection fo Dependency Injection Container.</param>
    /// <param name="configuration">Application configuration used for binding infrastructure options.</param>
    /// <returns>Collection of DI services.</returns>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEmailService, EmailService>();

        services
            .AddOptions<KeycloakOptions>()
            .Bind(configuration.GetSection(KeycloakOptions.SectionName))
            .Validate(
                o =>
                !string.IsNullOrWhiteSpace(o.Authority) &&
                !string.IsNullOrWhiteSpace(o.ClientId) &&
                !string.IsNullOrWhiteSpace(o.ClientSecret),
                "Keycloak configuration is incomplete")
            .ValidateOnStart();

        services.AddHttpClient<IAuthService, KeycloakAuthService>();

        return services;
    }
}
