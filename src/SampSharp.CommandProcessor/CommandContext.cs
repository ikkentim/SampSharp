namespace SampSharp.CommandProcessor
{
    // requirements
    // generic
    // overloads
    // usage messages
    // help commands
    // localization

    public record CommandContext(ICommandProcessor CommandProcessor, CommandProcessorOptions Options, object? UserContext);
}