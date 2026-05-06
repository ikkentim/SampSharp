namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Represents the complete definition of a command with all its overloads.
/// A command can have multiple overloads (different parameter signatures) but the same name and group.
/// </summary>
internal class CommandSet
{
    private readonly CommandDefinition[] _overloads;

    /// <summary>Initializes a new instance.</summary>
    internal CommandSet(string name, CommandGroup? group, CommandDefinition[] overloads)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Command name cannot be empty.", nameof(name));
        }

        if (overloads == null || overloads.Length == 0)
        {
            throw new ArgumentException("Command must have at least one overload.", nameof(overloads));
        }

        Name = name;
        Group = group;
        _overloads = overloads;
    }

    /// <summary>The command name (without leading slash or group prefix).</summary>
    public string Name { get; }

    /// <summary>The command group, if any (e.g., ["admin", "money"]).</summary>
    public CommandGroup? Group { get; }

    /// <summary>All overloads of this command.</summary>
    public IReadOnlyList<CommandDefinition> Overloads => _overloads;

    /// <summary>The full command path (group + name), e.g., "admin money give".</summary>
    public string FullName => Group.HasValue ? $"{Group.Value.FullName} {Name}" : Name;
}