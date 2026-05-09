namespace SampSharp.Entities.SAMP.Commands;

internal class CommandRegistry : ICommandRegistry
{
    private readonly CommandTree _tree = new();
    private readonly List<CommandSet> _allCommands = [];
    private readonly Dictionary<string, List<CommandDefinition>> _overloadsByKey = new();
    private readonly Dictionary<string, List<CommandDefinition>> _overloadsByAliasKey = new();

    public void Register(CommandDefinition overload)
    {
        ArgumentNullException.ThrowIfNull(overload);

        var key = overload.FullName.ToLowerInvariant();

        // Add to overload list for this command
        if (!_overloadsByKey.TryGetValue(key, out var overloads))
        {
            overloads = new List<CommandDefinition>();
            _overloadsByKey[key] = overloads;
        }
        overloads.Add(overload);

        // Create or update the command definition (wrapper)
        var command = new CommandSet(overload.Name, overload.Group, _overloadsByKey[key].ToArray());

        if (overloads.Count == 1)
        {
            // First overload for this command - add to all collections
            _allCommands.Add(command);
        }
        else
        {
            // Additional overload - update existing command in all collections
            var existingIndex = _allCommands.FindIndex(c => c.Name == command.Name && c.Group == command.Group);
            if (existingIndex >= 0)
            {
                _allCommands[existingIndex] = command;
            }
        }

        // Always register in the tree (this updates the reference when adding new overloads)
        _tree.Register(command);

        // Register aliases - accumulate overloads per alias key so multiple overloads sharing
        // an alias are all reachable via that alias (consistent with canonical command behaviour).
        foreach (var alias in overload.Aliases)
        {
            var aliasKey = alias.Name.ToLowerInvariant();

            if (!_overloadsByAliasKey.TryGetValue(aliasKey, out var aliasOverloads))
            {
                aliasOverloads = new List<CommandDefinition>();
                _overloadsByAliasKey[aliasKey] = aliasOverloads;
            }
            aliasOverloads.Add(overload);

            var aliasCommand = new CommandSet(alias.Name, null, aliasOverloads.ToArray());
            _tree.RegisterAlias(alias.Name, aliasCommand);
        }
    }

    // Internal method for dispatcher: get command group by path
    // Advances 'input' past the consumed command path words.
    internal CommandSet? GetCommandGroupByPath(ref StringSpan input)
    {
        return _tree.FindCommand(ref input);
    }

    // Internal method to get all overloads for a command
    internal CommandSet? GetCommand(string? nameOrAlias)
    {
        if (string.IsNullOrWhiteSpace(nameOrAlias))
        {
            return null;
        }

        // Look up as a single word in the tree (covers both commands and aliases)
        var span = StringSpan.For(nameOrAlias.ToLowerInvariant());
        return _tree.FindCommand(ref span);
    }

    CommandDefinition? ICommandRegistry.TryFind(string nameOrAlias)
    {
        var command = GetCommand(nameOrAlias);
        return command?.Overloads.Count > 0 ? command.Overloads[0] : null;
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

        // Build a StringSpan from the joined path parts and use the tree for lookup.
        var joined = string.Join(" ", parts);
        var span = StringSpan.For(joined);
        var commandSet = _tree.FindCommand(ref span);

        // Compute how many parts were consumed by measuring consumed characters.
        var consumedChars = joined.Length - span.Length;
        var cumulativeLen = 0;
        for (var i = 0; i < parts.Count; i++)
        {
            cumulativeLen += parts[i].Length;
            if (cumulativeLen <= consumedChars)
            {
                consumedParts++;
                cumulativeLen++; // account for the separator space
            }
            else
            {
                break;
            }
        }

        if (commandSet is not null && commandSet.Overloads.Count > 0)
        {
            return commandSet.Overloads[0];
        }

        return null;
    }

    IEnumerable<CommandDefinition> ICommandRegistry.GetAll()
    {
        return _allCommands.SelectMany(c => c.Overloads);
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
        _tree.Clear();
        _allCommands.Clear();
        _overloadsByKey.Clear();
        _overloadsByAliasKey.Clear();
    }
}