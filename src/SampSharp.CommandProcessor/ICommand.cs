namespace SampSharp.CommandProcessor;

public interface ICommand
{
    bool Execute(CommandContext context, ParsedCommand parsedCommand);
}