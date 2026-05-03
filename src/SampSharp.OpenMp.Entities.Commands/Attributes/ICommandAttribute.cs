namespace SampSharp.Entities.SAMP.Commands.Attributes;

/// <summary>
/// Base interface for command attributes. Implemented by [PlayerCommand] and [ConsoleCommand].
/// </summary>
public interface ICommandAttribute
{
    /// <summary>The command name (optional; derives from method name if not specified).</summary>
    string? Name { get; }
}
