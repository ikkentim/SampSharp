using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Std.Chrono;

/// <summary>
/// Represents a duration in hours which is represented in memory like an <c>std::chrono::hours</c> from the C++
/// standard library. MSVC uses <c>duration&lt;int, ratio&lt;3600&gt;&gt;</c>, so the layout is a single 32-bit int.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct Hours
{
    private readonly int _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Hours" /> struct.
    /// </summary>
    /// <param name="value">The duration value in hours.</param>
    public Hours(int value)
    {
        _value = value;
    }

    /// <summary>
    /// Converts this duration to a <see cref="TimeSpan" />.
    /// </summary>
    /// <returns>The duration as a <see cref="TimeSpan" />.</returns>
    public TimeSpan AsTimeSpan()
    {
        return TimeSpan.FromHours(_value);
    }

    /// <summary>
    /// Converts an <see cref="Hours" /> value to a <see cref="TimeSpan" />.
    /// </summary>
    /// <param name="hours">The value to convert.</param>
    public static implicit operator TimeSpan(Hours hours)
    {
        return hours.AsTimeSpan();
    }

    /// <summary>
    /// Converts a <see cref="TimeSpan" /> to an <see cref="Hours" /> value.
    /// </summary>
    /// <param name="timeSpan">The time span to convert.</param>
    public static implicit operator Hours(TimeSpan timeSpan)
    {
        return new Hours((int)(timeSpan.Ticks / TimeSpan.TicksPerHour));
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return _value.ToString();
    }
}
