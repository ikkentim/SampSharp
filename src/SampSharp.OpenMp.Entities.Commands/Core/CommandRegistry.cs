namespace SampSharp.Entities.SAMP.Commands;

internal class CommandRegistry(StringComparison stringComparison) : ICommandRegistry
{
    private readonly CommandTree _tree = new(stringComparison);
    private readonly List<CommandDefinition> _allCommands = [];

    public void Register(CommandDefinition overload)
    {
        ArgumentNullException.ThrowIfNull(overload);

        _allCommands.Add(overload);

        // Register by canonical path (group + name)
        _tree.Register(overload, overload.Group, overload.Name);

        // Register each alias as a top-level (no group) entry
        foreach (var alias in overload.Aliases)
        {
            _tree.Register(overload, null, alias.Name);
        }
    }

    // Internal method for dispatcher: get command overloads by path.
    // Advances 'input' past the consumed command path words.
    internal IReadOnlyList<CommandDefinition>? FindCommands(ref StringSpan input)
    {
        return _tree.FindCommands(ref input);
    }

    IEnumerable<CommandDefinition> ICommandRegistry.GetAll()
    {
        return _allCommands;
    }

    IEnumerable<CommandDefinition> ICommandRegistry.GetCommandsInGroup(CommandGroup group)
    {
        return _allCommands.Where(c => c.Group.HasValue && c.Group.Value == group);
    }

    IEnumerable<CommandGroup> ICommandRegistry.GetGroups()
    {
        // TODO: add group list to registry to optimize GetGroups calls.
        return _allCommands.Where(c => c.Group.HasValue).Select(c => c.Group!.Value).Distinct();
    }

    IEnumerable<CommandGroup> ICommandRegistry.GetGroups(CommandGroup group)
    {
        return _allCommands.Where(c => c.Group.HasValue)
            .Select(c => c.Group!.Value)
            .Distinct()
            .Where(x => x.Depth > group.Depth && group.Parts.SequenceEqual(x.Parts.Take(group.Depth)));
    }

    internal void Clear()
    {
        _tree.Clear();
        _allCommands.Clear();
    }
}