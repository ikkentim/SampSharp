using System;

namespace SampSharp.CommandProcessor.UnitTests;

public class TestCommandParser : ICommandParser
{
    private readonly TestCommand _instance;

    public TestCommandParser(string text, Action<CommandContext, ParsedCommand>? execute)
    {
        Text = text;
        _instance = new TestCommand(this, execute);
    }
    
    public string Text { get; }

    public ParsedCommand Parse(CommandContext context, ReadOnlySpan<char> commandText)
    {
        if (commandText.Equals(Text, StringComparison.InvariantCulture))
        {
            return new ParsedCommand(_instance, true, int.MaxValue, null);
        }

        return ParsedCommand.ParserFailure;
    }
}