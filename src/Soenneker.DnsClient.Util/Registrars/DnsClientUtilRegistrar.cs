using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.DnsClient.Util.Abstract;

namespace Soenneker.DnsClient.Util.Registrars;

/// <summary>
/// An async thread-safe singleton for DnsClient.NET
/// </summary>
public static class DnsClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="IDnsClientUtil"/> as a singleton service. <para/>
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddDnsClientUtilAsSingleton(this IServiceCollection services)
    {
        services.TryAddSingleton<IDnsClientUtil, DnsClientUtil>();
        return services;
    }

    /// <summary>
    /// Adds <see cref="IDnsClientUtil"/> as a scoped service. <para/>
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddDnsClientUtilAsScoped(this IServiceCollection services)
    {
        services.TryAddScoped<IDnsClientUtil, DnsClientUtil>();
        return services;
    }
}
