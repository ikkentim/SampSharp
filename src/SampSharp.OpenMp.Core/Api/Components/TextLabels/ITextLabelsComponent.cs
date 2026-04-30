using System.Numerics;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="ITextLabelsComponent" /> interface.
/// </summary>
[OpenMpApi(typeof(IPoolComponent<ITextLabel>))]
public readonly partial struct ITextLabelsComponent
{
    /// <inheritdoc />
    public static UID ComponentId => new(0xa0c57ea80a009742);

    /// <summary>
    /// Creates a new text label with the specified properties.
    /// </summary>
    /// <param name="text">The text to display in the label.</param>
    /// <param name="colour">The color of the text label.</param>
    /// <param name="pos">The position of the text label.</param>
    /// <param name="drawDist">The draw distance for the text label.</param>
    /// <param name="vw">The virtual world in which the text label is visible.</param>
    /// <param name="los">A value indicating whether the text label requires line of sight to be visible.</param>
    /// <returns>The created text label.</returns>
    public partial ITextLabel Create(string text, Colour colour, Vector3 pos, float drawDist, int vw, bool los);

    /// <summary>
    /// Creates a new text label attached to a player with the specified properties.
    /// </summary>
    /// <param name="text">The text to display in the label.</param>
    /// <param name="colour">The color of the text label.</param>
    /// <param name="pos">The position of the text label.</param>
    /// <param name="drawDist">The draw distance for the text label.</param>
    /// <param name="vw">The virtual world in which the text label is visible.</param>
    /// <param name="los">A value indicating whether the text label requires line of sight to be visible.</param>
    /// <param name="attach">The player to which the text label is attached.</param>
    /// <returns>The created text label.</returns>
    [OpenMpApiOverload("_player")]
    public partial ITextLabel Create(string text, Colour colour, Vector3 pos, float drawDist, int vw, bool los, IPlayer attach);

    /// <summary>
    /// Creates a new text label attached to a vehicle with the specified properties.
    /// </summary>
    /// <param name="text">The text to display in the label.</param>
    /// <param name="colour">The color of the text label.</param>
    /// <param name="pos">The position of the text label.</param>
    /// <param name="drawDist">The draw distance for the text label.</param>
    /// <param name="vw">The virtual world in which the text label is visible.</param>
    /// <param name="los">A value indicating whether the text label requires line of sight to be visible.</param>
    /// <param name="attach">The vehicle to which the text label is attached.</param>
    /// <returns>The created text label.</returns>
    [OpenMpApiOverload("_vehicle")]
    public partial ITextLabel Create(string text, Colour colour, Vector3 pos, float drawDist, int vw, bool los, IVehicle attach);
}