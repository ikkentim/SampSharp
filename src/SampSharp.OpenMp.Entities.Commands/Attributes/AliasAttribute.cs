using JetBrains.Annotations;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Marks a command method as having one or more aliases (shorthand names).
/// Aliases can be used instead of the full command path.
/// For example, [Alias("pm")] on a "message send" command allows "/pm" instead of "/message send".
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class AliasAttribute : Attribute
{
    /// <summary>Initializes a new instance with a single alias.</summary>
    public AliasAttribute(string alias)
    {
        if (string.IsNullOrWhiteSpace(alias))
        {
            throw new ArgumentException("Alias cannot be empty.", nameof(alias));
        }

        Aliases = [alias];
    }

    /// <summary>Initializes a new instance with multiple aliases.</summary>
    public AliasAttribute(params string[] aliases)
    {
        if (aliases == null || aliases.Length == 0)
        {
            throw new ArgumentException("At least one alias must be specified.", nameof(aliases));
        }

        if (aliases.Any(string.IsNullOrWhiteSpace))
        {
            throw new ArgumentException("Aliases cannot be empty.", nameof(aliases));
        }

        Aliases = aliases;
    }

    /// <summary>The alias names (without leading slashes).</summary>
    public string[] Aliases { get; }
}
