namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Represents a command in enumeration results.
/// </summary>
public class CommandEnumerator
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommandEnumerator" /> class.
    /// </summary>
    /// <param name="name">The command name.</param>
    /// <param name="group">The command group, if any.</param>
    /// <param name="aliases">The list of command aliases.</param>
    /// <param name="overloads">The list of command overloads with parameter information.</param>
    public CommandEnumerator(string name, CommandGroup? group, IReadOnlyList<CommandAlias> aliases, IReadOnlyList<CommandDefinition> overloads)
    {
        Name = name;
        Group = group;
        Aliases = aliases;
        Overloads = overloads;
    }

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
    public IReadOnlyList<CommandDefinition> Overloads { get; }
}