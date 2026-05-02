using System.Diagnostics.CodeAnalysis;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Contains all fighting styles.
/// </summary>
/// <remarks>See <see href="https://www.open.mp/docs/scripting/resources/fightingstyles">https://www.open.mp/docs/scripting/resources/fightingstyles</see>.</remarks>
[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "CommentTypo")]
public enum FightStyle : uint
{
    /// <summary>
    /// Normal fighting style.
    /// </summary>
    Normal = 4,

    /// <summary>
    /// Boxing fighting style.
    /// </summary>
    Boxing = 5,

    /// <summary>
    /// Kung fu fighting style.
    /// </summary>
    Kungfu = 6,

    /// <summary>
    /// Kneehead fighting style.
    /// </summary>
    Kneehead = 7,

    /// <summary>
    /// Grabkick fighting style.
    /// </summary>
    Grabkick = 15,

    /// <summary>
    /// Elbow fighting style.
    /// </summary>
    Elbow = 16
}