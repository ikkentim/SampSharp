using System.Numerics;
using System.Runtime.InteropServices.Marshalling;
using SampSharp.OpenMp.Core.RobinHood;
using SampSharp.OpenMp.Core.Std;
using SampSharp.OpenMp.Core.Std.Chrono;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IVehicle" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtensible), typeof(IEntity))]
public readonly partial struct IVehicle
{
    /// <summary>
    /// Sets the spawn data for the vehicle.
    /// </summary>
    /// <param name="data">The spawn data to set.</param>
    public partial void SetSpawnData(ref VehicleSpawnData data);

    /// <summary>
    /// Retrieves the spawn data for the vehicle.
    /// </summary>
    /// <param name="data">The output parameter to store the spawn data.</param>
    private partial void GetSpawnData(out VehicleSpawnData data);

    /// <summary>
    /// Gets the spawn data for the vehicle.
    /// </summary>
    /// <returns>The spawn data of the vehicle.</returns>
    public VehicleSpawnData GetSpawnData()
    {
        GetSpawnData(out var data);
        return data;
    }

    /// <summary>
    /// Checks if the vehicle is streamed in for a specific player.
    /// </summary>
    /// <param name="player">The player to check.</param>
    /// <returns>True if the vehicle is streamed in for the player; otherwise, false.</returns>
    public partial bool IsStreamedInForPlayer(IPlayer player);

    /// <summary>
    /// Streams the vehicle in for a specific player.
    /// </summary>
    /// <param name="player">The player for whom to stream the vehicle.</param>
    public partial void StreamInForPlayer(IPlayer player);

    /// <summary>
    /// Streams the vehicle out for a specific player.
    /// </summary>
    /// <param name="player">The player for whom to stream the vehicle out.</param>
    public partial void StreamOutForPlayer(IPlayer player);

    /// <summary>
    /// Sets the primary and secondary colors of the vehicle.
    /// </summary>
    /// <param name="col1">The primary color.</param>
    /// <param name="col2">The secondary color.</param>
    public partial void SetColour(int col1, int col2);

    /// <summary>
    /// Gets the primary and secondary colors of the vehicle.
    /// </summary>
    /// <param name="result">The output parameter to store the colors.</param>
    public partial void GetColour(out Pair<int, int> result);

    /// <summary>
    /// Gets the primary and secondary colors of the vehicle.
    /// </summary>
    /// <returns>A tuple containing the primary and secondary colors.</returns>
    public (int, int) GetColour()
    {
        GetColour(out var result);
        return result;
    }

    /// <summary>
    /// Sets the health of the vehicle.
    /// </summary>
    /// <param name="health">The health value to set.</param>
    public partial void SetHealth(float health);

    /// <summary>
    /// Gets the health of the vehicle.
    /// </summary>
    /// <returns>The health value of the vehicle.</returns>
    public partial float GetHealth();

    /// <summary>
    /// Updates the vehicle state based on driver synchronization data.
    /// </summary>
    /// <param name="vehicleSync">The driver synchronization data.</param>
    /// <param name="player">The player associated with the synchronization.</param>
    /// <returns>True if the update was successful; otherwise, false.</returns>
    public partial bool UpdateFromDriverSync(ref VehicleDriverSyncPacket vehicleSync, IPlayer player);

    /// <summary>
    /// Updates the vehicle state based on passenger synchronization data.
    /// </summary>
    /// <param name="passengerSync">The passenger synchronization data.</param>
    /// <param name="player">The player associated with the synchronization.</param>
    /// <returns>True if the update was successful; otherwise, false.</returns>
    public partial bool UpdateFromPassengerSync(ref VehiclePassengerSyncPacket passengerSync, IPlayer player);

    /// <summary>
    /// Updates the vehicle state based on unoccupied synchronization data.
    /// </summary>
    /// <param name="unoccupiedSync">The unoccupied synchronization data.</param>
    /// <param name="player">The player associated with the synchronization.</param>
    /// <returns>True if the update was successful; otherwise, false.</returns>
    public partial bool UpdateFromUnoccupied(ref VehicleUnoccupiedSyncPacket unoccupiedSync, IPlayer player);

    /// <summary>
    /// Updates the vehicle state based on trailer synchronization data.
    /// </summary>
    /// <param name="unoccupiedSync">The trailer synchronization data.</param>
    /// <param name="player">The player associated with the synchronization.</param>
    /// <returns>True if the update was successful; otherwise, false.</returns>
    public partial bool UpdateFromTrailerSync(ref VehicleTrailerSyncPacket unoccupiedSync, IPlayer player);

    /// <summary>
    /// Gets the players for whom the vehicle is streamed in.
    /// </summary>
    /// <returns>A set of players for whom the vehicle is streamed in.</returns>
    public partial FlatPtrHashSet<IPlayer> StreamedForPlayers();

    /// <summary>
    /// Gets the driver of the vehicle.
    /// </summary>
    /// <returns>The player who is the driver of the vehicle.</returns>
    public partial IPlayer GetDriver();

    /// <summary>
    /// Gets the passengers of the vehicle.
    /// </summary>
    /// <returns>A set of players who are passengers in the vehicle.</returns>
    public partial FlatPtrHashSet<IPlayer> GetPassengers();

    /// <summary>
    /// Sets the license plate text for the vehicle.
    /// </summary>
    /// <param name="plate">The license plate text to set.</param>
    public partial void SetPlate(string plate);

    /// <summary>
    /// Gets the license plate text of the vehicle.
    /// </summary>
    /// <returns>The license plate text of the vehicle.</returns>
    public partial string GetPlate();

    /// <summary>
    /// Sets the damage status of the vehicle.
    /// </summary>
    /// <param name="panelStatus">The status of the vehicle's panels.</param>
    /// <param name="doorStatus">The status of the vehicle's doors.</param>
    /// <param name="lightStatus">The status of the vehicle's lights.</param>
    /// <param name="tyreStatus">The status of the vehicle's tyres.</param>
    /// <param name="vehicleUpdater">The player updating the vehicle's damage status. Optional.</param>
    public partial void SetDamageStatus(int panelStatus, int doorStatus, byte lightStatus, byte tyreStatus, IPlayer vehicleUpdater = default);

    /// <summary>
    /// Gets the damage status of the vehicle.
    /// </summary>
    /// <param name="panelStatus">The output parameter for the status of the vehicle's panels.</param>
    /// <param name="doorStatus">The output parameter for the status of the vehicle's doors.</param>
    /// <param name="lightStatus">The output parameter for the status of the vehicle's lights.</param>
    /// <param name="tyreStatus">The output parameter for the status of the vehicle's tyres.</param>
    public partial void GetDamageStatus(out int panelStatus, out int doorStatus, out int lightStatus, out int tyreStatus);

    /// <summary>
    /// Sets the paint job of the vehicle.
    /// </summary>
    /// <param name="paintjob">The paint job ID to set.</param>
    public partial void SetPaintJob(int paintjob);

    /// <summary>
    /// Gets the paint job of the vehicle.
    /// </summary>
    /// <returns>The paint job ID of the vehicle.</returns>
    public partial int GetPaintJob();

    /// <summary>
    /// Adds a component to the vehicle.
    /// </summary>
    /// <param name="component">The component ID to add.</param>
    public partial void AddComponent(int component);

    /// <summary>
    /// Gets the component in a specific slot of the vehicle.
    /// </summary>
    /// <param name="slot">The slot ID to check.</param>
    /// <returns>The component ID in the specified slot.</returns>
    public partial int GetComponentInSlot(int slot);

    /// <summary>
    /// Removes a component from the vehicle.
    /// </summary>
    /// <param name="component">The component ID to remove.</param>
    public partial void RemoveComponent(int component);

    /// <summary>
    /// Places a player in a specific seat of the vehicle.
    /// </summary>
    /// <param name="player">The player to place in the vehicle.</param>
    /// <param name="seatID">The seat ID to place the player in.</param>
    public partial void PutPlayer(IPlayer player, int seatID);

    /// <summary>
    /// Sets the Z angle (rotation) of the vehicle.
    /// </summary>
    /// <param name="angle">The Z angle to set.</param>
    public partial void SetZAngle(float angle);

    /// <summary>
    /// Gets the Z angle (rotation) of the vehicle.
    /// </summary>
    /// <returns>The Z angle of the vehicle.</returns>
    public partial float GetZAngle();

    /// <summary>
    /// Sets the parameters of the vehicle.
    /// </summary>
    /// <param name="parameters">The parameters to set.</param>
    public partial void SetParams(ref VehicleParams parameters);

    /// <summary>
    /// Sets the parameters of the vehicle for a specific player.
    /// </summary>
    /// <param name="player">The player for whom to set the parameters.</param>
    /// <param name="parameters">The parameters to set.</param>
    public partial void SetParamsForPlayer(IPlayer player, ref VehicleParams parameters);

    private partial void GetParams(out VehicleParams parameters);

    /// <summary>
    /// Gets the parameters of the vehicle.
    /// </summary>
    /// <returns>The parameters of the vehicle.</returns>
    public VehicleParams GetParams()
    {
        GetParams(out var parameters);
        return parameters;
    }

    /// <summary>
    /// Checks if the vehicle is dead.
    /// </summary>
    /// <returns>True if the vehicle is dead; otherwise, false.</returns>
    public partial bool IsDead();

    /// <summary>
    /// Respawns the vehicle.
    /// </summary>
    public partial void Respawn();

    /// <summary>
    /// Gets the respawn delay of the vehicle.
    /// </summary>
    /// <returns>The respawn delay of the vehicle.</returns>
    [return: MarshalUsing(typeof(SecondsMarshaller))]
    public partial TimeSpan GetRespawnDelay();

    /// <summary>
    /// Sets the respawn delay of the vehicle.
    /// </summary>
    /// <param name="delay">The respawn delay to set.</param>
    public partial void SetRespawnDelay([MarshalUsing(typeof(SecondsMarshaller))]TimeSpan delay);

    /// <summary>
    /// Checks if the vehicle is respawning.
    /// </summary>
    /// <returns>True if the vehicle is respawning; otherwise, false.</returns>
    public partial bool IsRespawning();

    /// <summary>
    /// Sets the interior ID of the vehicle.
    /// </summary>
    /// <param name="interiorID">The interior ID to set.</param>
    public partial void SetInterior(int interiorID);

    /// <summary>
    /// Gets the interior ID of the vehicle.
    /// </summary>
    /// <returns>The interior ID of the vehicle.</returns>
    public partial int GetInterior();

    /// <summary>
    /// Attaches a trailer to the vehicle.
    /// </summary>
    /// <param name="trailer">The trailer to attach.</param>
    public partial void AttachTrailer(IVehicle trailer);

    /// <summary>
    /// Detaches the trailer from the vehicle.
    /// </summary>
    public partial void DetachTrailer();

    /// <summary>
    /// Checks if the vehicle has a trailer attached.
    /// </summary>
    /// <returns>True if the vehicle has a trailer attached; otherwise, false.</returns>
    public partial bool IsTrailer();

    /// <summary>
    /// Gets the trailer attached to the vehicle.
    /// </summary>
    /// <returns>The trailer attached to the vehicle.</returns>
    public partial IVehicle GetTrailer();

    /// <summary>
    /// Gets the cab of the vehicle.
    /// </summary>
    /// <returns>The cab of the vehicle.</returns>
    public partial IVehicle GetCab();

    /// <summary>
    /// Repairs the vehicle.
    /// </summary>
    public partial void Repair();

    /// <summary>
    /// Adds a carriage to the vehicle.
    /// </summary>
    /// <param name="carriage">The carriage to add.</param>
    /// <param name="pos">The position of the carriage.</param>
    public partial void AddCarriage(IVehicle carriage, int pos);

    /// <summary>
    /// Updates the position and velocity of a carriage.
    /// </summary>
    /// <param name="pos">The position of the carriage.</param>
    /// <param name="veloc">The velocity of the carriage.</param>
    public partial void UpdateCarriage(Vector3 pos, Vector3 veloc);

    /// <summary>
    /// Gets the carriages attached to the vehicle.
    /// </summary>
    /// <returns>The carriages attached to the vehicle.</returns>
    public partial ref CarriagesArray GetCarriages();

    /// <summary>
    /// Sets the velocity of the vehicle.
    /// </summary>
    /// <param name="velocity">The velocity to set.</param>
    public partial void SetVelocity(Vector3 velocity);

    /// <summary>
    /// Gets the velocity of the vehicle.
    /// </summary>
    /// <returns>The velocity of the vehicle.</returns>
    public partial Vector3 GetVelocity();

    /// <summary>
    /// Sets the angular velocity of the vehicle.
    /// </summary>
    /// <param name="velocity">The angular velocity to set.</param>
    public partial void SetAngularVelocity(Vector3 velocity);

    /// <summary>
    /// Gets the angular velocity of the vehicle.
    /// </summary>
    /// <returns>The angular velocity of the vehicle.</returns>
    public partial Vector3 GetAngularVelocity();

    /// <summary>
    /// Gets the model ID of the vehicle.
    /// </summary>
    /// <returns>The model ID of the vehicle.</returns>
    public partial int GetModel();

    /// <summary>
    /// Gets the state of the vehicle's landing gear.
    /// </summary>
    /// <returns>The state of the landing gear.</returns>
    public partial byte GetLandingGearState();

    /// <summary>
    /// Checks if the vehicle has been occupied.
    /// </summary>
    /// <returns>True if the vehicle has been occupied; otherwise, false.</returns>
    public partial bool HasBeenOccupied();

    /// <summary>
    /// Gets the last time the vehicle was occupied.
    /// </summary>
    /// <returns>The last occupied time of the vehicle.</returns>
    public partial ref TimePoint GetLastOccupiedTime();

    /// <summary>
    /// Gets the last time the vehicle was spawned.
    /// </summary>
    /// <returns>The last spawn time of the vehicle.</returns>
    public partial ref TimePoint GetLastSpawnTime();

    /// <summary>
    /// Checks if the vehicle is currently occupied.
    /// </summary>
    /// <returns>True if the vehicle is occupied; otherwise, false.</returns>
    public partial bool IsOccupied();

    /// <summary>
    /// Sets the siren status of the vehicle.
    /// </summary>
    /// <param name="status">The siren status to set.</param>
    public partial void SetSiren(bool status);

    /// <summary>
    /// Gets the siren state of the vehicle.
    /// </summary>
    /// <returns>The siren state of the vehicle.</returns>
    public partial byte GetSirenState();

    /// <summary>
    /// Gets the hydra thrust angle of the vehicle.
    /// </summary>
    /// <returns>The hydra thrust angle of the vehicle.</returns>
    public partial uint GetHydraThrustAngle();

    /// <summary>
    /// Gets the train speed of the vehicle.
    /// </summary>
    /// <returns>The train speed of the vehicle.</returns>
    public partial float GetTrainSpeed();

    /// <summary>
    /// Gets the pool ID of the last driver of the vehicle.
    /// </summary>
    /// <returns>The pool ID of the last driver.</returns>
    public partial int GetLastDriverPoolID();
}