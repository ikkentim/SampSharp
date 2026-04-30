using System.Numerics;
using System.Runtime.InteropServices.Marshalling;
using SampSharp.OpenMp.Core.Std.Chrono;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IVehiclesComponent" /> interface.
/// </summary>
[OpenMpApi(typeof(IPoolComponent<IVehicle>))]
public readonly partial struct IVehiclesComponent
{
    /// <inheritdoc />
    public static UID ComponentId => new(0x3f1f62ee9e22ab19);

    /// <summary>
    /// Retrieves the array of vehicle models.
    /// </summary>
    /// <returns>A reference to the array of vehicle models.</returns>
    public partial ref VehicleModelsArray Models();

    /// <summary>
    /// Creates a new vehicle.
    /// </summary>
    /// <param name="isStatic"><see langword="true" /> if the vehicle is static; otherwise, <see langword="false" />.</param>
    /// <param name="modelID">The model ID of the vehicle.</param>
    /// <param name="position">The position where the vehicle will be created.</param>
    /// <param name="Z">The Z angle (rotation) of the vehicle. Default is 0.0f.</param>
    /// <param name="colour1">The primary color of the vehicle. Default is -1.</param>
    /// <param name="colour2">The secondary color of the vehicle. Default is -1.</param>
    /// <param name="respawnDelay">The respawn delay of the vehicle. Use <see cref="TimeSpan.Zero"/> or a negative value to disable respawn.</param>
    /// <param name="addSiren"><see langword="true" /> if the vehicle should have a siren; otherwise, <see langword="false" />. Default is <see langword="false" />.</param>
    /// <returns>The created vehicle.</returns>
    public partial IVehicle Create(bool isStatic, int modelID, Vector3 position, float Z = 0.0f, int colour1 = -1, int colour2 = -1, [MarshalUsing(typeof(SecondsMarshaller))] TimeSpan respawnDelay = default, bool addSiren = false);

    /// <summary>
    /// Gets the event dispatcher for vehicle events.
    /// </summary>
    /// <returns>The event dispatcher for vehicle events.</returns>
    public partial IEventDispatcher<IVehicleEventHandler> GetEventDispatcher();
}