namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Represents a command group in enumeration results.
/// </summary>
public class CommandGroupEnumerator
{
    public CommandGroupEnumerator(string name, CommandGroup group, IReadOnlyList<CommandEnumerator> commands, IReadOnlyList<CommandGroupEnumerator> subgroups)
    {
        Name = name;
        Group = group;
        Commands = commands;
        Subgroups = subgroups;
    }

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
}