namespace SampSharp.Entities.SAMP.Commands;

internal class CommandRegistry : ICommandRegistry
{
    private readonly Dictionary<string, CommandDefinition> _aliasMap = new();
    private readonly List<CommandDefinition> _allCommands = [];
    private readonly Dictionary<string, CommandDefinition> _commandsByName = new();

    public void Register(CommandDefinition definition)
    {
        if (definition == null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        // Register by full name (or short name if no group)
        var key = definition.FullName.ToLowerInvariant();
        if (!_commandsByName.TryAdd(key, definition))
        {
            throw new InvalidOperationException($"Command '{definition.FullName}' is already registered.");
        }

        _allCommands.Add(definition);

        // Register aliases from all overloads
        foreach (var overload in definition.Overloads)
        {
            foreach (var alias in overload.Aliases)
            {
                var aliasKey = alias.Name.ToLowerInvariant();
                if (!_aliasMap.TryAdd(aliasKey, definition))
                {
                    throw new InvalidOperationException($"Alias '{alias.Name}' is already registered.");
                }
            }
        }
    }

    public CommandDefinition? TryFind(string nameOrAlias)
    {
        if (string.IsNullOrWhiteSpace(nameOrAlias))
        {
            return null;
        }

        var key = nameOrAlias.ToLowerInvariant();

        // Try as full/short name first, otherwise as alias
        return _commandsByName.TryGetValue(key, out var cmd) 
            ? cmd
            : _aliasMap.GetValueOrDefault(key);

    }

    public CommandDefinition? TryFindByPath(IEnumerable<string> pathParts)
    {
        return TryFindByPath(pathParts, out _);
    }

    public CommandDefinition? TryFindByPath(IEnumerable<string> pathParts, out int consumedParts)
    {
        consumedParts = 0;
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
        for (var i = parts.Count; i > 0; i--)
        {
            var partial = string.Join(" ", parts.Take(i)).ToLowerInvariant();
            if (_commandsByName.TryGetValue(partial, out var cmd))
            {
                consumedParts = i;
                return cmd;
            }

            // Also check aliases (useful for single-word commands)
            if (_aliasMap.TryGetValue(partial, out var aliased))
            {
                consumedParts = i;
                return aliased;
            }
        }

        return null;
    }

    public IReadOnlyList<CommandDefinition> GetAll()
    {
        return _allCommands.AsReadOnly();
    }

    public IEnumerable<CommandDefinition> GetCommandsInGroup(CommandGroup group)
    {
        return _allCommands.Where(c => c.Group == group);
    }

    public IEnumerable<CommandGroup> GetGroups()
    {
        return _allCommands.Where(c => c.Group.HasValue).Select(c => c.Group!.Value).Distinct();
    }

    public void Clear()
    {
        _commandsByName.Clear();
        _aliasMap.Clear();
        _allCommands.Clear();
    }
}