namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Represents information about a command parameter that will be parsed from user input.
/// </summary>
public class CommandParameterInfo
{
    /// <summary>Initializes a new instance.</summary>
    public CommandParameterInfo(string name, ICommandParameterParser parser, bool isRequired, object? defaultValue, int parameterIndex)
    {
        Name = name;
        Parser = parser;
        IsRequired = isRequired;
        DefaultValue = defaultValue;
        ParameterIndex = parameterIndex;
    }

    /// <summary>Gets the name of the parameter.</summary>
    public string Name { get; }

    /// <summary>Gets the parser used to parse this parameter from user input.</summary>
    public ICommandParameterParser Parser { get; }

    /// <summary>Gets whether this parameter is required.</summary>
    public bool IsRequired { get; }

    /// <summary>Gets the default value if the parameter is optional and not provided.</summary>
    public object? DefaultValue { get; }

    /// <summary>Gets the index of this parameter in the method signature (for positional lookup).</summary>
    public int ParameterIndex { get; }
}