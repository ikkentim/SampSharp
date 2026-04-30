using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents data for attaching a text label to a player or vehicle.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct TextLabelAttachmentData
{
    /// <summary>
    /// Gets the ID of the player to which the text label is attached.
    /// Default value is INVALID_PLAYER_ID.
    /// </summary>
    public readonly int PlayerId;

    /// <summary>
    /// Gets the ID of the vehicle to which the text label is attached.
    /// Default value is INVALID_VEHICLE_ID.
    /// </summary>
    public readonly int VehicleId;
}