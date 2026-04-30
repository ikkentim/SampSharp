using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Std.Chrono;

/// <summary>
/// Represents a point in time which is represented in memory like an <c>std::chrono::TimePoint</c> from the C++
/// standard library.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct TimePoint
{
    private readonly long _value;

    private TimePoint(long value)
    {
        _value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TimePoint" /> struct.
    /// </summary>
    /// <param name="time">The point in time the value should represent.</param>
    /// <returns>The new time point value.</returns>
    public static TimePoint FromDateTimeOffset(DateTimeOffset time)
    {
        var ticksSinceEpoch = time.UtcTicks - DateTimeOffset.UnixEpoch.Ticks;
        return new TimePoint(ticksSinceEpoch);
    }

    /// <summary>
    /// Converts this time point to a <see cref="DateTimeOffset" />.
    /// </summary>
    /// <returns>The converted value.</returns>
    public DateTimeOffset ToDateTimeOffset()
    {
        return new DateTimeOffset(_value + DateTimeOffset.UnixEpoch.Ticks, TimeSpan.Zero);
    }

    /// <summary>
    /// Converts a <see cref="TimePoint" /> to a <see cref="DateTimeOffset" />.
    /// </summary>
    /// <param name="timePoint">The value to convert.</param>
    public static implicit operator DateTimeOffset(TimePoint timePoint)
    {
        return timePoint.ToDateTimeOffset();
    }

    /// <summary>
    /// Converts a <see cref="DateTimeOffset" /> to a <see cref="TimePoint" />.
    /// </summary>
    /// <param name="time">The value to convert.</param>
    public static implicit operator TimePoint(DateTimeOffset time)
    {
        return FromDateTimeOffset(time);
    }
    /// <inheritdoc />
    public override string ToString()
    {
        return _value.ToString();
    }
}