using PlayerComponent = SampSharp.Entities.SAMP.Player;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Provides enumeration and search capabilities for commands.
/// Used by help systems and other utilities to list available commands.
/// </summary>
public interface ICommandEnumerator
{
    /// <summary>
    /// Gets all available commands, optionally filtered by permission for a specific player.
    /// </summary>
    /// <param name="player">Optional player to filter commands by permission.</param>
    /// <returns>An enumerable of command definitions.</returns>
    IEnumerable<CommandDefinition> GetAllCommands(PlayerComponent? player = null);

    /// <summary>
    /// Gets all command groups organized hierarchically.
    /// </summary>
    /// <returns>An enumerable of command groups.</returns>
    IEnumerable<CommandGroup> GetCommandGroups();

    /// <summary>
    /// Gets all commands within a specific group, optionally filtered by permission.
    /// </summary>
    /// <param name="group">The command group to search within.</param>
    /// <param name="player">Optional player to filter commands by permission.</param>
    /// <returns>An enumerable of command definitions within the group.</returns>
    IEnumerable<CommandDefinition> GetCommandsInGroup(CommandGroup group, PlayerComponent? player = null);

    /// <summary>
    /// Finds a command by its name or alias.
    /// </summary>
    /// <param name="commandName">The command name to search for (case-insensitive by default).</param>
    /// <param name="player">Optional player to check permission for the found command.</param>
    /// <returns>The command definition if found; otherwise, null.</returns>
    CommandDefinition? FindCommand(string commandName, PlayerComponent? player = null);

    /// <summary>
    /// Searches for commands matching a query string.
    /// </summary>
    /// <param name="query">The search query.</param>
    /// <param name="player">Optional player to filter commands by permission.</param>
    /// <returns>An enumerable of matching command definitions.</returns>
    IEnumerable<CommandDefinition> SearchCommands(string query, PlayerComponent? player = null);

    /// <summary>
    /// Gets command suggestions based on a partial input (useful for "command not found" suggestions).
    /// </summary>
    /// <param name="input">The partial input text.</param>
    /// <param name="player">Optional player to filter commands by permission.</param>
    /// <returns>An enumerable of suggested command definitions.</returns>
    IEnumerable<CommandDefinition> GetSuggestions(string input, PlayerComponent? player = null);
}