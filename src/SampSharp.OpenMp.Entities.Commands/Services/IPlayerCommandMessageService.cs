using PlayerComponent = SampSharp.Entities.SAMP.Player;

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
    /// <param name="command">The command definition.</param>
    /// <returns>True to continue processing, false to stop.</returns>
    bool SendUsage(PlayerComponent player, CommandDefinition command);

    /// <summary>
    /// Sends a permission denied message to a player.
    /// </summary>
    /// <param name="player">The player to send the message to.</param>
    /// <param name="command">The command that was denied.</param>
    /// <returns>True to continue processing (treat as command not found), false to stop.</returns>
    bool SendPermissionDenied(PlayerComponent player, CommandDefinition command);

    /// <summary>
    /// Sends a command not found message to a player.
    /// </summary>
    /// <param name="player">The player to send the message to.</param>
    /// <param name="input">The input text that didn't match any command.</param>
    /// <returns>True to continue processing, false to stop and return false from the command handler.</returns>
    bool SendCommandNotFound(PlayerComponent player, string input);
}
