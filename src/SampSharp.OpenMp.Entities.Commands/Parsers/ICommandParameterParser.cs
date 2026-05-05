namespace SampSharp.Entities.SAMP.Commands;

/// <summary>Parses one command parameter from chat input.</summary>
public interface ICommandParameterParser
{
    /// <summary>Tries to parse the next token from <paramref name="inputText" />.</summary>
    /// <param name="services">Service provider (used by parsers that need DI, e.g. <see cref="PlayerParser" />).</param>
    /// <param name="inputText">Remaining input text. Consumed text is removed.</param>
    /// <param name="result">Parsed value on success.</param>
    /// <returns><see langword="true" /> on success.</returns>
    bool TryParse(IServiceProvider services, ref string inputText, out object? result);
}