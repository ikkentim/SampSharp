using PlayerComponent = SampSharp.Entities.SAMP.Player;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Formats and sends command usage/error messages to players or console.
/// This service is responsible for all message delivery to players and console.
/// </summary>
public interface ICommandUsageFormatter
{
    /// <summary>
    /// Formats and sends the usage message for a command to a player.
    /// </summary>
    /// <param name="player">The player to send the message to.</param>
    /// <param name="command">The command definition.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task FormatUsageAsync(PlayerComponent player, CommandDefinition command);

    /// <summary>
    /// Formats and sends the "command not found" message to a player.
    /// </summary>
    /// <param name="player">The player to send the message to.</param>
    /// <param name="input">The input text that didn't match any command.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task FormatNotFoundAsync(PlayerComponent player, string input);

    /// <summary>
    /// Formats and sends the "permission denied" message to a player.
    /// </summary>
    /// <param name="player">The player to send the message to.</param>
    /// <param name="command">The command that was denied.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task FormatPermissionDeniedAsync(PlayerComponent player, CommandDefinition command);

    /// <summary>
    /// Formats and sends the usage message for a command to a console.
    /// </summary>
    /// <param name="context">The console command dispatch context.</param>
    /// <param name="command">The command definition.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task FormatUsageAsync(ConsoleCommandDispatchContext context, CommandDefinition command);

    /// <summary>
    /// Formats and sends the "command not found" message to a console.
    /// </summary>
    /// <param name="context">The console command dispatch context.</param>
    /// <param name="input">The input text that didn't match any command.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task FormatNotFoundAsync(ConsoleCommandDispatchContext context, string input);
}