using System.Numerics;

namespace SampSharp.Entities.SAMP;

/// <summary>Provides functionality for getting information about vehicle models and components.</summary>
public interface IVehicleInfoService
{
    /// <summary>Gets the car mod type of the specified <paramref name="componentId" />.</summary>
    /// <param name="componentId">The identifier of the component.</param>
    /// <returns>The car mod type of the component.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the specified <paramref name="componentId" /> is invalid.</exception>
    CarModType GetComponentType(int componentId);

    /// <summary>Gets information of type specified by <paramref name="infoType" /> for the specified <paramref name="vehicleModel" />.</summary>
    /// <param name="vehicleModel">The model of the vehicle.</param>
    /// <param name="infoType">The type of information to get.</param>
    /// <returns>The information about the vehicle model.</returns>
    Vector3 GetModelInfo(VehicleModelType vehicleModel, VehicleModelInfoType infoType);

    /// <summary>
    /// Returns a value indicating whether the specified <paramref name="componentId" /> is valid for the specified <paramref name="vehicleModel" />.
    /// </summary>
    /// <param name="vehicleModel">The model of the vehicle.</param>
    /// <param name="componentId">The component to check.</param>
    /// <returns><see langword="true" /> if the component is valid for the vehicle model; <see langword="false" /> otherwise.</returns>
    public bool IsValidComponentForVehicle(VehicleModelType vehicleModel, int componentId);

    /// <summary>
    /// Gets a set of random colors for the specified <paramref name="vehicleModel" /> as they would naturally appear in the game.
    /// </summary>
    /// <param name="vehicleModel">The model of the vehicle.</param>
    /// <returns>The vehicle colors (color 1 - 4)</returns>
    public (int, int, int, int) GetRandomVehicleColor(VehicleModelType vehicleModel);

    /// <summary>
    /// Gets the <see cref="Color" /> representation of the specified <paramref name="vehicleColor" />.
    /// </summary>
    /// <param name="vehicleColor">The vehicle color.</param>
    /// <param name="alpha">The alpha value of the result.</param>
    /// <returns>A <see cref="Color" /> value representing the specified vehicle color.</returns>
    public Color GetColorFromVehicleColor(int vehicleColor, uint alpha = 0xff);

    /// <summary>
    /// Gets the number of passenger seats in the specified <paramref name="vehicleModel" />.
    /// </summary>
    /// <param name="vehicleModel">The model of the vehicle.</param>
    /// <returns>The number of passenger seats excluding the driver seat.</returns>
    public int GetPassengerSeatCount(VehicleModelType vehicleModel);
}