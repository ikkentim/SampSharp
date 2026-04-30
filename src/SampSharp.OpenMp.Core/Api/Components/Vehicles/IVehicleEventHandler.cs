namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="IVehiclesComponent.GetEventDispatcher" />.
/// </summary>
[OpenMpEventHandler]
public partial interface IVehicleEventHandler
{
    /// <summary>
    /// Called when a vehicle is streamed in for a player.
    /// </summary>
    /// <param name="vehicle">The vehicle being streamed in.</param>
    /// <param name="player">The player for whom the vehicle is streamed in.</param>
    void OnVehicleStreamIn(IVehicle vehicle, IPlayer player);

    /// <summary>
    /// Called when a vehicle is streamed out for a player.
    /// </summary>
    /// <param name="vehicle">The vehicle being streamed out.</param>
    /// <param name="player">The player for whom the vehicle is streamed out.</param>
    void OnVehicleStreamOut(IVehicle vehicle, IPlayer player);

    /// <summary>
    /// Called when a vehicle is destroyed.
    /// </summary>
    /// <param name="vehicle">The vehicle that was destroyed.</param>
    /// <param name="player">The player associated with the destruction, if any.</param>
    void OnVehicleDeath(IVehicle vehicle, IPlayer player);

    /// <summary>
    /// Called when a player enters a vehicle.
    /// </summary>
    /// <param name="player">The player entering the vehicle.</param>
    /// <param name="vehicle">The vehicle being entered.</param>
    /// <param name="passenger"><see langword="true" /> if the player is entering as a passenger; otherwise, <see langword="false" />.</param>
    void OnPlayerEnterVehicle(IPlayer player, IVehicle vehicle, bool passenger);

    /// <summary>
    /// Called when a player exits a vehicle.
    /// </summary>
    /// <param name="player">The player exiting the vehicle.</param>
    /// <param name="vehicle">The vehicle being exited.</param>
    void OnPlayerExitVehicle(IPlayer player, IVehicle vehicle);

    /// <summary>
    /// Called when a vehicle's damage status is updated.
    /// </summary>
    /// <param name="vehicle">The vehicle whose damage status was updated.</param>
    /// <param name="player">The player associated with the update, if any.</param>
    void OnVehicleDamageStatusUpdate(IVehicle vehicle, IPlayer player);

    /// <summary>
    /// Called when a player applies a paint job to a vehicle.
    /// </summary>
    /// <param name="player">The player applying the paint job.</param>
    /// <param name="vehicle">The vehicle being painted.</param>
    /// <param name="paintJob">The paint job ID being applied.</param>
    /// <returns><see langword="true" /> if the paint job is allowed; otherwise, <see langword="false" />.</returns>
    bool OnVehiclePaintJob(IPlayer player, IVehicle vehicle, int paintJob);

    /// <summary>
    /// Called when a player modifies a vehicle.
    /// </summary>
    /// <param name="player">The player modifying the vehicle.</param>
    /// <param name="vehicle">The vehicle being modified.</param>
    /// <param name="component">The component ID being added.</param>
    /// <returns><see langword="true" /> if the modification is allowed; otherwise, <see langword="false" />.</returns>
    bool OnVehicleMod(IPlayer player, IVehicle vehicle, int component);

    /// <summary>
    /// Called when a player resprays a vehicle.
    /// </summary>
    /// <param name="player">The player respraying the vehicle.</param>
    /// <param name="vehicle">The vehicle being resprayed.</param>
    /// <param name="colour1">The primary color being applied.</param>
    /// <param name="colour2">The secondary color being applied.</param>
    /// <returns><see langword="true" /> if the respray is allowed; otherwise, <see langword="false" />.</returns>
    bool OnVehicleRespray(IPlayer player, IVehicle vehicle, int colour1, int colour2);

    /// <summary>
    /// Called when a player enters or exits a mod shop.
    /// </summary>
    /// <param name="player">The player entering or exiting the mod shop.</param>
    /// <param name="enterexit"><see langword="true" /> if the player is entering; <see langword="false" /> if exiting.</param>
    /// <param name="interiorId">The interior ID of the mod shop.</param>
    void OnEnterExitModShop(IPlayer player, bool enterexit, int interiorId);

    /// <summary>
    /// Called when a vehicle spawns.
    /// </summary>
    /// <param name="vehicle">The vehicle that spawned.</param>
    void OnVehicleSpawn(IVehicle vehicle);

    /// <summary>
    /// Called when an unoccupied vehicle is updated.
    /// </summary>
    /// <param name="vehicle">The vehicle being updated.</param>
    /// <param name="player">The player associated with the update.</param>
    /// <param name="updateData">The update data for the vehicle.</param>
    /// <returns><see langword="true" /> if the update is allowed; otherwise, <see langword="false" />.</returns>
    bool OnUnoccupiedVehicleUpdate(IVehicle vehicle, IPlayer player, UnoccupiedVehicleUpdate updateData);

    /// <summary>
    /// Called when a trailer is updated.
    /// </summary>
    /// <param name="player">The player associated with the update.</param>
    /// <param name="trailer">The trailer being updated.</param>
    /// <returns><see langword="true" /> if the update is allowed; otherwise, <see langword="false" />.</returns>
    bool OnTrailerUpdate(IPlayer player, IVehicle trailer);

    /// <summary>
    /// Called when a vehicle's siren state changes.
    /// </summary>
    /// <param name="player">The player associated with the siren state change.</param>
    /// <param name="vehicle">The vehicle whose siren state changed.</param>
    /// <param name="sirenState">The new siren state.</param>
    /// <returns><see langword="true" /> if the siren state change is allowed; otherwise, <see langword="false" />.</returns>
    bool OnVehicleSirenStateChange(IPlayer player, IVehicle vehicle, byte sirenState);
}