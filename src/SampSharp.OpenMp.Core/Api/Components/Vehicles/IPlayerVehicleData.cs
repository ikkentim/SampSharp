namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IPlayerVehicleData" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtension))]
public readonly partial struct IPlayerVehicleData
{
    /// <inheritdoc />
    public static UID ExtensionId => new(0xa960485be6c70fb2);

    /// <summary>
    /// Gets the vehicle the player is currently in.
    /// </summary>
    /// <returns>The vehicle the player is in, or null if not in a vehicle.</returns>
    public partial IVehicle GetVehicle();

    /// <summary>
    /// Resets the player's vehicle data internally.
    /// </summary>
    /// <remarks>
    /// TODO: Clarify the exact purpose and usage of this method.
    /// </remarks>
    public partial void ResetVehicle();

    /// <summary>
    /// Gets the seat the player is currently occupying in a vehicle.
    /// </summary>
    /// <returns>The seat index, or -1 if the player is not in a vehicle.</returns>
    public partial int GetSeat();

    /// <summary>
    /// Checks if the player is in a mod shop.
    /// </summary>
    /// <returns><see langword="true" /> if the player is in a mod shop; otherwise, <see langword="false" />.</returns>
    public partial bool IsInModShop();

    /// <summary>
    /// Checks if the player is in drive-by mode.
    /// </summary>
    /// <returns><see langword="true" /> if the player is in drive-by mode; otherwise, <see langword="false" />.</returns>
    public partial bool IsInDriveByMode();

    /// <summary>
    /// Checks if the player is cuffed.
    /// </summary>
    /// <returns><see langword="true" /> if the player is cuffed; otherwise, <see langword="false" />.</returns>
    public partial bool IsCuffed();
}