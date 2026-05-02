namespace SampSharp.Entities.SAMP;

/// <summary>
/// Contains all player states.
/// </summary>
public enum PlayerState
{
    /// <summary>
    /// No state.
    /// </summary>
    None = 0,

    /// <summary>
    /// Player is on foot.
    /// </summary>
    OnFoot = 1,

    /// <summary>
    /// Player is driving a vehicle.
    /// </summary>
    Driving = 2,

    /// <summary>
    /// Player is in a vehicle as passenger.
    /// </summary>
    Passenger = 3,

    /// <summary>
    /// Player is exiting a vehicle.
    /// </summary>
    ExitVehicle = 4,

    /// <summary>
    /// Player is entering a vehicle as driver.
    /// </summary>
    EnterVehicleDriver = 5,

    /// <summary>
    /// Player is entering a vehicle as passenger.
    /// </summary>
    EnterVehiclePassenger = 6,

    /// <summary>
    /// Player is dead.
    /// </summary>
    Wasted = 7,

    /// <summary>
    /// Player has spawned.
    /// </summary>
    Spawned = 8,

    /// <summary>
    /// Player is spectating.
    /// </summary>
    Spectating = 9
}