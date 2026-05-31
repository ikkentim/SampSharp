namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Specifies metadata for a command parameter, such as its display name and optional parser type.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter)]
public class CommandParameterAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommandParameterAttribute"/> class.
    /// </summary>
    public CommandParameterAttribute()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandParameterAttribute"/> class with a name and optional parser type.
    /// </summary>
    /// <param name="name">The display name of the command parameter or <see langword="null"/> to use the default name.</param>
    /// <param name="parserType">The type used to parse the parameter value, or <see langword="null"/> to use the default parser.</param>
    public CommandParameterAttribute(string? name, Type? parserType = null)
    {
        Name = name;
        ParserType = parserType;
    }

    /// <summary>
    /// Gets or sets the display name of the command parameter.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the type used to parse the command parameter.
    /// </summary>
    public Type? ParserType { get; set; }
}