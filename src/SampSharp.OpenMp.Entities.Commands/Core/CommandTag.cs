namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Represents a key-value metadata tag attached to a command.
/// Used to store custom command metadata like permissions, categories, etc.
/// </summary>
public readonly record struct CommandTag(string Key, string Value);