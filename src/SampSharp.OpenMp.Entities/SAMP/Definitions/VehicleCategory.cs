using System.Diagnostics.CodeAnalysis;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Contains all vehicle categories.
/// </summary>
[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "CommentTypo")]
public enum VehicleCategory
{
    /// <summary>
    /// Airplanes.
    /// </summary>
    Airplane = 1,

    /// <summary>
    /// Helicopters.
    /// </summary>
    Helicopter = 2,

    /// <summary>
    /// Bikes.
    /// </summary>
    Bike = 3,

    /// <summary>
    /// Convertibles.
    /// </summary>
    Convertible = 4,

    /// <summary>
    /// Industrials.
    /// </summary>
    Industrial = 5,

    /// <summary>
    /// Lowriders.
    /// </summary>
    Lowrider = 6,

    /// <summary>
    /// Off Road.
    /// </summary>
    OffRoad = 7,

    /// <summary>
    /// Public Service Vehicles.
    /// </summary>
    PublicService = 8,

    /// <summary>
    /// Saloons.
    /// </summary>
    Saloon = 9,

    /// <summary>
    /// Sport Vehicles.
    /// </summary>
    Sport = 10,

    /// <summary>
    /// Station Wagons.
    /// </summary>
    Station = 11,

    /// <summary>
    /// Boats.
    /// </summary>
    Boat = 12,

    /// <summary>
    /// Trailers.
    /// </summary>
    Trailer = 13,

    /// <summary>
    /// Unique Vehicles.
    /// </summary>
    Unique = 14,

    /// <summary>
    /// RC Vehicles.
    /// </summary>
    RemoteControl = 15,

    /// <summary>
    /// Train trailers.
    /// </summary>
    TrainTrailer = 16
}