namespace SampSharp.Entities;

/// <summary>
/// Provides methods for starting and stopping timers.
/// </summary>
public interface ITimerService
{
    /// <summary>
    /// Starts a timer with the specified <paramref name="interval" /> and <paramref name="action" />.
    /// </summary>
    /// <param name="action">The action to perform each timer tick.</param>
    /// <param name="interval">The interval at which to tick.</param>
    /// <returns>A reference to the started timer.</returns>
    TimerReference Start(Action<IServiceProvider> action, TimeSpan interval);

    /// <summary>
    /// Starts a timer with the specified <paramref name="interval" /> and <paramref name="action" />.
    /// </summary>
    /// <param name="action">The action to perform each timer tick.</param>
    /// <param name="interval">The interval at which to tick.</param>
    /// <returns>A reference to the started timer.</returns>
    TimerReference Start(Action<IServiceProvider, TimerReference> action, TimeSpan interval);

    /// <summary>
    /// Starts a timer with the specified <paramref name="action" /> which will tick only once after the specified <paramref name="delay" />.
    /// </summary>
    /// <param name="action">The action perform at the ticker</param>
    /// <param name="delay">The delay after which the timer will tick.</param>
    /// <returns>A reference to the started timer</returns>
    TimerReference Delay(Action<IServiceProvider> action, TimeSpan delay);

    /// <summary>
    /// Stops the specified timer.
    /// </summary>
    /// <param name="timer">The timer to stop.</param>
    void Stop(TimerReference timer);
}