using System.Reflection;

namespace SampSharp.Entities.SAMP.Commands.Core;

/// <summary>
/// Central registry of all commands. Scans ISystem types via reflection to discover
/// and register commands marked with [PlayerCommand], [ConsoleCommand], and related attributes.
/// </summary>
public class CommandRegistry
{
    private readonly Dictionary<string, CommandDefinition> _commandsByName = new();
    private readonly Dictionary<string, CommandDefinition> _aliasMap = new();
    private readonly List<CommandDefinition> _allCommands = new();

    /// <summary>Registers a command definition in the registry.</summary>
    public void Register(CommandDefinition definition)
    {
        if (definition == null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        // Register by full name (or short name if no group)
        var key = definition.FullName.ToLowerInvariant();
        if (_commandsByName.ContainsKey(key))
        {
            throw new InvalidOperationException($"Command '{definition.FullName}' is already registered.");
        }

        _commandsByName[key] = definition;
        _allCommands.Add(definition);

        // Register aliases
        foreach (var alias in definition.Aliases)
        {
            var aliasKey = alias.Name.ToLowerInvariant();
            if (_aliasMap.ContainsKey(aliasKey))
            {
                throw new InvalidOperationException($"Alias '{alias.Name}' is already registered.");
            }

            _aliasMap[aliasKey] = definition;
        }
    }

    /// <summary>Tries to find a command by name or alias (case-insensitive).</summary>
    public CommandDefinition? TryFind(string nameOrAlias)
    {
        if (string.IsNullOrWhiteSpace(nameOrAlias))
        {
            return null;
        }

        var key = nameOrAlias.ToLowerInvariant();

        // Try as full/short name first
        if (_commandsByName.TryGetValue(key, out var cmd))
        {
            return cmd;
        }

        // Try as alias
        if (_aliasMap.TryGetValue(key, out var aliased))
        {
            return aliased;
        }

        return null;
    }

    /// <summary>Tries to find a command by its full path, matching command groups.</summary>
    public CommandDefinition? TryFindByPath(IEnumerable<string> pathParts)
    {
        if (pathParts == null)
        {
            return null;
        }

        var parts = pathParts.ToList();
        if (parts.Count == 0)
        {
            return null;
        }

        // Build possible full names from longest to shortest match
        for (int i = parts.Count; i > 0; i--)
        {
            var partial = string.Join(" ", parts.Take(i)).ToLowerInvariant();
            if (_commandsByName.TryGetValue(partial, out var cmd))
            {
                return cmd;
            }
        }

        return null;
    }

    /// <summary>Gets all registered commands.</summary>
    public IReadOnlyList<CommandDefinition> GetAll() => _allCommands.AsReadOnly();

    /// <summary>Gets all player commands.</summary>
    public IEnumerable<CommandDefinition> GetPlayerCommands()
        => _allCommands.Where(c => c.IsPlayerCommand);

    /// <summary>Gets all console commands.</summary>
    public IEnumerable<CommandDefinition> GetConsoleCommands()
        => _allCommands.Where(c => c.IsConsoleCommand);

    /// <summary>Gets all commands in a specific group.</summary>
    public IEnumerable<CommandDefinition> GetCommandsInGroup(CommandGroup group)
        => _allCommands.Where(c => c.Group == group);

    /// <summary>Gets all command groups.</summary>
    public IEnumerable<CommandGroup> GetGroups()
        => _allCommands
            .Where(c => c.Group.HasValue)
            .Select(c => c.Group.Value)
            .Distinct();

    /// <summary>Clears all registered commands.</summary>
    public void Clear()
    {
        _commandsByName.Clear();
        _aliasMap.Clear();
        _allCommands.Clear();
    }
}
