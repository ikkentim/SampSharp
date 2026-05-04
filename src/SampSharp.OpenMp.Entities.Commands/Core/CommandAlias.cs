namespace SampSharp.Entities.SAMP.Commands.Core;

/// <summary>
/// Represents an alias for a command. An alias allows a command to be invoked
/// using a different name (shorthand), ignoring command groups.
/// For example, "/pm" as an alias for "/message send".
/// </summary>
public readonly struct CommandAlias : IEquatable<CommandAlias>
{
    /// <summary>Initializes a new instance with an alias name.</summary>
    public CommandAlias(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Alias name cannot be empty.", nameof(name));
        }

        Name = name;
    }

    /// <summary>The alias name (without leading slash).</summary>
    public string Name { get; }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return obj is CommandAlias other && Equals(other);
    }

    /// <inheritdoc />
    public bool Equals(CommandAlias other)
    {
        return Name == other.Name;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return Name;
    }

    /// <summary>
    /// Implementation of the equality operator.
    /// </summary>
    public static bool operator ==(CommandAlias left, CommandAlias right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Implementation of the inequality operator.
    /// </summary>
    public static bool operator !=(CommandAlias left, CommandAlias right)
    {
        return !left.Equals(right);
    }
}
