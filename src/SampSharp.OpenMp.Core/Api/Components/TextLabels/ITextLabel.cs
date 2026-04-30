namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="ITextLabel" /> interface.
/// </summary>
[OpenMpApi(typeof(ITextLabelBase))]
public readonly partial struct ITextLabel
{
    /// <summary>
    /// Determines whether the text label is streamed in for the specified player.
    /// </summary>
    /// <param name="player">The player to check.</param>
    /// <returns><see langword="true" /> if the text label is streamed in for the player; otherwise, <see langword="false" />.</returns>
    public partial bool IsStreamedInForPlayer(IPlayer player);

    /// <summary>
    /// Streams the text label in for the specified player.
    /// </summary>
    /// <param name="player">The player for whom the text label will be streamed in.</param>
    public partial void StreamInForPlayer(IPlayer player);

    /// <summary>
    /// Streams the text label out for the specified player.
    /// </summary>
    /// <param name="player">The player for whom the text label will be streamed out.</param>
    public partial void StreamOutForPlayer(IPlayer player);
}