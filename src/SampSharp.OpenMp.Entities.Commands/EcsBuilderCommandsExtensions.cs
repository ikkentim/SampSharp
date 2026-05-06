using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>Provides extension methods for registering the commands subsystem.</summary>
public static class EcsBuilderCommandsExtensions
{
    /// <summary>
    /// Registers player command services and systems in the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to register services into.</param>
    /// <returns>The service collection for method chaining.</returns>
    /// <remarks>
    /// This method registers both the player command service and the core commands system infrastructure.
    /// Call this method to enable player command handling in your game mode.
    /// </remarks>
    public static IServiceCollection AddPlayerCommands(this IServiceCollection services)
    {
        services.AddCommandsSystem();

        services.TryAddSingleton<IPlayerCommandService>(sp => sp.GetRequiredService<PlayerCommandService>());
        return services;
    }

    /// <summary>
    /// Registers all core command system services in the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to register services into.</param>
    /// <returns>The service collection for method chaining.</returns>
    /// <remarks>
    /// This method registers default implementations of command text formatting, permission checking,
    /// message services, and the command scanning system. Override these services by registering
    /// your own implementations before calling this method to use custom implementations.
    /// </remarks>
    public static IServiceCollection AddCommandsSystem(this IServiceCollection services)
    {
        services.TryAddSingleton<ICommandTextFormatter, DefaultCommandTextFormatter>();
        services.TryAddSingleton<IPlayerCommandMessageService, DefaultPlayerCommandMessageService>();
        services.TryAddSingleton<IConsoleCommandMessageService, DefaultConsoleCommandMessageService>();
        services.TryAddSingleton<IPermissionChecker, DefaultPermissionChecker>();
        services.TryAddSingleton<IPlayerCommandService, PlayerCommandService>();
        services.TryAddSingleton<IConsoleCommandService, ConsoleCommandService>();

        // Register the console bridge system for handling console command events
        services.AddSystem<ConsoleBridgeSystem>();

        return services;
    }

    /// <summary>
    /// Wires <see cref="PlayerCommandProcessingMiddleware" /> on
    /// <c>OnPlayerCommandText</c>: any chat input not claimed by an
    /// <c>[Event]</c> listener gets forwarded to <see cref="IPlayerCommandService" />.
    /// </summary>
    public static IEcsBuilder UsePlayerCommands(this IEcsBuilder builder)
    {
        return builder.UseMiddleware<PlayerCommandProcessingMiddleware>("OnPlayerCommandText");
    }

    /// <summary>
    /// Registers the player-commands subsystem: adds the default
    /// <see cref="IPlayerCommandService" /> implementation and wires up the
    /// <see cref="PlayerCommandProcessingMiddleware" /> on <c>OnPlayerCommandText</c>.
    /// </summary>
    public static IEcsHostBuilder UsePlayerCommands(this IEcsHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices(services => services.AddPlayerCommands());
        hostBuilder.Configure(builder => builder.UsePlayerCommands());

        return hostBuilder;
    }
}