using System.Numerics;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IPlayerTextDrawData" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtension), typeof(IPool<IPlayerTextDraw>))]
public readonly partial struct IPlayerTextDrawData
{
    /// <inheritdoc />
    public static UID ExtensionId => new(0xbf08495682312400);

    /// <summary>
    /// Begins the selection of text draws for the player.
    /// </summary>
    /// <param name="highlight">The highlight color to use during selection.</param>
    public partial void BeginSelection(Colour highlight);

    /// <summary>
    /// Determines whether the player is currently selecting text draws.
    /// </summary>
    /// <returns><see langword="true" /> if the player is selecting text draws; otherwise, <see langword="false" />.</returns>
    public partial bool IsSelecting();

    /// <summary>
    /// Ends the selection of text draws for the player.
    /// </summary>
    public partial void EndSelection();

    /// <summary>
    /// Creates a new player-specific text draw with the specified position and text.
    /// </summary>
    /// <param name="position">The position of the text draw.</param>
    /// <param name="text">The text of the text draw.</param>
    /// <returns>The created player-specific text draw.</returns>
    public partial IPlayerTextDraw Create(Vector2 position, string text);

    /// <summary>
    /// Creates a new player-specific text draw with the specified position and preview model.
    /// </summary>
    /// <param name="position">The position of the text draw.</param>
    /// <param name="model">The preview model of the text draw.</param>
    /// <returns>The created player-specific text draw.</returns>
    [OpenMpApiOverload("_model")]
    public partial IPlayerTextDraw Create(Vector2 position, int model);

    /// <summary>
    /// Gets the pool interface for managing player-specific text draws.
    /// </summary>
    /// <returns>A pool interface for player-specific text draws.</returns>
    public IPool<IPlayerTextDraw> AsPool()
    {
        return (IPool<IPlayerTextDraw>)this;
    }
}