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
}