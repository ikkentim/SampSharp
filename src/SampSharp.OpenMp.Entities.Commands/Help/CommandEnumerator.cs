namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Represents a command in enumeration results.
/// </summary>
public class CommandEnumerator
{
    public CommandEnumerator(string name, CommandGroup? group, IReadOnlyList<CommandAlias> aliases, IReadOnlyList<CommandOverload> overloads)
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
    public IReadOnlyList<CommandOverload> Overloads { get; }
}