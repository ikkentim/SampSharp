namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Default implementation of ICommandHelpProvider.
/// </summary>
public class DefaultCommandHelpProvider : ICommandHelpProvider
{
    private readonly CommandRegistry _registry;

    public DefaultCommandHelpProvider(CommandRegistry registry)
    {
        _registry = registry ?? throw new ArgumentNullException(nameof(registry));
    }

    public IEnumerable<CommandDefinition> GetAllCommands()
    {
        return _registry.GetAll();
    }

    public IEnumerable<CommandGroup> GetCommandGroups()
    {
        return _registry.GetGroups();
    }

    public IEnumerable<CommandDefinition> GetCommandsInGroup(CommandGroup group)
    {
        return _registry.GetCommandsInGroup(group);
    }

    public IEnumerable<CommandDefinition> SearchCommands(string query)
    {
        var lower = query?.ToLowerInvariant() ?? "";
        return _registry.GetAll()
            .Where(c => c.Name.Contains(lower, StringComparison.OrdinalIgnoreCase) || c.FullName.Contains(lower, StringComparison.OrdinalIgnoreCase) ||
                        c.Aliases.Any(a => a.Name.Contains(lower, StringComparison.OrdinalIgnoreCase)));
    }

    public CommandDefinition? FindCommand(string name)
    {
        return _registry.TryFind(name);
    }
}