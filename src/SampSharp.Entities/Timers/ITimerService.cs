using System;

namespace SampSharp.Entities
{
    /// <summary>
    /// Provides methods for starting and stopping timers.
    /// </summary>
    public interface ITimerService
    {
        /// <summary>
        /// Starts a timer with the specified <paramref name="interval"/> and <paramref name="action"/>.
        /// </summary>
        /// <param name="action">The action to perform each timer tick.</param>
        /// <param name="interval">The interval at which to tick.</param>
        /// <returns>A reference to the started timer.</returns>
        TimerReference Start(Action<IServiceProvider> action, TimeSpan interval);

        /// <summary>
        /// Stops the specified timer.
        /// </summary>
        /// <param name="timer">The timer to stop.</param>
        void Stop(TimerReference timer);
    }
}