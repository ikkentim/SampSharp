namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Default implementation of ICommandEnumerator using the command registry.
/// </summary>
public class DefaultCommandEnumerator : ICommandEnumerator
{
    private readonly Lazy<IReadOnlyList<CommandEnumerator>> _allCommands;
    private readonly Lazy<IReadOnlyList<CommandGroupEnumerator>> _allGroups;
    private readonly ICommandTextFormatter _textFormatter;
    private readonly ICommandRegistry _registry;

    public DefaultCommandEnumerator(ICommandRegistry registry, ICommandTextFormatter? textFormatter = null)
    {
        _registry = registry ?? throw new ArgumentNullException(nameof(registry));
        _textFormatter = textFormatter ?? new DefaultCommandTextFormatter();

        _allCommands = new Lazy<IReadOnlyList<CommandEnumerator>>(() => BuildAllCommands());
        _allGroups = new Lazy<IReadOnlyList<CommandGroupEnumerator>>(() => BuildAllGroups());
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
        if (group == null)
        {
            throw new ArgumentNullException(nameof(group));
        }

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
            c.Name.Contains(lowerTerm, StringComparison.OrdinalIgnoreCase) || c.Aliases.Any(a => a.Name.Contains(lowerTerm, StringComparison.OrdinalIgnoreCase)) ||
            c.HelpText.Contains(lowerTerm, StringComparison.OrdinalIgnoreCase));
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
        return _registry.GetAll().Select(BuildCommandEnumerator).ToList().AsReadOnly();
    }

    private IReadOnlyList<CommandGroupEnumerator> BuildAllGroups()
    {
        var groups = _registry.GetGroups().ToList();
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

    private CommandEnumerator BuildCommandEnumerator(CommandDefinition definition)
    {
        var firstOverload = definition.Overloads.FirstOrDefault();
        var usageMessage = firstOverload != null 
            ? $"Usage: {_textFormatter.FormatCommandUsage(definition.Name, definition.Group?.ToString(), firstOverload.ParsedParameters, includeSlash: true)}"
            : $"Usage: /{definition.Name}";

        var helpText = BuildHelpText(definition);

        // Collect aliases and tags from all overloads
        var allAliases = definition.Overloads.SelectMany(o => o.Aliases).Distinct().ToList().AsReadOnly();
        var allTags = definition.Overloads.SelectMany(o => o.Tags).GroupBy(t => t.Key).Select(g => g.First()).Select(t => t.Value).ToList().AsReadOnly();

        return new CommandEnumerator(definition.Name, definition.Group, allAliases, definition.Overloads.ToList().AsReadOnly(),
            allTags, usageMessage, helpText);
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

    private string BuildHelpText(CommandDefinition definition)
    {
        var lines = new List<string>
        {
            $"Command: {definition.Name}"
        };

        if (definition.Group != null)
        {
            lines.Add($"Group: {definition.Group}");
        }

        // Collect aliases from all overloads
        var allAliases = definition.Overloads.SelectMany(o => o.Aliases).Distinct().ToList();
        if (allAliases.Count > 0)
        {
            lines.Add($"Aliases: {string.Join(", ", allAliases.Select(a => a.Name))}");
        }

        if (definition.Overloads.Count > 0)
        {
            lines.Add("Usage:");
            foreach (var overload in definition.Overloads)
            {
                var paramNames = string.Join(" ", overload.ParsedParameters.Select(p => p.IsRequired ? $"[{p.Name}]" : $"<{p.Name}>"));

                lines.Add($"  /{definition.Name}" + (paramNames.Length > 0 ? $" {paramNames}" : ""));
            }
        }

        return string.Join("\n", lines);
    }
}