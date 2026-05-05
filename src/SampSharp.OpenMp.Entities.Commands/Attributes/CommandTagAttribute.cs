namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Attaches custom metadata to a command via key-value pairs.
/// This attribute can be applied multiple times to add multiple tags.
/// Users can define custom tags for their own purposes.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class CommandTagAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance with the specified key and value.
    /// </summary>
    /// <param name="key">The tag key (e.g., "permission", "category").</param>
    /// <param name="value">The tag value.</param>
    public CommandTagAttribute(string key, string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);
        ArgumentNullException.ThrowIfNull(value);

        Key = key;
        Value = value;
    }

    /// <summary>The tag key.</summary>
    public string Key { get; }

    /// <summary>The tag value.</summary>
    public string Value { get; }
}