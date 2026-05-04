using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SampSharp.Entities.SAMP.Commands.Core;
using SampSharp.Entities.SAMP.Commands.Console;
using SampSharp.Entities.SAMP.Commands.Help;
using SampSharp.Entities.SAMP.Commands.Player;
using SampSharp.Entities.SAMP.Commands.Services;

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

    /// <summary>
    /// Registers the new Commands system infrastructure:
    /// - CommandRegistry (for command storage/lookup)
    /// - CommandDispatcher (for parsing and matching)
    /// - ICommandEnumerator (for help/discovery)
    /// - ICommandUsageFormatter (for message formatting and delivery)
    /// - IPermissionChecker (for permission validation)
    /// - Service implementations (help, error handling)
    /// - ConsoleBridgeSystem (handles console command registration and dispatch)
    /// </summary>
    public static IServiceCollection AddCommandsSystem(this IServiceCollection services)
    {
        services.TryAddSingleton<CommandRegistry>();
        services.TryAddSingleton<ICommandUsageFormatter, DefaultCommandUsageFormatter>();
        services.TryAddSingleton<IPermissionChecker, DefaultPermissionChecker>();
        services.TryAddSingleton<ICommandHelpProvider, DefaultCommandHelpProvider>();
        services.TryAddSingleton<Services.ICommandEnumerator>(sp =>
            new Services.DefaultCommandEnumerator(sp.GetRequiredService<CommandRegistry>(), sp.GetRequiredService<IPermissionChecker>()));

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
