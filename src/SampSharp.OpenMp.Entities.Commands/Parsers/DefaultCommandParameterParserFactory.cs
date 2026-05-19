using System.Reflection;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Default implementation that creates parsers for standard types.
/// </summary>
public class DefaultCommandParameterParserFactory : ICommandParameterParserFactory
{
    /// <inheritdoc />
    public virtual ICommandParameterParser? CreateParser(ParameterInfo[] parameters, int index)
    {
        ArgumentNullException.ThrowIfNull(parameters);
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, parameters.Length);

        var param = parameters[index];
        var paramType = param.ParameterType;
        var effectiveType = Nullable.GetUnderlyingType(paramType) ?? paramType;

        // Standard scalar types
        if (effectiveType == typeof(int))
        {
            return new IntParser();
        }

        if (effectiveType == typeof(float))
        {
            return new FloatParser();
        }

        if (effectiveType == typeof(double))
        {
            return new DoubleParser();
        }

        if (effectiveType == typeof(bool))
        {
            return new BooleanParser();
        }

        // String: use StringParser for last parameter, WordParser otherwise
        if (paramType == typeof(string))
        {
            return index == parameters.Length - 1 ? new StringParser() : new WordParser();
        }

        // Player/EntityId
        if (paramType == typeof(Player) || paramType == typeof(EntityId))
        {
            return new PlayerParser();
        }

        // Enum
        if (effectiveType.IsEnum)
        {
            return new EnumParser(effectiveType);
        }

        // No parser for this type - will be treated as DI
        return null;
    }
}
