using System.Globalization;

namespace SampSharp.Entities.SAMP.Commands.Parsers;

/// <summary>Parses a single <see cref="double" /> word (invariant culture).</summary>
public class DoubleParser : ICommandParameterParser
{
    private readonly WordParser _wordParser = new();

    /// <inheritdoc />
    public bool TryParse(IServiceProvider services, ref string inputText, out object? result)
    {
        if (!_wordParser.TryParse(services, ref inputText, out var sub) || sub is not string word
            || !double.TryParse(word, NumberStyles.Float, CultureInfo.InvariantCulture, out var num))
        {
            result = null;
            return false;
        }
        result = num;
        return true;
    }
}
