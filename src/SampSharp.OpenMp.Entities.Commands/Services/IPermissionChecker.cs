using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands.Core;
using PlayerComponent = SampSharp.Entities.SAMP.Player;

namespace SampSharp.Entities.SAMP.Commands.Services;

/// <summary>
/// Checks whether a player has permission to invoke a specific command.
/// Used only for player commands; console commands do not require permission checks.
/// </summary>
public interface IPermissionChecker
{
    /// <summary>
    /// Checks if the given player has permission to invoke a command.
    /// </summary>
    /// <param name="player">The player entity.</param>
    /// <param name="command">The command definition with metadata tags.</param>
    /// <returns>True if the player has permission; otherwise, false.</returns>
    bool HasPermission(PlayerComponent player, CommandDefinition command);
}

