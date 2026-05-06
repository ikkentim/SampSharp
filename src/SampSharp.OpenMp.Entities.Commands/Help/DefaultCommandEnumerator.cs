namespace SampSharp.Entities.SAMP.Commands;

internal class DefaultCommandEnumerator : ICommandEnumerator
{
    private readonly Lazy<IReadOnlyList<CommandEnumerator>> _allCommands;
    private readonly Lazy<IReadOnlyList<CommandGroupEnumerator>> _allGroups;
    private readonly ICommandRegistry _registry;

    public DefaultCommandEnumerator(ICommandRegistry registry)
    {
        _registry = registry;

        _allCommands = new Lazy<IReadOnlyList<CommandEnumerator>>(BuildAllCommands);
        _allGroups = new Lazy<IReadOnlyList<CommandGroupEnumerator>>(BuildAllGroups);
    }

    public ICommandRegistry Registry => _registry;

    public IEnumerable<CommandEnumerator> GetAllCommands()
    {
        return _allCommands.Value;
    }

    public IEnumerable<CommandGroupEnumerator> GetAllCommandGroups()
    {
        return _allGroups.Value;
    }

    public IEnumerable<CommandEnumerator> GetCommandsInGroup(CommandGroup group)
    {
        return _allCommands.Value.Where(c => c.Group?.Equals(group) ?? false);
    }

    public IEnumerable<CommandEnumerator> SearchCommands(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return [];
        }

        var lowerTerm = searchTerm.ToLowerInvariant();
        return _allCommands.Value.Where(c =>
            c.Name.Contains(lowerTerm, StringComparison.OrdinalIgnoreCase) || 
            c.Aliases.Any(a => a.Name.Contains(lowerTerm, StringComparison.OrdinalIgnoreCase)));
    }

    public CommandEnumerator? FindCommand(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return null;
        }

        var definition = _registry.TryFind(name);
        return definition != null ? BuildCommandEnumerator(definition) : null;
    }

    private IReadOnlyList<CommandEnumerator> BuildAllCommands()
    {
        var result = new List<CommandEnumerator>();
        var allDefs = _registry.GetAll();

        // Group by name and group
        var grouped = allDefs
            .GroupBy(d => (d.Name, d.Group), new CommandKeyComparer())
            .OrderBy(g => g.Key.Name);

        foreach (var group in grouped)
        {
            var (name, grp) = group.Key;
            var overloads = group.ToList();

            // Collect aliases from all overloads
            var allAliases = overloads.SelectMany(o => o.Aliases).Distinct().ToList().AsReadOnly();

            result.Add(new CommandEnumerator(name, grp, allAliases, overloads.AsReadOnly()));
        }

        return result.AsReadOnly();
    }

    private class CommandKeyComparer : IEqualityComparer<(string Name, CommandGroup? Group)>
    {
        public bool Equals((string Name, CommandGroup? Group) x, (string Name, CommandGroup? Group) y)
        {
            return x.Name == y.Name && (x.Group?.Equals(y.Group) ?? y.Group == null);
        }

        public int GetHashCode((string Name, CommandGroup? Group) obj)
        {
            return HashCode.Combine(obj.Name, obj.Group);
        }
    }

    private CommandEnumerator BuildCommandEnumerator(CommandDefinition definition)
    {
        // Collect aliases and tags from all overloads
        var allAliases = new[] { definition }.SelectMany(o => o.Aliases).Distinct().ToList().AsReadOnly();

        return new CommandEnumerator(definition.Name, definition.Group, allAliases, new[] { definition }.AsReadOnly());
    }

    private IReadOnlyList<CommandGroupEnumerator> BuildAllGroups()
    {
        var groups = _registry.GetGroups().OrderBy(g => g.FullName).ToList();

        if (groups.Count == 0)
        {
            return [];
        }

        var result = new List<CommandGroupEnumerator>();

        foreach (var group in groups)
        {
            var enumerator = BuildGroupEnumerator(group);
            if (enumerator != null)
            {
                result.Add(enumerator);
            }
        }

        return result.AsReadOnly();
    }

    private CommandGroupEnumerator? BuildGroupEnumerator(CommandGroup group)
    {
        var commands = GetCommandsInGroup(group).ToList();
        if (commands.Count == 0)
        {
            return null;
        }

        var subgroups = new List<CommandGroupEnumerator>();
        var immediateSubgroups = FindImmediateSubgroups(group);

        foreach (var subgroup in immediateSubgroups)
        {
            var subgroupEnum = BuildGroupEnumerator(subgroup);
            if (subgroupEnum != null)
            {
                subgroups.Add(subgroupEnum);
            }
        }

        return new CommandGroupEnumerator(group.ToString(), group, commands.AsReadOnly(), subgroups.AsReadOnly());
    }

    private IEnumerable<CommandGroup> FindImmediateSubgroups(CommandGroup parentGroup)
    {
        var allGroups = _registry.GetGroups();
        var parentParts = parentGroup.Parts.Count;

        return allGroups.Where(g => g.Parts.Count == parentParts + 1 && g.Parts.Take(parentParts).SequenceEqual(parentGroup.Parts)).Distinct();
    }
}