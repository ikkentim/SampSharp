namespace SampSharp.CommandProcessor;

public interface ICommandParser
{
    ParsedCommand Parse(CommandContext context, ReadOnlySpan<char> commandText);
}