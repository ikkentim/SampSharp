namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Represents a command group in enumeration results.
/// </summary>
public class CommandGroupEnumerator
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommandGroupEnumerator" /> class.
    /// </summary>
    /// <param name="name">The group name or path (space-separated parts).</param>
    /// <param name="group">The command group object.</param>
    /// <param name="commands">The list of commands in this group.</param>
    /// <param name="subgroups">The list of subgroups under this group.</param>
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