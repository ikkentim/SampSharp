using System.Globalization;

namespace SampSharp.Entities.SAMP.Commands.Parsers;

/// <summary>Parses a single <see cref="float" /> word (invariant culture).</summary>
public class FloatParser : ICommandParameterParser
{
    private readonly WordParser _wordParser = new();

    /// <inheritdoc />
    public bool TryParse(IServiceProvider services, ref string inputText, out object? result)
    {
        if (!_wordParser.TryParse(services, ref inputText, out var sub) || sub is not string word
            || !float.TryParse(word, NumberStyles.Float, CultureInfo.InvariantCulture, out var num))
        {
            result = null;
            return false;
        }
        result = num;
        return true;
    }
}
