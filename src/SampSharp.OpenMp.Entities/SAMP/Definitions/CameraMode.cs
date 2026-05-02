namespace SampSharp.Entities.SAMP;

/// <summary>
/// Contains all camera modes.
/// </summary>
/// <remarks>See <see href="https://www.open.mp/docs/scripting/resources/cameramodes">https://www.open.mp/docs/scripting/resources/cameramodes</see>.</remarks>
public enum CameraMode
{
    /// <summary>
    /// Invalid mode.
    /// </summary>
    Invalid = -1,

    /// <summary>
    /// Camera is behind a car.
    /// </summary>
    BehindCar = 3,

    /// <summary>
    /// Camera is behind a Ped.
    /// </summary>
    FollowPed = 4,

    /// <summary>
    /// Sniper view.
    /// </summary>
    SniperAiming = 7,

    /// <summary>
    /// Rocket launcher view.
    /// </summary>
    RocketLauncherAiming = 8,

    /// <summary>
    /// Camera is set to a fixed point (e.g. after setting the player's camera position)
    /// </summary>
    Fixed = 15,

    /// <summary>
    /// Camera is in first person mode (e.g. when looking from inside the vehicle)
    /// </summary>
    FirstPerson = 16,

    /// <summary>
    /// Camera 'normally' behind a car.
    /// </summary>
    NormalCar = 18,

    /// <summary>
    /// Camera behind a boat.
    /// </summary>
    BehindBoat = 22,

    /// <summary>
    /// Camera when aiming.
    /// </summary>
    CameraWeaponAiming = 46,

    /// <summary>
    /// Heat-seeking rocket launcher view.
    /// </summary>
    HeatSeekingRocketLauncher = 51,

    /// <summary>
    /// Aiming a weapon.
    /// </summary>
    AimingWeapon = 53,

    /// <summary>
    /// Drive by view.
    /// </summary>
    VehicleDriveBy = 55,

    /// <summary>
    /// Helicopter chase view.
    /// </summary>
    HelicopterChaseCam = 56
}