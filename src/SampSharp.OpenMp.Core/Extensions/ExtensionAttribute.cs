using SampSharp.OpenMp.Core.Api;

namespace SampSharp.OpenMp.Core;

/// <summary>
/// Provides a unique identifier for an extension.
/// </summary>
/// <param name="uid">The unique identifier of the extension.</param>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ExtensionAttribute(ulong uid) : Attribute
{
    /// <summary>
    /// Gets the unique identifier of the extension.
    /// </summary>
    public UID Uid => new(uid);
}