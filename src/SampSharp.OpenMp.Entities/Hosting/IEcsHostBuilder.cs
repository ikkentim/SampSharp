using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SampSharp.Entities;

/// <summary>
/// Defines methods for configuring the Entity Component System (ECS) framework, including systems, services, logging,
/// exception handling, and service provider factories.
/// </summary>
public interface IEcsHostBuilder
{
    /// <summary>
    /// Configures the ECS framework itself, allowing for configuration of systems, components, and other ECS-related features.
    /// </summary>
    /// <param name="build">A delegate that configures the ECS builder.</param>
    /// <returns>The updated host builder.</returns>
    IEcsHostBuilder Configure(Action<IEcsBuilder> build);

    /// <summary>
    /// Configures the services used by the application.
    /// </summary>
    /// <param name="build">A delegate that configures the service collection.</param>
    /// <returns>The updated host builder.</returns>
    IEcsHostBuilder ConfigureServices(Action<SampSharpEnvironment, IServiceCollection> build);

    /// <summary>
    /// Configures the services used by the application.
    /// </summary>
    /// <param name="build">A delegate that configures the service collection.</param>
    /// <returns>The updated host builder.</returns>
    IEcsHostBuilder ConfigureServices(Action<IServiceCollection> build);

    /// <summary>
    /// Configures the logging used by the application.
    /// </summary>
    /// <param name="builder">A delegate that configures the logging builder.</param>
    /// <returns>The updated host builder.</returns>
    IEcsHostBuilder ConfigureLogging(Action<ILoggingBuilder> builder);

    /// <summary>
    /// Configures the unhandled exception handler used by the application.
    /// </summary>
    /// <param name="handler">The handler for unhandled exceptions during the execution of the application.</param>
    /// <returns>The updated host builder.</returns>
    IEcsHostBuilder ConfigureUnhandledExceptionhandler(UnhandledExceptionHandler handler);

    /// <summary>
    /// Configures the service provider factory used by the application.
    /// </summary>
    /// <typeparam name="TContainerBuilder">The type of the container builder.</typeparam>
    /// <param name="serviceProviderFactory">The factory to use to create the service provider.</param>
    /// <returns>The updated host builder.</returns>
    IEcsHostBuilder UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> serviceProviderFactory)
        where TContainerBuilder : notnull;

    /// <summary>
    /// Prevents the framework from automatically loading systems from the entry assembly. This is useful if you want to manually control which systems are loaded.
    /// </summary>
    /// <returns>The updated host builder.</returns>
    IEcsHostBuilder DisableDefaultSystemsLoading();
}