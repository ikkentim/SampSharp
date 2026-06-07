namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Options for configuring the command service, shared by both player and console command services.
/// </summary>
public class CommandServiceOptions
{
    /// <summary>
    /// Gets or sets the string comparison used for command and group name matching.
    /// Defaults to <see cref="StringComparison.OrdinalIgnoreCase"/>.
    /// </summary>
    public StringComparison StringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

    /// <summary>
    /// Gets or sets the prefix displayed before a command usage message.
    /// For example, when set to "Syntax:", a usage message may be displayed as:
    /// "Syntax: /pm [targetId] [reason]".
    /// Defaults to "Usage:".
    /// </summary>
    public string UsageMessagePrefix { get; set; } = "Usage:";
}