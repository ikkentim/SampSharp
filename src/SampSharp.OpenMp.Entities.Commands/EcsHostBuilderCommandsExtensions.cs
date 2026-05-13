namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Extension methods for configuring the command system in the ECS host builder.
/// </summary>
public static class EcsHostBuilderCommandsExtensions
{

    extension(IEcsHostBuilder hostBuilder)
    {
        /// <summary>
        /// Configures the host to use player command processing.
        /// </summary>
        /// <param name="configure">An optional action to configure player command service options.</param>
        /// <returns>The ECS host builder for chaining.</returns>
        public IEcsHostBuilder UsePlayerCommands(Action<PlayerCommandServiceOptions>? configure = null)
        {
            return hostBuilder
                .ConfigureServices(services => services.AddPlayerCommands(configure))
                .Configure(builder => builder.UsePlayerCommands());
        }

        /// <summary>
        /// Configures the host to use console command processing.
        /// </summary>
        /// <param name="configure">An optional action to configure console command service options.</param>
        /// <returns>The ECS host builder for chaining.</returns>
        public IEcsHostBuilder UseConsoleCommands(Action<ConsoleCommandServiceOptions>? configure = null)
        {
            return hostBuilder
                .ConfigureServices(services => services.AddConsoleCommands(configure))
                .Configure(builder => builder.UseConsoleCommands());
        }

        /// <summary>
        /// Configures the host to use both player and console command processing.
        /// </summary>
        /// <returns>The ECS host builder for chaining.</returns>
        public IEcsHostBuilder UseCommands()
        {
            return hostBuilder
                .UsePlayerCommands()
                .UseConsoleCommands();
        }
    }
}