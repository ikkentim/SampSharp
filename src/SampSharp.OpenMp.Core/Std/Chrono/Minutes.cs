using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Std.Chrono;

/// <summary>
/// Represents a duration in minutes which is represented in memory like an <c>std::chrono::minutes</c> from the C++
/// standard library. MSVC uses <c>duration&lt;int, ratio&lt;60&gt;&gt;</c>, so the layout is a single 32-bit int.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct Minutes
{
    private readonly int _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Minutes" /> struct.
    /// </summary>
    /// <param name="value">The duration value in minutes.</param>
    public Minutes(int value)
    {
        _value = value;
    }

    /// <summary>
    /// Converts this duration to a <see cref="TimeSpan" />.
    /// </summary>
    /// <returns>The duration as a <see cref="TimeSpan" />.</returns>
    public TimeSpan AsTimeSpan()
    {
        return TimeSpan.FromMinutes(_value);
    }

    /// <summary>
    /// Converts a <see cref="Minutes" /> value to a <see cref="TimeSpan" />.
    /// </summary>
    /// <param name="minutes">The value to convert.</param>
    public static implicit operator TimeSpan(Minutes minutes)
    {
        return minutes.AsTimeSpan();
    }

    /// <summary>
    /// Converts a <see cref="TimeSpan" /> to a <see cref="Minutes" /> value.
    /// </summary>
    /// <param name="timeSpan">The time span to convert.</param>
    public static implicit operator Minutes(TimeSpan timeSpan)
    {
        return new Minutes((int)(timeSpan.Ticks / TimeSpan.TicksPerMinute));
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return _value.ToString();
    }
}
