namespace SampSharp.Entities.SAMP.Commands;

/// <summary>Provides additional info for a command parameter.</summary>
[AttributeUsage(AttributeTargets.Parameter)]
public class CommandParameterAttribute : Attribute
{
    /// <summary>Override for the displayed parameter name (used in usage messages).</summary>
    public string? Name { get; set; }

    /// <summary>Override parser type. Must implement <see cref="Parsers.ICommandParameterParser" /> and have a parameterless ctor.</summary>
    public Type? Parser { get; set; }
}
