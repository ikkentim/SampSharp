namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Represents a registry for managing and retrieving command definitions by name, alias, or group.
/// </summary>
public interface ICommandRegistry
{
    /// <summary>Gets all registered commands.</summary>
    IEnumerable<CommandDefinition> GetAll();

    /// <summary>Gets all commands in a specific group.</summary>
    IEnumerable<CommandDefinition> GetCommandsInGroup(CommandGroup group);

    /// <summary>Gets all command groups.</summary>
    IEnumerable<CommandGroup> GetGroups();
}