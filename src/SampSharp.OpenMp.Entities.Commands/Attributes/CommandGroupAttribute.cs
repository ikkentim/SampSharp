namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Marks a class or method as part of one or more command groups.
/// Can be applied multiple times to stack groups.
/// For example, [CommandGroup("admin"), CommandGroup("money")] stacks to "admin money".
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class CommandGroupAttribute : Attribute
{
    /// <summary>Initializes a new instance with a single group part.</summary>
    public CommandGroupAttribute(string part)
    {
        if (string.IsNullOrWhiteSpace(part))
        {
            throw new ArgumentException("Group part cannot be empty.", nameof(part));
        }

        Parts = new[]
        {
            part
        };
    }

    /// <summary>Initializes a new instance with multiple group parts.</summary>
    public CommandGroupAttribute(params string[] parts)
    {
        if (parts == null || parts.Length == 0)
        {
            throw new ArgumentException("At least one group part must be specified.", nameof(parts));
        }

        if (parts.Any(string.IsNullOrWhiteSpace))
        {
            throw new ArgumentException("Group parts cannot be empty.", nameof(parts));
        }

        Parts = parts;
    }

    /// <summary>The group parts (e.g., ["admin", "money"]).</summary>
    public string[] Parts { get; }
}