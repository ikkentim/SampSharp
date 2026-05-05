using PlayerComponent = SampSharp.Entities.SAMP.Player;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Default implementation of command enumeration and search.
/// </summary>
public class DefaultCommandEnumerator : ICommandEnumerator
{
    private readonly IPermissionChecker _permissionChecker;
    private readonly CommandRegistry _registry;

    public DefaultCommandEnumerator(CommandRegistry registry, IPermissionChecker permissionChecker)
    {
        _registry = registry ?? throw new ArgumentNullException(nameof(registry));
        _permissionChecker = permissionChecker ?? throw new ArgumentNullException(nameof(permissionChecker));
    }

    public IEnumerable<CommandDefinition> GetAllCommands(PlayerComponent? player = null)
    {
        return _registry.GetAll().Where(cmd => player == null || _permissionChecker.HasPermission(player, cmd));
    }

    public IEnumerable<CommandGroup> GetCommandGroups()
    {
        return _registry.GetGroups();
    }

    public IEnumerable<CommandDefinition> GetCommandsInGroup(CommandGroup group, PlayerComponent? player = null)
    {
        return _registry.GetCommandsInGroup(group).Where(cmd => player == null || _permissionChecker.HasPermission(player, cmd));
    }

    public CommandDefinition? FindCommand(string commandName, PlayerComponent? player = null)
    {
        var cmd = _registry.TryFind(commandName);
        if (cmd == null)
        {
            return null;
        }

        if (player != null && !_permissionChecker.HasPermission(player, cmd))
        {
            return null;
        }

        return cmd;
    }

    public IEnumerable<CommandDefinition> SearchCommands(string query, PlayerComponent? player = null)
    {
        var lowerQuery = query?.ToLowerInvariant() ?? "";
        return _registry.GetAll()
            .Where(cmd => cmd.Name.Contains(lowerQuery, StringComparison.OrdinalIgnoreCase) || cmd.FullName.Contains(lowerQuery, StringComparison.OrdinalIgnoreCase) ||
                          cmd.Aliases.Any(a => a.Name.Contains(lowerQuery, StringComparison.OrdinalIgnoreCase)))
            .Where(cmd => player == null || _permissionChecker.HasPermission(player, cmd));
    }

    public IEnumerable<CommandDefinition> GetSuggestions(string input, PlayerComponent? player = null)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return Enumerable.Empty<CommandDefinition>();
        }

        var lowerInput = input.ToLowerInvariant();
        return _registry.GetAll()
            .Where(cmd => cmd.Name.StartsWith(lowerInput, StringComparison.OrdinalIgnoreCase) ||
                          cmd.Aliases.Any(a => a.Name.StartsWith(lowerInput, StringComparison.OrdinalIgnoreCase)))
            .Where(cmd => player == null || _permissionChecker.HasPermission(player, cmd))
            .OrderBy(cmd => cmd.Name);
    }
}