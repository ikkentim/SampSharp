using System.Runtime.InteropServices.Marshalling;
using SampSharp.OpenMp.Core.Std.Chrono;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IFixesComponent" /> interface.
/// </summary>
[OpenMpApi(typeof(IComponent))]
public readonly partial struct IFixesComponent
{
    /// <inheritdoc />
    public static UID ComponentId => new(0xb5c615eff0329ff7);

    /// <summary>
    /// Sends game text to all players.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="time">How long to display the message.</param>
    /// <param name="style">The display style/slot for the game text.</param>
    /// <returns><c>true</c> if successful; otherwise, <c>false</c>.</returns>
    public partial bool SendGameTextToAll(string message, [MarshalUsing(typeof(MillisecondsMarshaller))] TimeSpan time, int style);

    /// <summary>
    /// Hides game text for all players.
    /// </summary>
    /// <param name="style">The display style/slot to hide.</param>
    /// <returns><c>true</c> if successful; otherwise, <c>false</c>.</returns>
    public partial bool HideGameTextForAll(int style);

    /// <summary>
    /// Clears the current animation of a player or actor.
    /// </summary>
    /// <param name="player">The player to clear animation for.</param>
    /// <param name="actor">The actor to clear animation for.</param>
    public partial void ClearAnimation(IPlayer player, IActor actor);
}