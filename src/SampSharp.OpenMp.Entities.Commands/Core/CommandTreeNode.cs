namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Represents a node in the command tree where edges are words.
/// Each node may contain a command set and child nodes for further word matching.
/// </summary>
internal class CommandTreeNode
{
    private readonly Dictionary<string, CommandTreeNode> _children = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Gets or sets the command set at this node, if this node represents a complete command.
    /// </summary>
    public CommandSet? CommandSet { get; set; }

    /// <summary>
    /// Gets the child nodes mapped by word (case-insensitive).
    /// </summary>
    public IReadOnlyDictionary<string, CommandTreeNode> Children => _children;

    /// <summary>
    /// Gets or sets the child node for a given word, creating it if it doesn't exist.
    /// </summary>
    /// <param name="word">The word (command group part, command name, or alias).</param>
    /// <returns>The child node for the given word.</returns>
    public CommandTreeNode GetOrCreateChild(string word)
    {
        if (!_children.TryGetValue(word, out var node))
        {
            node = new CommandTreeNode();
            _children[word] = node;
        }

        return node;
    }

    /// <summary>
    /// Tries to get the child node for a given word.
    /// </summary>
    /// <param name="word">The word to look up.</param>
    /// <param name="node">The child node, if found.</param>
    /// <returns><c>true</c> if the child node was found; otherwise, <c>false</c>.</returns>
    public bool TryGetChild(string word, out CommandTreeNode node)
    {
        return _children.TryGetValue(word, out node!);
    }

    /// <summary>
    /// Attempts to traverse the tree following the given path, returning the deepest node reached
    /// and the number of path segments consumed.
    /// </summary>
    /// <param name="pathSegments">The path segments to traverse.</param>
    /// <param name="consumedCount">The number of path segments consumed before the path ended.</param>
    /// <returns>The deepest node reached.</returns>
    public CommandTreeNode Traverse(IReadOnlyList<string> pathSegments, out int consumedCount)
    {
        consumedCount = 0;
        var current = this;

        foreach (var segment in pathSegments)
        {
            if (current.TryGetChild(segment, out var child) && child != null)
            {
                current = child;
                consumedCount++;
            }
            else
            {
                break;
            }
        }

        return current;
    }
}
