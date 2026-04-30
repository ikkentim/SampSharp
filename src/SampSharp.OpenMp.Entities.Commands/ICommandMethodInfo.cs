namespace SampSharp.Entities.SAMP.Commands;

/// <summary>Describes metadata about a command method (extracted from the attribute).</summary>
public interface ICommandMethodInfo
{
    /// <summary>The overridden command name; <see langword="null" /> = derive from method name.</summary>
    string? Name { get; }
}
