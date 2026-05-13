namespace SampSharp.Entities.Commands;

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

    public void Clear()
    {
        _root = new CommandTreeNode(stringComparison);
    }

    public IReadOnlyList<CommandDefinition>? FindCommands(ref StringSpan input)
    {
        var node = _root.Traverse(ref input);
        return node.Commands;
    }
}
