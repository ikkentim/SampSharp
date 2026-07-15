namespace SampSharp.Entities.SAMP.Commands;

/// <summary>
/// Options for configuring the player command service.
/// </summary>
public class PlayerCommandServiceOptions : CommandServiceOptions
{
    /// <summary>
    /// Gets or sets the color used when displaying command usage messages.
    /// Defaults to <see cref="Color.White"/>.
    /// </summary>
    public Color UsageMessageColor { get; set; } = Color.White;

    /// <summary>
    /// Gets or sets the color used when displaying permission denied messages.
    /// Defaults to <see cref="Color.White"/>.
    /// </summary>
    public Color PermissionDeniedMessageColor { get; set; } = Color.White;

    /// <summary>
    /// Gets or sets the message displayed when a player attempts to execute
    /// a command without sufficient permissions.
    /// If <see langword="null"/>, no message is sent.
    /// Defaults to "You do not have permission to use this command.".
    /// </summary>
    public string? PermissionDeniedMessage { get; set; }
        = "You do not have permission to use this command.";
}