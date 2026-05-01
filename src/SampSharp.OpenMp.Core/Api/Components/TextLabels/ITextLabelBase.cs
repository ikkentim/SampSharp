using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="ITextLabelBase" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtensible), typeof(IEntity))]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public readonly partial struct ITextLabelBase
{
    /// <summary>
    /// Sets the text content of the text label.
    /// </summary>
    /// <param name="text">The new text to display.</param>
    public partial void SetText(string text);

    /// <summary>
    /// Gets the text content of the text label.
    /// </summary>
    /// <returns>The text currently displayed on the text label.</returns>
    public partial string GetText();

    /// <summary>
    /// Sets the colour of the text label.
    /// </summary>
    /// <param name="colour">The colour to set.</param>
    public partial void SetColour(Colour colour);

    /// <summary>
    /// Gets the colour of the text label.
    /// </summary>
    /// <param name="colour">When the method returns, contains the colour of the text label.</param>
    public partial void GetColour(out Colour colour);

    /// <summary>
    /// Gets the colour of the text label.
    /// </summary>
    /// <returns>The colour of the text label.</returns>
    public Colour GetColour()
    {
        GetColour(out var result);
        return result;
    }

    /// <summary>
    /// Sets the draw distance for the text label.
    /// </summary>
    /// <param name="dist">The draw distance in units.</param>
    public partial void SetDrawDistance(float dist);

    /// <summary>
    /// Gets the draw distance of the text label.
    /// </summary>
    /// <returns>The draw distance in units.</returns>
    public partial float GetDrawDistance();

    /// <summary>
    /// Attaches the text label to a player.
    /// </summary>
    /// <param name="player">The player to attach the text label to.</param>
    /// <param name="offset">The offset from the player's position.</param>
    public partial void AttachToPlayer(IPlayer player, Vector3 offset);

    /// <summary>
    /// Attaches the text label to a vehicle.
    /// </summary>
    /// <param name="vehicle">The vehicle to attach the text label to.</param>
    /// <param name="offset">The offset from the vehicle's position.</param>
    public partial void AttachToVehicle(IVehicle vehicle, Vector3 offset);

    /// <summary>
    /// Gets the attachment data of the text label.
    /// </summary>
    /// <returns>A reference to the <see cref="TextLabelAttachmentData" /> structure containing the attachment information.</returns>
    public partial ref TextLabelAttachmentData GetAttachmentData();

    /// <summary>
    /// Detaches the text label from the player it was attached to.
    /// </summary>
    /// <param name="position">The new position for the text label after detachment.</param>
    public partial void DetachFromPlayer(Vector3 position);

    /// <summary>
    /// Detaches the text label from the vehicle it was attached to.
    /// </summary>
    /// <param name="position">The new position for the text label after detachment.</param>
    public partial void DetachFromVehicle(Vector3 position);

    /// <summary>
    /// Sets whether line-of-sight testing is enabled for the text label.
    /// </summary>
    /// <param name="status"><c>true</c> to enable line-of-sight testing; <c>false</c> to disable it.</param>
    public partial void SetTestLOS(bool status);

    /// <summary>
    /// Gets whether line-of-sight testing is enabled for the text label.
    /// </summary>
    /// <returns><c>true</c> if line-of-sight testing is enabled; otherwise, <c>false</c>.</returns>
    public partial bool GetTestLOS();

    /// <summary>
    /// Sets both the colour and text of the text label in a single operation.
    /// </summary>
    /// <param name="colour">The colour to set.</param>
    /// <param name="text">The text to set.</param>
    public partial void SetColourAndText(Colour colour, string text);
}