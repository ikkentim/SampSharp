namespace SampSharp.CommandProcessor;

public record struct ParsedCommand(ICommand? Command, bool Success, int Probability, object? Data)
{
    public static readonly ParsedCommand ParserFailure = new(null, false, 0, null);
}