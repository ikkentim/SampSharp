namespace SampSharp.Entities.Commands;

internal class CommandTreeNode(StringComparison stringComparison)
{
    private readonly Dictionary<string, CommandTreeNode> _children = new(StringComparer.FromComparison(stringComparison));
    private List<CommandDefinition>? _commands;

    public IReadOnlyList<CommandDefinition>? Commands => _commands;

    public IReadOnlyDictionary<string, CommandTreeNode> Children => _children;

    public void AddCommand(CommandDefinition command)
    {
        (_commands ??= []).Add(command);
    }

    public CommandTreeNode GetOrCreateChild(string word)
    {
        if (!_children.TryGetValue(word, out var node))
        {
            node = new CommandTreeNode(stringComparison);
            _children[word] = node;
        }

        return node;
    }

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

    private bool TryGetChild(ReadOnlySpan<char> word, out CommandTreeNode node)
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
}
