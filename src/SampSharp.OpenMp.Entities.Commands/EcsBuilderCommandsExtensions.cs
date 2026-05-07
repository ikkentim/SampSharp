namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Extension methods for configuring the command system middleware in the ECS builder.
/// </summary>
public static class EcsBuilderCommandsExtensions
{

    extension(IEcsBuilder builder)
    {
        /// <summary>
        /// Adds console command processing middleware to the ECS configuration.
        /// </summary>
        /// <returns>The ECS builder for chaining.</returns>
        public IEcsBuilder UseConsoleCommands()
        {
            return builder
                .UseMiddleware<ConsoleCommandProcessingMiddleware>("OnConsoleText")
                .UseMiddleware<ConsoleCommandListMiddleware>("OnConsoleCommandListRequest");
        }

        /// <summary>
        /// Adds player command processing middleware to the ECS configuration.
        /// </summary>
        /// <returns>The ECS builder for chaining.</returns>
        public IEcsBuilder UsePlayerCommands()
        {
            return builder.UseMiddleware<PlayerCommandProcessingMiddleware>("OnPlayerCommandText");
        }
    }
}