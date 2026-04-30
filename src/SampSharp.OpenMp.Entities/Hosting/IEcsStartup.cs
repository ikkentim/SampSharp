using Microsoft.Extensions.DependencyInjection;
using SampSharp.OpenMp.Core;

namespace SampSharp.Entities;

/// <summary>
/// Provides methods for configuring an Entity Component System in SampSharp.
/// </summary>
public interface IEcsStartup : IStartup
{
    /// <summary>
    /// Register services into the <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add the services to.</param>
    void ConfigureServices(IServiceCollection services);

    /// <summary>
    /// Configures the application.
    /// </summary>
    /// <param name="builder">An <see cref="IEcsBuilder" /> for the app to configure.</param>
    void Configure(IEcsBuilder builder);
}