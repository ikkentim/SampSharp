namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents events related to vehicle SCM (Scriptable Content Management).
/// </summary>
public enum VehicleSCMEvent : uint
{
    /// <summary>
    /// Event for setting a paint job on a vehicle.
    /// </summary>
    SetPaintjob = 1,

    /// <summary>
    /// Event for adding a component to a vehicle.
    /// </summary>
    AddComponent,

    /// <summary>
    /// Event for setting the color of a vehicle.
    /// </summary>
    SetColour,

    /// <summary>
    /// Event for entering or exiting a mod shop.
    /// </summary>
    EnterExitModShop
}