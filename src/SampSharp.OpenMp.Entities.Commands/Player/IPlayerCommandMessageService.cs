namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Sends formatted command messages to players.
/// </summary>
public interface IPlayerCommandMessageService
{
    /// <summary>
    /// Sends a usage message to a player.
    /// </summary>
    /// <param name="player">The player to send the message to.</param>
    /// <param name="overloads">All overloads of the command.</param>
    /// <param name="usedCommandName">The actual command name/path used (e.g., alias name). Empty string means use canonical command name.</param>
    void SendUsage(Player player, IReadOnlyList<CommandDefinition> overloads, string usedCommandName = "");

    /// <summary>
    /// Sends a permission denied message to a player.
    /// </summary>
    /// <param name="player">The player to send the message to.</param>
    /// <param name="overload">The command overload that was denied.</param>
    /// <returns>True if a message was send to the player, false otherwise.</returns>
    bool SendPermissionDenied(Player player, CommandDefinition overload);

    /// <summary>
    /// Sends a command not found message to a player.
    /// </summary>
    /// <param name="player">The player to send the message to.</param>
    /// <param name="input">The input text that didn't match any command.</param>
    /// <returns>True to continue processing, false to stop and return false from the command handler.</returns>
    bool SendCommandNotFound(Player player, string input);
}
