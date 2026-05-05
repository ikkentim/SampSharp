namespace SampSharp.Entities.SAMP.Commands.Help;

/// <summary>
/// Default implementation of ICommandHelpProvider.
/// </summary>
public class DefaultCommandHelpProvider : ICommandHelpProvider
{
    private readonly ICommandRegistry _registry;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultCommandHelpProvider" /> class with the specified command registry.
    /// </summary>
    /// <param name="registry">The command registry to provide help information for.</param>
    public DefaultCommandHelpProvider(ICommandRegistry registry)
    {
        ArgumentNullException.ThrowIfNull(registry);

        _registry = registry;
    }

    /// <inheritdoc />
    public IEnumerable<CommandDefinition> GetAllCommands()
    {
        return _registry.GetAll();
    }

    /// <inheritdoc />
    public IEnumerable<CommandGroup> GetCommandGroups()
    {
        return _registry.GetGroups();
    }

    /// <inheritdoc />
    public IEnumerable<CommandDefinition> GetCommandsInGroup(CommandGroup group)
    {
        return _registry.GetCommandsInGroup(group);
    }

    /// <inheritdoc />
    public IEnumerable<CommandDefinition> SearchCommands(string query)
    {
        var lower = query?.ToLowerInvariant() ?? "";
        return _registry.GetAll()
            .Where(c => c.Name.Contains(lower, StringComparison.OrdinalIgnoreCase) || c.FullName.Contains(lower, StringComparison.OrdinalIgnoreCase) ||
                        c.Overloads.SelectMany(o => o.Aliases).Any(a => a.Name.Contains(lower, StringComparison.OrdinalIgnoreCase)));
    }

    /// <inheritdoc />
    public CommandDefinition? FindCommand(string name)
    {
        return _registry.TryFind(name);
    }
}