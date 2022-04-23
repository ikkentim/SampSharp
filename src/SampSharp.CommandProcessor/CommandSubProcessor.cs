using System.Collections;

namespace SampSharp.CommandProcessor;

internal class CommandSubProcessor : ICommandParser, ICommandCollection
{
    private readonly string? _prefix;

    public CommandSubProcessor(string? prefix)
    {
        _prefix = prefix;
    }

    private readonly List<ICommandParser> _commands = new();
        
    public void AddCommand(ICommandParser command)
    {
        _commands.Add(command);
    }

    public IEnumerator<ICommandParser> GetEnumerator()
    {
        return _commands.SelectMany(Enumerate)
            .GetEnumerator();
    }

    private static IEnumerable<ICommandParser> Enumerate(ICommandParser command)
    {
        if (command is ICommandCollection collection)
        {
            return collection;
        }

        return new[] { command };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public ParsedCommand Parse(CommandContext context, ReadOnlySpan<char> commandText)
    {
        if (_prefix != null)
        {
            if (commandText.Length < _prefix.Length + 1)
            {
                return ParsedCommand.ParserFailure;
            }
            
            var comparison = context.Options.IgnoreCase
                ? StringComparison.InvariantCultureIgnoreCase
                : StringComparison.InvariantCulture;

            if (!commandText.StartsWith(_prefix, comparison))
            {
                return ParsedCommand.ParserFailure;
            }

            commandText = commandText[(_prefix.Length + 1)..];

            while (!commandText.IsEmpty && char.IsWhiteSpace(commandText[0]))
            {
                commandText = commandText[1..];
            }

            if (commandText.IsEmpty)
            {
                return ParsedCommand.ParserFailure;
            }
        }

        ParsedCommand bestMatch = default;
        foreach (var cmd in _commands)
        {
            var result = cmd.Parse(context, commandText);

            if (result.Probability > bestMatch.Probability || (result.Success && !bestMatch.Success))
            {
                bestMatch = result;
            }
        }

        return bestMatch;
    }
}