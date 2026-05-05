namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Represents a registry for managing and retrieving command definitions by name, alias, or group.
/// </summary>
public interface ICommandRegistry
{
    /// <summary>Tries to find a command by name or alias (case-insensitive).</summary>
    CommandDefinition? TryFind(string nameOrAlias);

    /// <summary>Tries to find a command by its full path, matching command groups or aliases.</summary>
    CommandDefinition? TryFindByPath(IEnumerable<string> pathParts);

    /// <summary>
    /// Tries to find a command by its full path, matching command groups or aliases.
    /// Returns how many path parts were consumed.
    /// </summary>
    CommandDefinition? TryFindByPath(IEnumerable<string> pathParts, out int consumedParts);

    /// <summary>Gets all registered commands.</summary>
    IReadOnlyList<CommandDefinition> GetAll();

    /// <summary>Gets all commands in a specific group.</summary>
    IEnumerable<CommandDefinition> GetCommandsInGroup(CommandGroup group);

    /// <summary>Gets all command groups.</summary>
    IEnumerable<CommandGroup> GetGroups();
}