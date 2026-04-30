using SampSharp.Entities.SAMP.Commands.Parsers;

namespace SampSharp.Entities.SAMP.Commands;

/// <summary>Describes a single parsed command parameter.</summary>
public class CommandParameterInfo
{
    /// <summary>Initializes a new instance.</summary>
    public CommandParameterInfo(string name, ICommandParameterParser parser, bool isRequired, object? defaultValue, int parameterIndex)
    {
        Name = name;
        Parser = parser;
        IsRequired = isRequired;
        DefaultValue = defaultValue;
        Index = parameterIndex;
    }

    /// <summary>Display name (used in usage messages).</summary>
    public string Name { get; }

    /// <summary>Parser that converts input text into the parameter value.</summary>
    public ICommandParameterParser Parser { get; }

    /// <summary>Required parameters must be supplied; optional ones use <see cref="DefaultValue"/>.</summary>
    public bool IsRequired { get; }

    /// <summary>Default value used when an optional parameter is omitted.</summary>
    public object? DefaultValue { get; }

    /// <summary>Index of this parameter inside the invoker's args array.</summary>
    public int Index { get; }
}
