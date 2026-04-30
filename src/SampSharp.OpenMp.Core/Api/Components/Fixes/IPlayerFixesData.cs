using System.Runtime.InteropServices.Marshalling;
using SampSharp.OpenMp.Core.Std.Chrono;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IPlayerFixesData" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtension))]
public readonly partial struct IPlayerFixesData
{
    /// <inheritdoc />
    public static UID ExtensionId => new(0x672d5d6fbb094ef7);

    /// <summary>
    /// Sends game text to the player.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="time">How long to display the message.</param>
    /// <param name="style">The display style/slot for the game text.</param>
    /// <returns><c>true</c> if successful; otherwise, <c>false</c>.</returns>
    public partial bool SendGameText(string message, [MarshalUsing(typeof(MillisecondsMarshaller))] TimeSpan time, int style);

    /// <summary>
    /// Hides game text for the player.
    /// </summary>
    /// <param name="style">The display style/slot to hide.</param>
    /// <returns><c>true</c> if successful; otherwise, <c>false</c>.</returns>
    public partial bool HideGameText(int style);

    /// <summary>
    /// Checks if the player has active game text at the specified style/slot.
    /// </summary>
    /// <param name="style">The display style/slot to check.</param>
    /// <returns><c>true</c> if there is active game text at that slot; otherwise, <c>false</c>.</returns>
    public partial bool HasGameText(int style);

    /// <summary>
    /// Gets information about the game text at the specified style/slot.
    /// </summary>
    /// <param name="style">The display style/slot to query.</param>
    /// <param name="message">When the method returns, contains the message text.</param>
    /// <param name="time">When the method returns, contains the total display time.</param>
    /// <param name="remaining">When the method returns, contains the remaining display time.</param>
    /// <returns><c>true</c> if information was retrieved successfully; otherwise, <c>false</c>.</returns>
    public partial bool GetGameText(int style, out string? message, [MarshalUsing(typeof(MillisecondsMarshaller))] out TimeSpan time, [MarshalUsing(typeof(MillisecondsMarshaller))] out TimeSpan remaining);

    /// <summary>
    /// Applies an animation to the player or actor.
    /// </summary>
    /// <param name="player">The player to apply the animation to.</param>
    /// <param name="actor">The actor to apply the animation to.</param>
    /// <param name="animation">The animation data to apply.</param>
    public partial void ApplyAnimation(IPlayer player, IActor actor, AnimationData animation);
}