namespace SampSharp.Entities.SAMP.Commands.Parsers;

/// <summary>Consumes the next whitespace-delimited word.</summary>
public class WordParser : ICommandParameterParser
{
    /// <inheritdoc />
    public bool TryParse(IServiceProvider services, ref string inputText, out object? result)
    {
        result = null;
        inputText = inputText.TrimStart();
        if (inputText.Length == 0) return false;

        var index = inputText.IndexOf(' ');
        if (index == 0) return false;

        var str = index < 0 ? inputText : inputText[..index];
        inputText = inputText[str.Length..];
        result = str;
        return true;
    }
}
