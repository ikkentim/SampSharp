using System.Reflection;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Factory for creating command parameter parsers.
/// Can be extended to support custom parameter types.
/// </summary>
public interface ICommandParameterParserFactory
{
    /// <summary>
    /// Creates a parser for the parameter at the given index, or null if the parameter should be treated as DI.
    /// </summary>
    ICommandParameterParser? CreateParser(ParameterInfo[] parameters, int index);
}