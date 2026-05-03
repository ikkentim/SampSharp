using System;
using System.Collections.Generic;
using System.Linq;
using SampSharp.Entities.SAMP.Commands.Core;
using SampSharp.Entities.SAMP.Commands.Services;

namespace SampSharp.Entities.SAMP.Commands.Help;

/// <summary>
/// Default implementation of ICommandEnumerator using the command registry.
/// </summary>
public class DefaultCommandEnumerator : ICommandEnumerator
{
    private readonly CommandRegistry _registry;
    private readonly ICommandNameProvider _nameProvider;
    private readonly Lazy<IReadOnlyList<CommandEnumerator>> _allCommands;
    private readonly Lazy<IReadOnlyList<CommandGroupEnumerator>> _allGroups;

    public DefaultCommandEnumerator(CommandRegistry registry, ICommandNameProvider? nameProvider = null)
    {
        _registry = registry ?? throw new ArgumentNullException(nameof(registry));
        _nameProvider = nameProvider ?? new DefaultCommandNameProvider();

        _allCommands = new Lazy<IReadOnlyList<CommandEnumerator>>(() => BuildAllCommands());
        _allGroups = new Lazy<IReadOnlyList<CommandGroupEnumerator>>(() => BuildAllGroups());
    }

    public IEnumerable<CommandEnumerator> GetAllCommands() => _allCommands.Value;

    public IEnumerable<CommandGroupEnumerator> GetAllCommandGroups() => _allGroups.Value;

    public IEnumerable<CommandEnumerator> GetCommandsInGroup(CommandGroup group)
    {
        if (group == null) throw new ArgumentNullException(nameof(group));
        return _allCommands.Value.Where(c => c.Group?.Equals(group) ?? false);
    }

    public IEnumerable<CommandEnumerator> SearchCommands(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return Enumerable.Empty<CommandEnumerator>();

        var lowerTerm = searchTerm.ToLowerInvariant();
        return _allCommands.Value.Where(c =>
            c.Name.Contains(lowerTerm, StringComparison.OrdinalIgnoreCase) ||
            c.Aliases.Any(a => a.Name.Contains(lowerTerm, StringComparison.OrdinalIgnoreCase)) ||
            c.HelpText.Contains(lowerTerm, StringComparison.OrdinalIgnoreCase));
    }

    public CommandEnumerator? FindCommand(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return null;

        var definition = _registry.TryFind(name);
        return definition != null ? BuildCommandEnumerator(definition) : null;
    }

    private IReadOnlyList<CommandEnumerator> BuildAllCommands()
    {
        return _registry.GetAll()
            .Select(BuildCommandEnumerator)
            .ToList()
            .AsReadOnly();
    }

    private IReadOnlyList<CommandGroupEnumerator> BuildAllGroups()
    {
        var groups = _registry.GetGroups().ToList();
        var result = new List<CommandGroupEnumerator>();

        foreach (var group in groups)
        {
            var enumerator = BuildGroupEnumerator(group);
            if (enumerator != null)
                result.Add(enumerator);
        }

        return result.AsReadOnly();
    }

    private CommandEnumerator BuildCommandEnumerator(CommandDefinition definition)
    {
        var usageMessage = _nameProvider.GetUsageMessage(
            definition.Name,
            definition.Group?.ToString(),
            definition.Overloads.FirstOrDefault()?.ParsedParameters ?? [],
            null);

        var helpText = BuildHelpText(definition);

        return new CommandEnumerator(
            name: definition.Name,
            group: definition.Group,
            isPlayerCommand: definition.IsPlayerCommand,
            isConsoleCommand: definition.IsConsoleCommand,
            aliases: definition.Aliases.ToList().AsReadOnly(),
            overloads: definition.Overloads.ToList().AsReadOnly(),
            permissions: definition.Permissions.ToList().AsReadOnly(),
            usageMessage: usageMessage,
            helpText: helpText);
    }

    private CommandGroupEnumerator? BuildGroupEnumerator(CommandGroup group)
    {
        var commands = GetCommandsInGroup(group).ToList();
        if (commands.Count == 0)
            return null;

        var subgroups = new List<CommandGroupEnumerator>();
        var immediateSubgroups = FindImmediateSubgroups(group);

        foreach (var subgroup in immediateSubgroups)
        {
            var subgroupEnum = BuildGroupEnumerator(subgroup);
            if (subgroupEnum != null)
                subgroups.Add(subgroupEnum);
        }

        return new CommandGroupEnumerator(
            name: group.ToString(),
            group: group,
            commands: commands.AsReadOnly(),
            subgroups: subgroups.AsReadOnly());
    }

    private IEnumerable<CommandGroup> FindImmediateSubgroups(CommandGroup parentGroup)
    {
        var allGroups = _registry.GetGroups();
        var parentParts = parentGroup.Parts.Count;

        return allGroups.Where(g =>
            g.Parts.Count == parentParts + 1 &&
            g.Parts.Take(parentParts).SequenceEqual(parentGroup.Parts))
            .Distinct();
    }

    private string BuildHelpText(CommandDefinition definition)
    {
        var lines = new List<string> { $"Command: {definition.Name}" };

        if (definition.Group != null)
            lines.Add($"Group: {definition.Group}");

        if (definition.Aliases.Count > 0)
            lines.Add($"Aliases: {string.Join(", ", definition.Aliases.Select(a => a.Name))}");

        if (definition.Permissions.Count > 0)
            lines.Add($"Requires: {string.Join(", ", definition.Permissions)}");

        if (definition.Overloads.Count > 0)
        {
            lines.Add("Usage:");
            foreach (var overload in definition.Overloads)
            {
                var paramNames = string.Join(
                    " ",
                    overload.ParsedParameters.Select(p =>
                        p.IsRequired ? $"[{p.Name}]" : $"<{p.Name}>"));

                lines.Add($"  /{definition.Name}" + (paramNames.Length > 0 ? $" {paramNames}" : ""));
            }
        }

        return string.Join("\n", lines);
    }
}
