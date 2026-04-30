namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Specifies the type of velocity to set for a vehicle.
/// </summary>
public enum VehicleVelocitySetType : byte
{
    /// <summary>
    /// Represents normal velocity.
    /// </summary>
    Normal = 0,

    /// <summary>
    /// Represents angular velocity.
    /// </summary>
    Angular
}