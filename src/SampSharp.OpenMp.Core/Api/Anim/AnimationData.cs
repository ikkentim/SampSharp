using System.Runtime.InteropServices.Marshalling;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides animation data for the ApplyAnimation function.
/// </summary>
/// <param name="Delta">The speed to play the animation.</param>
/// <param name="Loop">If set to 1, the animation will loop. If set to 0, the animation will play once.</param>
/// <param name="LockX">If set to 0, the player is returned to their old X coordinate once the animation is complete (for animations that move the player such as walking). 1 will not return them to their old position.</param>
/// <param name="LockY">Same as above but for the Y axis. Should be kept the same as the previous parameter.</param>
/// <param name="Freeze">Setting this to 1 will freeze the player at the end of the animation. 0 will not.</param>
/// <param name="Time">Timer in milliseconds. For a never-ending loop it should be 0.</param>
/// <param name="Library">The animation library of the animation to apply.</param>
/// <param name="Name">The name of the animation to apply.</param>
[NativeMarshalling(typeof(AnimationDataMarshaller))]
public record AnimationData(float Delta, bool Loop, bool LockX, bool LockY, bool Freeze, uint Time, string Library, string Name);