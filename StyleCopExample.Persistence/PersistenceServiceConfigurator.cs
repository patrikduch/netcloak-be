namespace NetCloak.Persistence;

using Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

/// <summary>
/// Registration of Persistence services.
/// </summary>
public static class PersistenceServiceConfigurator
{
    /// <summary>
    /// Definition of service sets that are being used by Infrastructure project.
    /// </summary>
    /// <param name="services">Service collection fo Dependency Injection Container.</param>
    /// <returns>Collection of DI services.</returns>
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        var listener = new EfCoreDiagnosticListener();
        DiagnosticListener.AllListeners.Subscribe(listener);

        return services;
    }
}