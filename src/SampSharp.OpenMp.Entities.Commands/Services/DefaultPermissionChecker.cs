namespace SampSharp.Entities.SAMP.Commands.Services;

/// <summary>
/// Default permission checker that grants all permissions.
/// In a real application, override this with custom permission logic.
/// </summary>
public class DefaultPermissionChecker : IPermissionChecker
{
    public bool HasPermission(EntityId player, params string[] permissions)
    {
        // Default: allow all permissions
        return true;
    }
}
