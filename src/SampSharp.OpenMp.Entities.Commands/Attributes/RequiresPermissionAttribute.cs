namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Adds a <c>permission</c> tag to the command.
/// See also <see cref="CommandTagAttribute" />.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class RequiresPermissionAttribute(string permission) : CommandTagAttribute("permission", permission);