namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Represents a node in the command tree where edges are words.
/// Each node may contain a list of command overloads and child nodes for further word matching.
/// </summary>
internal class CommandTreeNode(StringComparison stringComparison)
{
    private readonly Dictionary<string, CommandTreeNode> _children = new(StringComparer.FromComparison(stringComparison));
    private List<CommandDefinition>? _commands;

    /// <summary>
    /// Gets the command overloads registered at this node, or <see langword="null"/> if none.
    /// </summary>
    public IReadOnlyList<CommandDefinition>? Commands => _commands;

    /// <summary>
    /// Adds a command overload to this node.
    /// </summary>
    public void AddCommand(CommandDefinition command)
    {
        (_commands ??= new List<CommandDefinition>()).Add(command);
    }

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
            node = new CommandTreeNode(stringComparison);
            _children[word] = node;
        }

        return node;
    }

    /// <summary>
    /// Tries to get the child node for a given word using a case-insensitive span comparison,
    /// without any string allocation.
    /// </summary>
    /// <param name="word">The word to look up.</param>
    /// <param name="node">The child node, if found.</param>
    /// <returns><c>true</c> if the child node was found; otherwise, <c>false</c>.</returns>
    public bool TryGetChild(ReadOnlySpan<char> word, out CommandTreeNode node)
    {
        foreach (var kvp in _children)
        {
            if (word.Equals(kvp.Key, stringComparison))
            {
                node = kvp.Value;
                return true;
            }
        }

        node = null!;
        return false;
    }

    /// <summary>
    /// Attempts to traverse the tree following words read from <paramref name="remaining" />, returning
    /// the deepest node reached. <paramref name="remaining" /> is advanced past the words that were
    /// successfully consumed; unmatched tokens are left in place.
    /// </summary>
    /// <param name="remaining">
    /// The input span to read from. On return this span starts immediately after the last consumed word.
    /// </param>
    /// <returns>The deepest node reached (may be <c>this</c> if no words matched).</returns>
    public CommandTreeNode Traverse(ref StringSpan remaining)
    {
        var current = this;

        while (true)
        {
            var trimmed = remaining.TrimStart();
            if (trimmed.Length == 0)
            {
                break;
            }

            // Read the next whitespace-delimited word without consuming it yet
            var wordLen = 0;
            while (wordLen < trimmed.Length && !char.IsWhiteSpace(trimmed[wordLen]))
            {
                wordLen++;
            }

            var wordSpan = trimmed.AsSpan()[..wordLen];

            if (current.TryGetChild(wordSpan, out var child))
            {
                current = child;
                remaining = trimmed.Skip(wordLen); // consume the word
            }
            else
            {
                break;
            }
        }

        return current;
    }
}
