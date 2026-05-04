using JetBrains.Annotations;

namespace SampSharp.Entities.SAMP.Commands.Attributes;

/// <summary>
/// Attaches custom metadata to a command via key-value pairs.
/// This attribute can be applied multiple times to add multiple tags.
/// 
/// Built-in tags:
/// - "permission" (value is the permission key, used by IPermissionChecker for player commands)
/// 
/// Users can define custom tags for their own purposes.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
[MeansImplicitUse]
public class CommandTagAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance with the specified key and value.
    /// </summary>
    /// <param name="key">The tag key (e.g., "permission", "category").</param>
    /// <param name="value">The tag value.</param>
    public CommandTagAttribute(string key, string value)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentException("Tag key cannot be empty.", nameof(key));
        }

        if (value == null)
        {
            throw new ArgumentNullException(nameof(value), "Tag value cannot be null.");
        }

        Key = key;
        Value = value;
    }

    /// <summary>The tag key.</summary>
    public string Key { get; }

    /// <summary>The tag value.</summary>
    public string Value { get; }
}
