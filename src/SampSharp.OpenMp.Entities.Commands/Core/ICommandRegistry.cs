namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Represents a registry for managing and retrieving command definitions by name, alias, or group.
/// </summary>
public interface ICommandRegistry
{
    /// <summary>Gets all registered commands.</summary>
    /// <returns>An enumerable containing all registered command definitions.</returns>
    IEnumerable<CommandDefinition> GetAll();

    /// <summary>Gets all commands in a specific <paramref name="group"/>.</summary>
    /// <param name="group">The command group to retrieve commands from.</param>
    /// <returns>An enumerable containing all command definitions in the specified <paramref name="group"/>.</returns>
    IEnumerable<CommandDefinition> GetCommandsInGroup(CommandGroup group);

    /// <summary>Gets all command groups in the specified <paramref name="group"/>.</summary>
    /// <param name="group">The parent command group whose child groups should be returned.</param>
    /// <returns>An enumerable containing all command groups in the specified <paramref name="group"/>.</returns>
    IEnumerable<CommandGroup> GetGroups(CommandGroup group);

    /// <summary>Gets all command groups.</summary>
    /// <returns>An enumerable containing all registered command groups.</returns>
    IEnumerable<CommandGroup> GetGroups();
}