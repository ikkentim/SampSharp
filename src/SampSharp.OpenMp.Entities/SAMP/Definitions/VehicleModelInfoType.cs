namespace SampSharp.Entities.SAMP;

/// <summary>
/// Contains all vehicle model information types.
/// </summary>
public enum VehicleModelInfoType
{
    /// <summary>
    /// Vehicle size
    /// </summary>
    Size = 1,

    /// <summary>
    /// Position of the front seat. (calculated from the center of the vehicle)
    /// </summary>
    FrontSeat = 2,

    /// <summary>
    /// Position of the rear seat. (calculated from the center of the vehicle)
    /// </summary>
    RearSeat = 3,

    /// <summary>
    /// Position of the fuel cap. (calculated from the center of the vehicle)
    /// </summary>
    PetrolCap = 4,

    /// <summary>
    /// Position of the front wheels. (calculated from the center of the vehicle)
    /// </summary>
    WheelsFront = 5,

    /// <summary>
    /// Position of the rear wheels. (calculated from the center of the vehicle)
    /// </summary>
    WheelsRear = 6,

    /// <summary>
    /// Position of the middle wheels, applies to vehicles with 3 axes. (calculated from the center of the vehicle)
    /// </summary>
    WheelsMiddle = 7,

    /// <summary>
    /// Height of the front bumper. (calculated from the center of the vehicle)
    /// </summary>
    FrontBumperZ = 8,

    /// <summary>
    /// Height of the rear bumper. (calculated from the center of the vehicle)
    /// </summary>
    RearBumperZ = 9
}