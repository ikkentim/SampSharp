using System.Numerics;
using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

#pragma warning disable CA1401

/// <summary>
/// Provides utility functions and data for vehicle operations.
/// </summary>
public static class VehicleData
{
    /// <summary>
    /// Checks if a component is valid for a specific vehicle model.
    /// </summary>
    /// <param name="vehicleModel">The vehicle model ID.</param>
    /// <param name="componentId">The component ID to check.</param>
    /// <returns><see langword="true" /> if the component is valid for the vehicle model; otherwise, <see langword="false" />.</returns>
    [DllImport("SampSharp", EntryPoint = "vehicles_isValidComponentForVehicleModel")]
    public static extern bool IsValidComponentForVehicleModel(int vehicleModel, int componentId);

    /// <summary>
    /// Gets the slot of a specific vehicle component.
    /// </summary>
    /// <param name="component">The component ID.</param>
    /// <returns>The slot ID of the component.</returns>
    [DllImport("SampSharp", EntryPoint = "vehicles_getVehicleComponentSlot")]
    public static extern int GetVehicleComponentSlot(int component);

    /// <summary>
    /// Retrieves information about a vehicle model.
    /// </summary>
    /// <param name="model">The vehicle model ID.</param>
    /// <param name="type">The type of information to retrieve.</param>
    /// <param name="outInfo">The output parameter to store the retrieved information.</param>
    /// <returns><see langword="true" /> if the information was successfully retrieved; otherwise, <see langword="false" />.</returns>
    [DllImport("SampSharp", EntryPoint = "vehicles_getVehicleModelInfo")]
    public static extern bool GetVehicleModelInfo(int model, VehicleModelInfoType type, out Vector3 outInfo);

    /// <summary>
    /// Gets random colors for a vehicle.
    /// </summary>
    /// <param name="modelId">The vehicle model ID.</param>
    /// <param name="colour1">The output parameter for the first color.</param>
    /// <param name="colour2">The output parameter for the second color.</param>
    /// <param name="colour3">The output parameter for the third color.</param>
    /// <param name="colour4">The output parameter for the fourth color.</param>
    [DllImport("SampSharp", EntryPoint = "vehicles_getRandomVehicleColour")]
    public static extern void GetRandomVehicleColour(int modelId, out int colour1, out int colour2, out int colour3, out int colour4);

    /// <summary>
    /// Converts a car color index to a color value.
    /// </summary>
    /// <param name="index">The color index.</param>
    /// <param name="alpha">The alpha value of the color. Default is 0xFF.</param>
    /// <returns>The color value corresponding to the index.</returns>
    [DllImport("SampSharp", EntryPoint = "vehicles_carColourIndexToColour")]
    public static extern Colour CarColourIndexToColour(int index, uint alpha = 0xFF);

    /// <summary>
    /// Gets the number of passenger seats for a vehicle model.
    /// </summary>
    /// <param name="model">The vehicle model ID.</param>
    /// <returns>The number of passenger seats in the vehicle model.</returns>
    [DllImport("SampSharp", EntryPoint = "vehicles_getVehiclePassengerSeats")]
    public static extern byte GetVehiclePassengerSeats(int model);
}
#pragma warning restore CA1401