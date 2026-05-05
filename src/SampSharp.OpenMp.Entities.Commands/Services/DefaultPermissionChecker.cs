using PlayerComponent = SampSharp.Entities.SAMP.Player;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Default permission checker that grants all permissions.
/// In a real application, override this with custom permission logic.
/// </summary>
public class DefaultPermissionChecker : IPermissionChecker
{
    public bool HasPermission(PlayerComponent player, CommandOverload command)
    {
        // Default: allow all permissions
        return true;
    }
}