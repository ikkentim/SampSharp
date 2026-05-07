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
        /// <returns>The ECS host builder for chaining.</returns>
        public IEcsHostBuilder UsePlayerCommands()
        {
            return hostBuilder
                .ConfigureServices(services => services.AddPlayerCommands())
                .Configure(builder => builder.UsePlayerCommands());
        }

        /// <summary>
        /// Configures the host to use console command processing.
        /// </summary>
        /// <returns>The ECS host builder for chaining.</returns>
        public IEcsHostBuilder UseConsoleCommands()
        {
            return hostBuilder
                .ConfigureServices(services => services.AddConsoleCommands())
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