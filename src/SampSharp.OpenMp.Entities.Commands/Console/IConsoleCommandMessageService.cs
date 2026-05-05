namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Sends formatted command messages to the console.
/// </summary>
public interface IConsoleCommandMessageService
{
    /// <summary>
    /// Sends a usage message to the console.
    /// </summary>
    /// <param name="context">The console command dispatch context.</param>
    /// <param name="command">The command definition.</param>
    /// <returns>True to continue processing, false to stop.</returns>
    bool SendUsage(ConsoleCommandDispatchContext context, CommandDefinition command);
}
