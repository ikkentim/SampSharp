namespace SampSharp.Entities.SAMP.Commands.Services;

/// <summary>
/// Checks whether a player has permission to invoke a specific command.
/// Used only for player commands; console commands do not require permission checks.
/// </summary>
public interface IPermissionChecker
{
    /// <summary>
    /// Checks if the given player has all required permissions to invoke a command.
    /// </summary>
    /// <param name="player">The player entity ID.</param>
    /// <param name="permissions">The permission(s) required (all must be satisfied).</param>
    /// <returns>True if the player has all required permissions; otherwise, false.</returns>
    bool HasPermission(EntityId player, params string[] permissions);

    /// <summary>
    /// Checks if the given player has all required permissions to invoke a command.
    /// </summary>
    /// <param name="player">The player entity ID.</param>
    /// <param name="permissions">The permission(s) required (all must be satisfied).</param>
    /// <returns>True if the player has all required permissions; otherwise, false.</returns>
    bool HasPermission(EntityId player, IEnumerable<string> permissions)
    {
        return HasPermission(player, permissions.ToArray());
    }
}
