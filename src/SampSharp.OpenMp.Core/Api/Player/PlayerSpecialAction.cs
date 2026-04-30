namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the special actions a player can perform.
/// </summary>
public enum PlayerSpecialAction
{
    /// <summary>
    /// No special action is being performed.
    /// </summary>
    None,

    /// <summary>
    /// The player is ducking.
    /// </summary>
    Duck,

    /// <summary>
    /// The player is using a jetpack.
    /// </summary>
    Jetpack,

    /// <summary>
    /// The player is entering a vehicle.
    /// </summary>
    EnterVehicle,

    /// <summary>
    /// The player is exiting a vehicle.
    /// </summary>
    ExitVehicle,

    /// <summary>
    /// The player is performing the first dance animation.
    /// </summary>
    Dance1,

    /// <summary>
    /// The player is performing the second dance animation.
    /// </summary>
    Dance2,

    /// <summary>
    /// The player is performing the third dance animation.
    /// </summary>
    Dance3,

    /// <summary>
    /// The player is performing the fourth dance animation.
    /// </summary>
    Dance4,

    /// <summary>
    /// The player has their hands up.
    /// </summary>
    HandsUp = 10,

    /// <summary>
    /// The player is using a cellphone.
    /// </summary>
    Cellphone,

    /// <summary>
    /// The player is sitting.
    /// </summary>
    Sitting,

    /// <summary>
    /// The player has stopped using a cellphone.
    /// </summary>
    StopCellphone,

    /// <summary>
    /// The player is drinking beer.
    /// </summary>
    Beer = 20,

    /// <summary>
    /// The player is smoking.
    /// </summary>
    Smoke,

    /// <summary>
    /// The player is drinking wine.
    /// </summary>
    Wine,

    /// <summary>
    /// The player is drinking Sprunk.
    /// </summary>
    Sprunk,

    /// <summary>
    /// The player is cuffed.
    /// </summary>
    Cuffed,

    /// <summary>
    /// The player is carrying something.
    /// </summary>
    Carry,

    /// <summary>
    /// The player is performing the pissing action.
    /// </summary>
    Pissing = 68
}