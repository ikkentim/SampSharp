namespace SampSharp.Entities.SAMP.Commands.Core;

/// <summary>
/// Represents the full definition of a command with all its metadata.
/// A command can have multiple overloads (different parameter signatures).
/// </summary>
public class CommandDefinition
{
    private readonly CommandOverload[] _overloads;
    private readonly CommandAlias[] _aliases;
    private readonly string[] _permissions;

    /// <summary>Initializes a new instance.</summary>
    public CommandDefinition(
        string name,
        CommandGroup? group,
        CommandOverload[] overloads,
        CommandAlias[]? aliases = null,
        string[]? permissions = null)
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
        _aliases = aliases ?? [];
        _permissions = permissions ?? [];
    }

    /// <summary>The command name (without leading slash or group prefix).</summary>
    public string Name { get; }

    /// <summary>The command group, if any (e.g., ["admin", "money"]).</summary>
    public CommandGroup? Group { get; }

    /// <summary>All overloads of this command.</summary>
    public IReadOnlyList<CommandOverload> Overloads => _overloads;

    /// <summary>Aliases for this command (shorthand names).</summary>
    public IReadOnlyList<CommandAlias> Aliases => _aliases;

    /// <summary>Required permissions for this command.</summary>
    public IReadOnlyList<string> Permissions => _permissions;

    /// <summary>The full command path (group + name), e.g., "admin money give".</summary>
    public string FullName => Group.HasValue
        ? $"{Group.Value.FullName} {Name}"
        : Name;

    /// <summary>Checks if this command has the given alias.</summary>
    public bool HasAlias(string alias)
        => _aliases.Any(a => a.Name == alias);

    /// <summary>Tries to find an overload that can handle the given parameter count.</summary>
    public CommandOverload? FindBestOverload(int parameterCount)
    {
        // Try exact match first
        var exact = _overloads.FirstOrDefault(o => o.ParsedParameters.Length == parameterCount);
        if (exact != null)
        {
            return exact;
        }

        // Try overload with optional parameters that can satisfy the count
        return _overloads.FirstOrDefault(o =>
        {
            var required = o.ParsedParameters.Count(p => p.IsRequired);
            var optional = o.ParsedParameters.Count(p => !p.IsRequired);
            return parameterCount >= required && parameterCount <= (required + optional);
        });
    }

    /// <summary>Gets a human-readable description for displaying in help.</summary>
    public string GetDisplayName()
    {
        if (Group.HasValue)
        {
            return $"/{Group.Value.FullName} {Name}";
        }

        return $"/{Name}";
    }
}
