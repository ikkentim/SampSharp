using System.Reflection;
using SampSharp.Entities.SAMP.Commands.Parsers;

namespace SampSharp.Entities.SAMP.Commands.Core.Scanning;

/// <summary>
/// Default implementation that creates parsers for standard types.
/// </summary>
public class DefaultCommandParameterParserFactory : ICommandParameterParserFactory
{
    public ICommandParameterParser? CreateParser(ParameterInfo[] parameters, int index)
    {
        var param = parameters[index];
        var paramType = param.ParameterType;

        // Standard scalar types
        if (paramType == typeof(int))
        {
            return new IntParser();
        }

        if (paramType == typeof(float))
        {
            return new FloatParser();
        }

        if (paramType == typeof(double))
        {
            return new DoubleParser();
        }

        if (paramType == typeof(bool))
        {
            return new BooleanParser();
        }

        // String: use StringParser for last parameter, WordParser otherwise
        if (paramType == typeof(string))
        {
            return index == parameters.Length - 1 ? new StringParser() : new WordParser();
        }

        // Player/EntityId
        if (paramType == typeof(SampSharp.Entities.SAMP.Player) || paramType == typeof(EntityId))
        {
            return new PlayerParser();
        }

        // Enum
        if (paramType.IsEnum)
        {
            return new EnumParser(paramType);
        }

        // No parser for this type - will be treated as DI
        return null;
    }
}