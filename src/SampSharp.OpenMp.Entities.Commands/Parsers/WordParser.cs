namespace SampSharp.Entities.SAMP.Commands;

/// <summary>Consumes the next whitespace-delimited word.</summary>
public class WordParser : ICommandParameterParser
{
    /// <inheritdoc />
    public bool TryParse(IServiceProvider services, ref StringSpan inputText, out object? result)
    {
        result = null;
        inputText = inputText.TrimStart();
        if (inputText.Length == 0)
        {
            return false;
        }

        var span = inputText.AsSpan();
        var index = span.IndexOf(' ');
        if (index == 0)
        {
            return false;
        }

        var wordLength = index < 0 ? span.Length : index;
        var word = span[..wordLength].ToString();

        inputText = inputText.Skip(wordLength);
        result = word;
        return true;
    }
}