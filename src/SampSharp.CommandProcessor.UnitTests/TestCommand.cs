using System;

namespace SampSharp.CommandProcessor.UnitTests;

public class TestCommand : ICommand
{
    private readonly Action<CommandContext, ParsedCommand>? _execute;

    public TestCommand(TestCommandParser parser, Action<CommandContext, ParsedCommand>? execute)
    {
        Parser = parser;
        _execute = execute;
    }
    
    public TestCommandParser Parser { get; }

    public bool Execute(CommandContext context, ParsedCommand parsedCommand)
    {
        _execute?.Invoke(context, parsedCommand);
        return true;
    }
}