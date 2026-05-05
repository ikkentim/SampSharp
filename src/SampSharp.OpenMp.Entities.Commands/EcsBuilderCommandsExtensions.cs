using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>Extensions to register the commands subsystem.</summary>
public static class EcsBuilderCommandsExtensions
{
    /// <summary>
    /// Registers <see cref="IPlayerCommandService" /> with the default
    /// <see cref="PlayerCommandService" /> implementation (NEW system).
    /// Uses <see cref="ServiceCollectionDescriptorExtensions.TryAddSingleton(IServiceCollection, Type, Type)" />
    /// so a custom <see cref="IPlayerCommandService" /> registered earlier wins.
    /// </summary>
    public static IServiceCollection AddPlayerCommands(this IServiceCollection services)
    {
        services.AddCommandsSystem();

        services.TryAddSingleton<IPlayerCommandService>(sp => sp.GetRequiredService<PlayerCommandService>());
        return services;
    }

    public static IServiceCollection AddCommandsSystem(this IServiceCollection services)
    {
        services.TryAddSingleton<ICommandTextFormatter, DefaultCommandTextFormatter>();
        services.TryAddSingleton<IPlayerCommandMessageService, DefaultPlayerCommandMessageService>();
        services.TryAddSingleton<IConsoleCommandMessageService, DefaultConsoleCommandMessageService>();
        services.TryAddSingleton<IPermissionChecker, DefaultPermissionChecker>();

        services.TryAddSingleton<PlayerCommandService>();
        services.TryAddSingleton<ConsoleCommandService>();

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