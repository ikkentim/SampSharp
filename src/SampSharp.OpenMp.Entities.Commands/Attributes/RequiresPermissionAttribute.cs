using JetBrains.Annotations;

namespace SampSharp.Entities.SAMP.Commands.Attributes;

/// <summary>
/// Marks a player command method as requiring specific permission(s).
/// The permission check is performed by an injected IPermissionChecker service.
/// This attribute is only applicable to player commands, not console commands.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
[MeansImplicitUse]
public class RequiresPermissionAttribute : Attribute
{
    /// <summary>Initializes a new instance with a single permission requirement.</summary>
    public RequiresPermissionAttribute(string permission)
    {
        if (string.IsNullOrWhiteSpace(permission))
            throw new ArgumentException("Permission cannot be empty.", nameof(permission));

        Permissions = new[] { permission };
    }

    /// <summary>Initializes a new instance with multiple permission requirements (all must be satisfied).</summary>
    public RequiresPermissionAttribute(params string[] permissions)
    {
        if (permissions == null || permissions.Length == 0)
            throw new ArgumentException("At least one permission must be specified.", nameof(permissions));

        if (permissions.Any(string.IsNullOrWhiteSpace))
            throw new ArgumentException("Permissions cannot be empty.", nameof(permissions));

        Permissions = permissions;
    }

    /// <summary>The permission(s) required to invoke this command (all must be satisfied).</summary>
    public string[] Permissions { get; }
}
