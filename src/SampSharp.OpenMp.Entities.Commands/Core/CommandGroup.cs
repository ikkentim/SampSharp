namespace SampSharp.Entities.SAMP.Commands.Core;

/// <summary>
/// Represents a hierarchical command group (e.g., ["admin", "money", "give"]).
/// Command groups provide a namespace for commands.
/// </summary>
public readonly struct CommandGroup : IEquatable<CommandGroup>
{
    private readonly string[] _parts;

    /// <summary>Initializes a new instance from an ordered sequence of group parts.</summary>
    public CommandGroup(params string[] parts)
    {
        if (parts == null || parts.Length == 0)
        {
            throw new ArgumentException("Command group must have at least one part.", nameof(parts));
        }

        _parts = parts.ToArray();
    }

    /// <summary>Initializes a new instance from a collection of group parts.</summary>
    public CommandGroup(IEnumerable<string> parts)
    {
        var partsList = parts?.ToArray() ?? [];
        if (partsList.Length == 0)
        {
            throw new ArgumentException("Command group must have at least one part.", nameof(parts));
        }

        _parts = partsList;
    }

    /// <summary>The ordered parts of this command group (e.g., ["admin", "money"]).</summary>
    public IReadOnlyList<string> Parts => _parts;

    /// <summary>The full command group as a space-separated string (e.g., "admin money").</summary>
    public string FullName => string.Join(" ", _parts);

    /// <summary>The number of parts in this command group.</summary>
    public int Depth => _parts.Length;

    /// <summary>Gets a subgroup with the first N parts (e.g., depth 1 of ["admin", "money"] = ["admin"]).</summary>
    public CommandGroup GetParent(int depth)
    {
        if (depth < 1 || depth > _parts.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(depth));
        }

        return new CommandGroup(_parts.Take(depth).ToArray());
    }

    /// <summary>Stacks this group with another, returning a new group with combined parts.</summary>
    public CommandGroup Stack(CommandGroup other)
    {
        var combined = _parts.Concat(other._parts).ToArray();
        return new CommandGroup(combined);
    }

    /// <summary>Stacks this group with a single part.</summary>
    public CommandGroup Stack(string part)
    {
        if (string.IsNullOrWhiteSpace(part))
        {
            throw new ArgumentException("Group part cannot be empty.", nameof(part));
        }

        var combined = _parts.Append(part).ToArray();
        return new CommandGroup(combined);
    }

    public override bool Equals(object? obj)
    {
        return obj is CommandGroup other && Equals(other);
    }

    public bool Equals(CommandGroup other)
    {
        return _parts.SequenceEqual(other._parts);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_parts);
    }

    public override string ToString()
    {
        return FullName;
    }

    public static bool operator ==(CommandGroup left, CommandGroup right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(CommandGroup left, CommandGroup right)
    {
        return !left.Equals(right);
    }
}
