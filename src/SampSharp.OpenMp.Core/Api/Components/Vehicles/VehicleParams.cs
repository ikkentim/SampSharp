using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the parameters of a vehicle in the game.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly record struct VehicleParams
{
    /// <summary>
    /// Gets the state of the vehicle's engine.
    /// </summary>
    public readonly sbyte engine = -1;

    /// <summary>
    /// Gets the state of the vehicle's lights.
    /// </summary>
    public readonly sbyte lights = -1;

    /// <summary>
    /// Gets the state of the vehicle's alarm.
    /// </summary>
    public readonly sbyte alarm = -1;

    /// <summary>
    /// Gets the state of the vehicle's doors.
    /// </summary>
    public readonly sbyte doors = -1;

    /// <summary>
    /// Gets the state of the vehicle's bonnet.
    /// </summary>
    public readonly sbyte bonnet = -1;

    /// <summary>
    /// Gets the state of the vehicle's boot.
    /// </summary>
    public readonly sbyte boot = -1;

    /// <summary>
    /// Gets the state of the vehicle's objective.
    /// </summary>
    public readonly sbyte objective = -1;

    /// <summary>
    /// Gets the state of the vehicle's siren.
    /// </summary>
    public readonly sbyte siren = -1;

    /// <summary>
    /// Gets the state of the driver's door.
    /// </summary>
    public readonly sbyte doorDriver = -1;

    /// <summary>
    /// Gets the state of the passenger's door.
    /// </summary>
    public readonly sbyte doorPassenger = -1;

    /// <summary>
    /// Gets the state of the back-left door.
    /// </summary>
    public readonly sbyte doorBackLeft = -1;

    /// <summary>
    /// Gets the state of the back-right door.
    /// </summary>
    public readonly sbyte doorBackRight = -1;

    /// <summary>
    /// Gets the state of the driver's window.
    /// </summary>
    public readonly sbyte windowDriver = -1;

    /// <summary>
    /// Gets the state of the passenger's window.
    /// </summary>
    public readonly sbyte windowPassenger = -1;

    /// <summary>
    /// Gets the state of the back-left window.
    /// </summary>
    public readonly sbyte windowBackLeft = -1;

    /// <summary>
    /// Gets the state of the back-right window.
    /// </summary>
    public readonly sbyte windowBackRight = -1;

    /// <summary>
    /// Initializes a new instance of the <see cref="VehicleParams" /> struct.
    /// </summary>
    /// <param name="engine">The state of the engine.</param>
    /// <param name="lights">The state of the lights.</param>
    /// <param name="alarm">The state of the alarm.</param>
    /// <param name="doors">The state of the doors.</param>
    /// <param name="bonnet">The state of the bonnet.</param>
    /// <param name="boot">The state of the boot.</param>
    /// <param name="objective">The state of the objective.</param>
    /// <param name="siren">The state of the siren.</param>
    /// <param name="doorDriver">The state of the driver's door.</param>
    /// <param name="doorPassenger">The state of the passenger's door.</param>
    /// <param name="doorBackLeft">The state of the back-left door.</param>
    /// <param name="doorBackRight">The state of the back-right door.</param>
    /// <param name="windowDriver">The state of the driver's window.</param>
    /// <param name="windowPassenger">The state of the passenger's window.</param>
    /// <param name="windowBackLeft">The state of the back-left window.</param>
    /// <param name="windowBackRight">The state of the back-right window.</param>
    public VehicleParams(sbyte engine, sbyte lights, sbyte alarm, sbyte doors, sbyte bonnet, sbyte boot, sbyte objective, sbyte siren, sbyte doorDriver, sbyte doorPassenger, sbyte doorBackLeft, sbyte doorBackRight, sbyte windowDriver, sbyte windowPassenger, sbyte windowBackLeft, sbyte windowBackRight)
    {
        this.engine = engine;
        this.lights = lights;
        this.alarm = alarm;
        this.doors = doors;
        this.bonnet = bonnet;
        this.boot = boot;
        this.objective = objective;
        this.siren = siren;
        this.doorDriver = doorDriver;
        this.doorPassenger = doorPassenger;
        this.doorBackLeft = doorBackLeft;
        this.doorBackRight = doorBackRight;
        this.windowDriver = windowDriver;
        this.windowPassenger = windowPassenger;
        this.windowBackLeft = windowBackLeft;
        this.windowBackRight = windowBackRight;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="VehicleParams" /> struct with default values.
    /// </summary>
    public VehicleParams()
    {
    }

    /// <summary>
    /// Determines whether any of the vehicle parameters are set.
    /// </summary>
    /// <returns><c>true</c> if any parameter is set; otherwise, <c>false</c>.</returns>
    public bool IsSet()
    {
        return engine != -1 || lights != -1 || alarm != -1 || doors != -1 || bonnet != -1 || boot != -1 || objective != -1 || siren != -1 || doorDriver != -1 ||
               doorPassenger != -1 || doorBackLeft != -1 || doorBackRight != -1 || windowDriver != -1 || windowPassenger != -1 || windowBackLeft != -1 || windowBackRight != -1;
    }
}