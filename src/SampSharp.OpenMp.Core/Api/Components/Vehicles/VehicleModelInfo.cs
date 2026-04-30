using System.Numerics;
using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents detailed information about a vehicle model.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct VehicleModelInfo
{
    /// <summary>
    /// Gets the size of the vehicle model.
    /// </summary>
    public readonly Vector3 Size;

    /// <summary>
    /// Gets the position of the front seat.
    /// </summary>
    public readonly Vector3 FrontSeat;

    /// <summary>
    /// Gets the position of the rear seat.
    /// </summary>
    public readonly Vector3 RearSeat;

    /// <summary>
    /// Gets the position of the petrol cap.
    /// </summary>
    public readonly Vector3 PetrolCap;

    /// <summary>
    /// Gets the position of the front wheel.
    /// </summary>
    public readonly Vector3 FrontWheel;

    /// <summary>
    /// Gets the position of the rear wheel.
    /// </summary>
    public readonly Vector3 RearWheel;

    /// <summary>
    /// Gets the position of the mid wheel.
    /// </summary>
    public readonly Vector3 MidWheel;

    /// <summary>
    /// Gets the Z position of the front bumper.
    /// </summary>
    public readonly float FrontBumperZ;

    /// <summary>
    /// Gets the Z position of the rear bumper.
    /// </summary>
    public readonly float RearBumperZ;
}