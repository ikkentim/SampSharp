using System.Diagnostics;

namespace SampSharp.Entities;

internal static class StopwatchTime
{
    /// <summary>
    /// Converts a TimeSpan to Stopwatch ticks.
    ///
    /// IMPORTANT:
    /// - TimeSpan ticks are fixed (1 tick = 100ns, 10,000,000 per second)
    /// - Stopwatch ticks depend on hardware (Stopwatch.Frequency)
    ///
    /// This method converts a TimeSpan duration to the equivalent number
    /// of Stopwatch ticks so both values can be used in the same time system.
    /// </summary>
    public static long ToStopwatchTicks(TimeSpan time)
    {
        return (long)(time.TotalSeconds * Stopwatch.Frequency);
    }

    /// <summary>
    /// Converts Stopwatch ticks to a TimeSpan.
    ///
    /// This is the inverse operation of ToStopwatchTicks.
    /// It converts a duration measured in Stopwatch ticks back into
    /// a TimeSpan using Stopwatch.Frequency.
    /// </summary>
    public static TimeSpan ToTimeSpan(long stopwatchTicks)
    {
        return TimeSpan.FromSeconds(stopwatchTicks / (double)Stopwatch.Frequency);
    }
}
