namespace SampSharp.CommandProcessor
{
    public record CommandContext(ICommandProcessor CommandProcessor, CommandProcessorOptions Options, object? UserContext);
}