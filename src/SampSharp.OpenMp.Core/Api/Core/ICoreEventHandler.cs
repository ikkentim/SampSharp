using SampSharp.OpenMp.Core.Std.Chrono;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="ICore.GetEventDispatcher"/>.
/// </summary>
[OpenMpEventHandler]
public partial interface ICoreEventHandler
{
    /// <summary>
    /// Called when the server ticks.
    /// </summary>
    /// <param name="elapsed">The number of microseconds since the last tick.</param>
    /// <param name="now">The current time.</param>
    void OnTick(Microseconds elapsed, TimePoint now);
}