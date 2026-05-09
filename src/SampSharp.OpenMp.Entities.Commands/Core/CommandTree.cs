namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// A command tree for efficient command lookup and dispatch.
/// 
/// The tree structure works as follows:
/// - Each edge is a word (command group part, command name, or alias)
/// - Nodes may contain a list of command overloads (representing a complete command)
/// - When no further edges can be matched, the remaining input is used as arguments
/// - From there, overload resolution is performed
/// 
/// Example tree:
/// Root
///   ├─ "hello" -> Node [overloads for "hello"]
///   │   ├─ "world" -> Node [overloads for "hello world"]
///   │   └─ "there" -> Node [overloads for "hello there"]
///   ├─ "admin" -> Node
///   │   ├─ "kick" -> Node [overloads for "admin kick"]
///   │   └─ "ban" -> Node [overloads for "admin ban"]
///   └─ "help" -> Node [overloads for "help"]
/// </summary>
internal class CommandTree(StringComparison stringComparison)
{
    private CommandTreeNode _root = new(stringComparison);

    /// <summary>
    /// Registers a command overload in the tree by traversing from the group parts to the command name.
    /// </summary>
    /// <param name="command">The command overload to register.</param>
    /// <param name="group">The command group, or <see langword="null"/> for a top-level or alias registration.</param>
    /// <param name="name">The command name (or alias name).</param>
    public void Register(CommandDefinition command, CommandGroup? group, string name)
    {
        ArgumentNullException.ThrowIfNull(command);
        ArgumentNullException.ThrowIfNull(name);

        var current = _root;

        if (group.HasValue)
        {
            foreach (var part in group.Value.Parts)
            {
                current = current.GetOrCreateChild(part);
            }
        }

        current = current.GetOrCreateChild(name);
        current.AddCommand(command);
    }

    /// <summary>
    /// Clears all commands from the tree.
    /// </summary>
    public void Clear()
    {
        _root = new CommandTreeNode(stringComparison);
    }

    /// <summary>
    /// Attempts to find the command overloads in the tree by consuming words from <paramref name="input" />.
    /// <paramref name="input" /> is advanced past the consumed command path on success.
    /// </summary>
    /// <param name="input">
    /// The input span. On return this span starts immediately after the last consumed word.
    /// </param>
    /// <returns>The command overloads at the deepest matching node, or <see langword="null"/> if none.</returns>
    public IReadOnlyList<CommandDefinition>? FindCommand(ref StringSpan input)
    {
        var node = _root.Traverse(ref input);
        return node.Commands;
    }

    /// <summary>
    /// Gets the root node of the tree.
    /// </summary>
    internal CommandTreeNode Root => _root;
}
