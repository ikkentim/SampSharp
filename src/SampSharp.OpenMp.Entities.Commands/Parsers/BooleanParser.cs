namespace SampSharp.Entities.SAMP.Commands;

/// <summary>Parses a single <see cref="bool" /> word.</summary>
public class BooleanParser : ICommandParameterParser
{
    private readonly WordParser _wordParser = new();

    /// <inheritdoc />
    public bool TryParse(IServiceProvider services, ref string inputText, out object? result)
    {
        if (!_wordParser.TryParse(services, ref inputText, out var sub) || sub is not string word)
        {
            result = null;
            return false;
        }

        word = word.ToLowerInvariant();
        if (word == "true" || word == "1" || word == "yes" || word == "on")
        {
            result = true;
            return true;
        }

        if (word == "false" || word == "0" || word == "no" || word == "off")
        {
            result = false;
            return true;
        }

        result = null;
        return false;
    }
}
