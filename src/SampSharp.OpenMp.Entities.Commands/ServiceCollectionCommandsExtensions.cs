using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Extension methods for configuring the command system in the dependency injection container.
/// </summary>
public static class ServiceCollectionCommandsExtensions
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Adds core command system services to the service collection.
        /// </summary>
        /// <returns>The service collection for chaining.</returns>
        public IServiceCollection AddCommandsSystem()
        {
            services.TryAddSingleton<ICommandTextFormatter, DefaultCommandTextFormatter>();
            services.TryAddSingleton<ICommandParameterParserFactory, DefaultCommandParameterParserFactory>();

            return services;
        }

        /// <summary>
        /// Adds player command services to the service collection.
        /// </summary>
        /// <returns>The service collection for chaining.</returns>
        public IServiceCollection AddPlayerCommands()
        {
            services.AddCommandsSystem();

            services.TryAddSingleton<IPermissionChecker, DefaultPermissionChecker>();
            services.TryAddSingleton<IPlayerCommandService, PlayerCommandService>();
            services.TryAddSingleton<IPlayerCommandMessageService, DefaultPlayerCommandMessageService>();

            return services;
        }

        /// <summary>
        /// Adds console command services to the service collection.
        /// </summary>
        /// <returns>The service collection for chaining.</returns>
        public IServiceCollection AddConsoleCommands()
        {
            services.AddCommandsSystem();

            services.TryAddSingleton<IConsoleCommandService, ConsoleCommandService>();
            services.TryAddSingleton<IConsoleCommandMessageService, DefaultConsoleCommandMessageService>();

            return services;
        }
    }
}