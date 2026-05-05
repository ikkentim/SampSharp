namespace SampSharp.Entities.SAMP.Commands;

/// <summary>Parses a single <see cref="int" /> word.</summary>
public class IntParser : ICommandParameterParser
{
    private readonly WordParser _wordParser = new();

    /// <inheritdoc />
    public bool TryParse(IServiceProvider services, ref string inputText, out object? result)
    {
        if (!_wordParser.TryParse(services, ref inputText, out var sub) || sub is not string word || !int.TryParse(word, out var num))
        {
            result = null;
            return false;
        }

        result = num;
        return true;
    }
}