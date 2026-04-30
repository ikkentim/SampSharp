namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the various states a player can be in during gameplay.
/// </summary>
public enum PlayerState
{
    /// <summary>
    /// The player is not in any specific state.
    /// </summary>
    None = 0,

    /// <summary>
    /// The player is on foot.
    /// </summary>
    OnFoot = 1,

    /// <summary>
    /// The player is driving a vehicle.
    /// </summary>
    Driver = 2,

    /// <summary>
    /// The player is a passenger in a vehicle.
    /// </summary>
    Passenger = 3,

    /// <summary>
    /// The player is exiting a vehicle.
    /// </summary>
    ExitVehicle = 4,

    /// <summary>
    /// The player is entering a vehicle as the driver.
    /// </summary>
    EnterVehicleDriver = 5,

    /// <summary>
    /// The player is entering a vehicle as a passenger.
    /// </summary>
    EnterVehiclePassenger = 6,

    /// <summary>
    /// The player is wasted (dead).
    /// </summary>
    Wasted = 7,

    /// <summary>
    /// The player has spawned in the game world.
    /// </summary>
    Spawned = 8,

    /// <summary>
    /// The player is spectating another player or entity.
    /// </summary>
    Spectating = 9
}