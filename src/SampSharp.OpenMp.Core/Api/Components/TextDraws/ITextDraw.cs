namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="ITextDraw" /> interface.
/// </summary>
[OpenMpApi(typeof(ITextDrawBase))]
public readonly partial struct ITextDraw
{
    /// <summary>
    /// Shows the text draw for the specified player.
    /// </summary>
    /// <param name="player">The player for whom the text draw will be shown.</param>
    public partial void ShowForPlayer(IPlayer player);

    /// <summary>
    /// Hides the text draw for the specified player.
    /// </summary>
    /// <param name="player">The player for whom the text draw will be hidden.</param>
    public partial void HideForPlayer(IPlayer player);

    /// <summary>
    /// Determines whether the text draw is shown for the specified player.
    /// </summary>
    /// <param name="player">The player to check.</param>
    /// <returns><see langword="true" /> if the text draw is shown for the player; otherwise, <see langword="false" />.</returns>
    public partial bool IsShownForPlayer(IPlayer player);

    /// <summary>
    /// Sets the text of the text draw for the specified player.
    /// </summary>
    /// <param name="player">The player for whom the text will be set.</param>
    /// <param name="text">The new text of the text draw.</param>
    public partial void SetTextForPlayer(IPlayer player, string text);
}