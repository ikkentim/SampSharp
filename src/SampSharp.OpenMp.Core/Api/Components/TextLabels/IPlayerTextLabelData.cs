using System.Numerics;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IPlayerTextLabelData" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtension), typeof(IPool<IPlayerTextLabel>))]
public readonly partial struct IPlayerTextLabelData
{
    /// <inheritdoc />
    public static UID ExtensionId => new(0xb9e2bd0dc5148c3c);

    /// <summary>
    /// Creates a new per-player text label.
    /// </summary>
    /// <param name="text">The text to display on the label.</param>
    /// <param name="colour">The colour of the text label.</param>
    /// <param name="pos">The position where the text label will be created.</param>
    /// <param name="drawDist">The draw distance of the text label.</param>
    /// <param name="los">Whether line-of-sight testing is enabled.</param>
    /// <returns>The created per-player text label, or <c>null</c> if creation failed.</returns>
    public partial IPlayerTextLabel Create(string text, Colour colour, Vector3 pos, float drawDist, bool los);

    /// <summary>
    /// Creates a new per-player text label attached to a player.
    /// </summary>
    /// <param name="text">The text to display on the label.</param>
    /// <param name="colour">The colour of the text label.</param>
    /// <param name="pos">The position offset from the player.</param>
    /// <param name="drawDist">The draw distance of the text label.</param>
    /// <param name="los">Whether line-of-sight testing is enabled.</param>
    /// <param name="attach">The player to attach the text label to.</param>
    /// <returns>The created per-player text label, or <c>null</c> if creation failed.</returns>
    [OpenMpApiOverload("_player")]
    public partial IPlayerTextLabel Create(string text, Colour colour, Vector3 pos, float drawDist, bool los, IPlayer attach);

    /// <summary>
    /// Creates a new per-player text label attached to a vehicle.
    /// </summary>
    /// <param name="text">The text to display on the label.</param>
    /// <param name="colour">The colour of the text label.</param>
    /// <param name="pos">The position offset from the vehicle.</param>
    /// <param name="drawDist">The draw distance of the text label.</param>
    /// <param name="los">Whether line-of-sight testing is enabled.</param>
    /// <param name="attach">The vehicle to attach the text label to.</param>
    /// <returns>The created per-player text label, or <c>null</c> if creation failed.</returns>
    [OpenMpApiOverload("_vehicle")]
    public partial IPlayerTextLabel Create(string text, Colour colour, Vector3 pos, float drawDist, bool los, IVehicle attach);

    /// <summary>
    /// Gets the pool interface for managing per-player text labels.
    /// </summary>
    /// <returns>A pool interface for per-player text labels.</returns>
    public IPool<IPlayerTextLabel> AsPool()
    {
        return (IPool<IPlayerTextLabel>)this;
    }
}