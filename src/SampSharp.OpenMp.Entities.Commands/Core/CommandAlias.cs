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

    public override bool Equals(object? obj) => obj is CommandAlias other && Equals(other);
    public bool Equals(CommandAlias other) => Name == other.Name;
    public override int GetHashCode() => Name.GetHashCode();

    public override string ToString() => Name;

    public static bool operator ==(CommandAlias left, CommandAlias right) => left.Equals(right);
    public static bool operator !=(CommandAlias left, CommandAlias right) => !left.Equals(right);
}
