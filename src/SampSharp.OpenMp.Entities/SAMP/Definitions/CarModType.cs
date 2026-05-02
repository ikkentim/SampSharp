using System.Diagnostics.CodeAnalysis;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Contains all modification types of vehicles.
/// </summary>
/// <remarks>See <see href="https://www.open.mp/docs/scripting/resources/carcomponentid">https://www.open.mp/docs/scripting/resources/carcomponentid</see>.</remarks>
[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "CommentTypo")]
public enum CarModType
{
    /// <summary>
    /// Car spoiler.
    /// </summary>
    Spoiler = 0,

    /// <summary>
    /// Car hood.
    /// </summary>
    Hood = 1,

    /// <summary>
    /// Car roof.
    /// </summary>
    Roof = 2,

    /// <summary>
    /// Car sideskirts.
    /// </summary>
    Sideskirt = 3,

    /// <summary>
    /// Car lamps.
    /// </summary>
    Lamps = 4,

    /// <summary>
    /// Nitrogen.
    /// </summary>
    Nitro = 5,

    /// <summary>
    /// Car exhaust.
    /// </summary>
    Exhaust = 6,

    /// <summary>
    /// Car wheels.
    /// </summary>
    Wheels = 7,

    /// <summary>
    /// Car stereo.
    /// </summary>
    Stereo = 8,

    /// <summary>
    /// Car hydraulics.
    /// </summary>
    Hydraulics = 9,

    /// <summary>
    /// Front car bumper.
    /// </summary>
    FrontBumper = 10,

    /// <summary>
    /// Rear car bumper.
    /// </summary>
    RearBumper = 11,

    /// <summary>
    /// Right car vent.
    /// </summary>
    VentRight = 12,

    /// <summary>
    /// Left car vent.
    /// </summary>
    VentLeft = 13
}