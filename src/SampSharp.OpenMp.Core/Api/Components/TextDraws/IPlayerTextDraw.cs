namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IPlayerTextDraw" /> interface.
/// </summary>
[OpenMpApi(typeof(ITextDrawBase))]
public readonly partial struct IPlayerTextDraw
{
    /// <summary>
    /// Shows the player-specific text draw.
    /// </summary>
    public partial void Show();

    /// <summary>
    /// Hides the player-specific text draw.
    /// </summary>
    public partial void Hide();

    /// <summary>
    /// Determines whether the player-specific text draw is currently shown.
    /// </summary>
    /// <returns><see langword="true" /> if the text draw is shown; otherwise, <see langword="false" />.</returns>
    public partial bool IsShown();
}