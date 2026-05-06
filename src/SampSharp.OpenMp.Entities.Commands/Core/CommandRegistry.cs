namespace SampSharp.Entities.SAMP.Commands;

internal class CommandRegistry : ICommandRegistry
{
    private readonly Dictionary<string, CommandSet> _aliasMap = new();
    private readonly List<CommandSet> _allCommands = [];
    private readonly Dictionary<string, CommandSet> _commandsByName = new();
    private readonly Dictionary<string, List<CommandDefinition>> _overloadsByKey = new();

    public void Register(CommandDefinition overload)
    {
        if (overload == null)
        {
            throw new ArgumentNullException(nameof(overload));
        }

        var key = overload.FullName.ToLowerInvariant();

        // Add to overload list for this command
        if (!_overloadsByKey.ContainsKey(key))
        {
            _overloadsByKey[key] = new List<CommandDefinition>();
        }
        _overloadsByKey[key].Add(overload);

        // Create or update the command definition (wrapper)
        if (!_commandsByName.ContainsKey(key))
        {
            var command = new CommandSet(overload.Name, overload.Group, _overloadsByKey[key].ToArray());
            _commandsByName[key] = command;
            _allCommands.Add(command);
        }
        else
        {
            // Update existing command with new overload array
            var command = new CommandSet(overload.Name, overload.Group, _overloadsByKey[key].ToArray());
            var oldCmd = _commandsByName[key];
            _commandsByName[key] = command;
            var index = _allCommands.IndexOf(oldCmd);
            if (index >= 0)
            {
                _allCommands[index] = command;
            }
        }

        // Register aliases from this overload
        foreach (var alias in overload.Aliases)
        {
            var aliasKey = alias.Name.ToLowerInvariant();
            if (!_aliasMap.ContainsKey(aliasKey))
            {
                _aliasMap[aliasKey] = _commandsByName[key];
            }
        }
    }

    // Internal method to get command group with all overloads
    internal CommandSet? GetCommandGroup(string nameOrAlias)
    {
        if (string.IsNullOrWhiteSpace(nameOrAlias))
        {
            return null;
        }

        var key = nameOrAlias.ToLowerInvariant();

        // Try as full/short name first, otherwise as alias
        if (_commandsByName.TryGetValue(key, out var command))
        {
            return command;
        }

        return _aliasMap.GetValueOrDefault(key);
    }

    // Internal method for dispatcher: get command group by path
    internal CommandSet? GetCommandGroupByPath(IEnumerable<string> pathParts, out int consumedParts)
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
            if (_commandsByName.TryGetValue(partial, out var command))
            {
                consumedParts = i;
                return command;
            }

            // Also check aliases
            if (_aliasMap.TryGetValue(partial, out var aliased))
            {
                consumedParts = i;
                return aliased;
            }
        }

        return null;
    }

    // Internal method to get all overloads for a command
    internal CommandSet? GetCommand(string nameOrAlias)
    {
        if (string.IsNullOrWhiteSpace(nameOrAlias))
        {
            return null;
        }

        var key = nameOrAlias.ToLowerInvariant();

        // Try as full/short name first, otherwise as alias
        if (_commandsByName.TryGetValue(key, out var command))
        {
            return command;
        }

        return _aliasMap.GetValueOrDefault(key);
    }

    CommandDefinition? ICommandRegistry.TryFind(string nameOrAlias)
    {
        var command = GetCommand(nameOrAlias);
        return command != null ? command.Overloads.FirstOrDefault() : null;
    }

    CommandDefinition? ICommandRegistry.TryFindByPath(IEnumerable<string> pathParts)
    {
        return ((ICommandRegistry)this).TryFindByPath(pathParts, out _);
    }

    CommandDefinition? ICommandRegistry.TryFindByPath(IEnumerable<string> pathParts, out int consumedParts)
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
            if (_commandsByName.TryGetValue(partial, out var command))
            {
                consumedParts = i;
                return command.Overloads.FirstOrDefault();
            }

            // Also check aliases
            if (_aliasMap.TryGetValue(partial, out var aliased))
            {
                consumedParts = i;
                return aliased.Overloads.FirstOrDefault();
            }
        }

        return null;
    }

    IReadOnlyList<CommandDefinition> ICommandRegistry.GetAll()
    {
        return _allCommands.SelectMany(c => c.Overloads).ToList().AsReadOnly();
    }

    IEnumerable<CommandDefinition> ICommandRegistry.GetCommandsInGroup(CommandGroup group)
    {
        return _allCommands.Where(c => c.Group == group).SelectMany(c => c.Overloads);
    }

    IEnumerable<CommandGroup> ICommandRegistry.GetGroups()
    {
        return _allCommands.Where(c => c.Group.HasValue).Select(c => c.Group!.Value).Distinct();
    }

    internal void Clear()
    {
        _commandsByName.Clear();
        _aliasMap.Clear();
        _allCommands.Clear();
        _overloadsByKey.Clear();
    }
}