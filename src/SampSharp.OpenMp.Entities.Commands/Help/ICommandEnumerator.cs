namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Provides enumeration of registered commands and command groups.
/// </summary>
public interface ICommandEnumerator
{
    /// <summary>
    /// Gets the command registry used to retrieve available commands.
    /// </summary>
    ICommandRegistry Registry { get; }

    /// <summary>
    /// Gets all registered commands.
    /// </summary>
    IEnumerable<CommandEnumerator> GetAllCommands();

    /// <summary>
    /// Gets all registered command groups.
    /// </summary>
    IEnumerable<CommandGroupEnumerator> GetAllCommandGroups();

    /// <summary>
    /// Gets commands in a specific group.
    /// </summary>
    IEnumerable<CommandEnumerator> GetCommandsInGroup(CommandGroup group);

    /// <summary>
    /// Searches for commands matching a criteria.
    /// </summary>
    IEnumerable<CommandEnumerator> SearchCommands(string searchTerm);

    /// <summary>
    /// Gets a specific command by name.
    /// </summary>
    CommandEnumerator? FindCommand(string name);
}