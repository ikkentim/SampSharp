namespace SampSharp.Entities.SAMP.Commands.Services;

using Parsers;

/// <summary>
/// Factory for obtaining parameter parsers for specific types.
/// </summary>
public interface ICommandParameterParserFactory
{
    /// <summary>
    /// Gets the parameter parser for the given type, if available.
    /// </summary>
    /// <param name="parameterType">The type to get a parser for.</param>
    /// <returns>The parser, or null if no parser is registered for the type.</returns>
    ICommandParameterParser? GetParser(Type parameterType);

    /// <summary>
    /// Tries to get the parameter parser for the given type.
    /// </summary>
    /// <param name="parameterType">The type to get a parser for.</param>
    /// <param name="parser">The parser, if available.</param>
    /// <returns>True if a parser was found; otherwise, false.</returns>
    bool TryGetParser(Type parameterType, out ICommandParameterParser? parser)
    {
        parser = GetParser(parameterType);
        return parser != null;
    }
}
