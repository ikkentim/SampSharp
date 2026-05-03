using System.Collections.Generic;
using System.Linq;
using SampSharp.Entities.SAMP.Commands.Core;
using SampSharp.Entities.SAMP.Commands.Services;

namespace SampSharp.Entities.SAMP.Commands.Help;

/// <summary>
/// Provides enumeration of registered commands and command groups.
/// </summary>
public interface ICommandEnumerator
{
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

/// <summary>
/// Represents a command in enumeration results.
/// </summary>
public class CommandEnumerator
{
    /// <summary>
    /// Gets the command name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the command group, if any.
    /// </summary>
    public CommandGroup? Group { get; }

    /// <summary>
    /// Gets all aliases for this command.
    /// </summary>
    public IReadOnlyList<CommandAlias> Aliases { get; }

    /// <summary>
    /// Gets all overloads for this command.
    /// </summary>
    public IReadOnlyList<CommandOverload> Overloads { get; }

    /// <summary>
    /// Gets the permission requirements for this command.
    /// </summary>
    public IReadOnlyList<string> Permissions { get; }

    /// <summary>
    /// Gets the formatted usage message for this command.
    /// </summary>
    public string UsageMessage { get; }

    /// <summary>
    /// Gets the formatted help text for this command (including all overloads).
    /// </summary>
    public string HelpText { get; }

    public CommandEnumerator(
        string name,
        CommandGroup? group,
        IReadOnlyList<CommandAlias> aliases,
        IReadOnlyList<CommandOverload> overloads,
        IReadOnlyList<string> permissions,
        string usageMessage,
        string helpText)
    {
        Name = name;
        Group = group;
        Aliases = aliases;
        Overloads = overloads;
        Permissions = permissions;
        UsageMessage = usageMessage;
        HelpText = helpText;
    }
}

/// <summary>
/// Represents a command group in enumeration results.
/// </summary>
public class CommandGroupEnumerator
{
    /// <summary>
    /// Gets the group name/path.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the command group structure.
    /// </summary>
    public CommandGroup Group { get; }

    /// <summary>
    /// Gets all commands in this group (including subgroups).
    /// </summary>
    public IReadOnlyList<CommandEnumerator> Commands { get; }

    /// <summary>
    /// Gets all child groups (one level deep).
    /// </summary>
    public IReadOnlyList<CommandGroupEnumerator> Subgroups { get; }

    public CommandGroupEnumerator(
        string name,
        CommandGroup group,
        IReadOnlyList<CommandEnumerator> commands,
        IReadOnlyList<CommandGroupEnumerator> subgroups)
    {
        Name = name;
        Group = group;
        Commands = commands;
        Subgroups = subgroups;
    }
}
