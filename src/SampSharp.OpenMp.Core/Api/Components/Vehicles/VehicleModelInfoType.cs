namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Specifies the type of information available for a vehicle model.
/// </summary>
public enum VehicleModelInfoType
{
    /// <summary>
    /// Represents the size of the vehicle model.
    /// </summary>
    Size = 1,

    /// <summary>
    /// Represents the position of the front seat.
    /// </summary>
    FrontSeat,

    /// <summary>
    /// Represents the position of the rear seat.
    /// </summary>
    RearSeat,

    /// <summary>
    /// Represents the position of the petrol cap.
    /// </summary>
    PetrolCap,

    /// <summary>
    /// Represents the position of the front wheels.
    /// </summary>
    WheelsFront,

    /// <summary>
    /// Represents the position of the rear wheels.
    /// </summary>
    WheelsRear,

    /// <summary>
    /// Represents the position of the mid wheels.
    /// </summary>
    WheelsMid,

    /// <summary>
    /// Represents the Z position of the front bumper.
    /// </summary>
    FrontBumperZ,

    /// <summary>
    /// Represents the Z position of the rear bumper.
    /// </summary>
    RearBumperZ
}