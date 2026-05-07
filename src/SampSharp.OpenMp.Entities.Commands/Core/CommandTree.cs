namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// A command tree for efficient command lookup and dispatch.
/// 
/// The tree structure works as follows:
/// - Each edge is a word (command group part, command name, or alias)
/// - Nodes may contain a command set (representing a complete command)
/// - When no further edges can be matched, the remaining input is used as arguments
/// - From there, overload resolution is performed
/// 
/// Example tree:
/// Root
///   ├─ "hello" -> Node [CommandSet for "hello"]
///   │   ├─ "world" -> Node [CommandSet for "hello world"]
///   │   └─ "there" -> Node [CommandSet for "hello there"]
///   ├─ "admin" -> Node
///   │   ├─ "kick" -> Node [CommandSet for "admin kick"]
///   │   └─ "ban" -> Node [CommandSet for "admin ban"]
///   └─ "help" -> Node [CommandSet for "help"]
/// </summary>
internal class CommandTree
{
    private CommandTreeNode _root = new();

    /// <summary>
    /// Registers a command by its full path into the tree.
    /// </summary>
    /// <param name="commandSet">The command set to register.</param>
    public void Register(CommandSet commandSet)
    {
        ArgumentNullException.ThrowIfNull(commandSet);

        // Register the command by its full path (group + name)
        var path = BuildPath(commandSet);
        RegisterPath(path, commandSet);
    }

    /// <summary>
    /// Registers an alias that points to a command set.
    /// </summary>
    /// <param name="aliasName">The alias name.</param>
    /// <param name="commandSet">The command set to register under this alias.</param>
    public void RegisterAlias(string aliasName, CommandSet commandSet)
    {
        ArgumentNullException.ThrowIfNull(aliasName);
        ArgumentNullException.ThrowIfNull(commandSet);

        RegisterPath([aliasName], commandSet);
    }

    /// <summary>
    /// Clears all commands from the tree.
    /// </summary>
    public void Clear()
    {
        _root = new CommandTreeNode();
    }

    /// <summary>
    /// Attempts to find a command in the tree by following the given path.
    /// Returns the command set at the deepest matching node and the number of path segments consumed.
    /// </summary>
    /// <param name="pathSegments">The path segments to match (words from the input).</param>
    /// <param name="consumedCount">The number of path segments consumed before no further match.</param>
    /// <returns>The command set found at the deepest matching node, or null if the root is reached.</returns>
    public CommandSet? FindCommand(IReadOnlyList<string> pathSegments, out int consumedCount)
    {
        var node = _root.Traverse(pathSegments, out consumedCount);
        return node.CommandSet;
    }

    /// <summary>
    /// Gets the root node of the tree.
    /// </summary>
    internal CommandTreeNode Root => _root;

    /// <summary>
    /// Builds the full path for a command by combining its group and name.
    /// </summary>
    private static string[] BuildPath(CommandSet commandSet)
    {
        var parts = new List<string>();

        if (commandSet.Group.HasValue)
        {
            parts.AddRange(commandSet.Group.Value.Parts);
        }

        parts.Add(commandSet.Name);
        return parts.ToArray();
    }

    /// <summary>
    /// Registers a path in the tree, creating intermediate nodes as needed.
    /// </summary>
    private void RegisterPath(string[] path, CommandSet commandSet)
    {
        if (path.Length == 0)
        {
            throw new ArgumentException("Path cannot be empty", nameof(path));
        }

        var current = _root;

        // Traverse/create all intermediate nodes
        for (var i = 0; i < path.Length - 1; i++)
        {
            current = current.GetOrCreateChild(path[i]);
        }

        // Register the command at the final node
        var finalNode = current.GetOrCreateChild(path[^1]);
        if (finalNode.CommandSet != null && finalNode.CommandSet != commandSet)
        {
            // Allow re-registration of the same command (happens when adding overloads)
            // but warn if a different command already exists at this location
        }

        finalNode.CommandSet = commandSet;
    }
}
