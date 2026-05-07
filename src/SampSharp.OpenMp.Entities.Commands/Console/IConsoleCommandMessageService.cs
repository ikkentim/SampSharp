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
    /// <param name="overloads">All overloads of the command.</param>
    /// <param name="usedCommandName">The actual command name/path used (e.g., alias name). Empty string means use canonical command name.</param>
    /// <returns>True to continue processing, false to stop.</returns>
    bool SendUsage(ConsoleCommandDispatchContext context, IReadOnlyList<CommandDefinition> overloads, string usedCommandName = "");
}
