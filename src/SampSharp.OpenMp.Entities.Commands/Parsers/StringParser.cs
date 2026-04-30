namespace SampSharp.Entities.SAMP.Commands.Parsers;

/// <summary>Consumes ALL remaining input text (used for the last <see cref="string"/> parameter, e.g. chat messages).</summary>
public class StringParser : ICommandParameterParser
{
    /// <inheritdoc />
    public bool TryParse(IServiceProvider services, ref string inputText, out object? result)
    {
        inputText = inputText.TrimStart();
        if (inputText.Length == 0)
        {
            result = null;
            return false;
        }

        result = inputText;
        inputText = string.Empty;
        return true;
    }
}
