namespace SampSharp.Entities.SAMP.Commands.Help;

/// <summary>
/// Provides help information about commands for use in help commands.
/// </summary>
public interface ICommandHelpProvider
{
    /// <summary>Gets all available commands.</summary>
    IEnumerable<CommandDefinition> GetAllCommands();

    /// <summary>Gets all available command groups.</summary>
    IEnumerable<CommandGroup> GetCommandGroups();

    /// <summary>Gets all commands in a specific group.</summary>
    IEnumerable<CommandDefinition> GetCommandsInGroup(CommandGroup group);

    /// <summary>Searches for commands matching a query.</summary>
    IEnumerable<CommandDefinition> SearchCommands(string query);

    /// <summary>Finds a specific command by name.</summary>
    CommandDefinition? FindCommand(string name);
}