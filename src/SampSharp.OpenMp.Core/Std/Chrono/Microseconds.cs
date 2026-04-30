using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Std.Chrono;

/// <summary>
/// Represents a duration in microseconds which is represented in memory like an <c>std::chrono::Microseconds</c> from
/// the C++ standard library.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct Microseconds
{
    private readonly long _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Microseconds" /> struct.
    /// </summary>
    /// <param name="value">The duration value in microseconds.</param>
    public Microseconds(long value)
    {
        _value = value;
    }

    /// <summary>
    /// Converts this duration to a <see cref="TimeSpan" />.
    /// </summary>
    /// <returns>The duration as a <see cref="TimeSpan" />.</returns>
    public TimeSpan AsTimeSpan()
    {
        return TimeSpan.FromMicroseconds(_value);
    }

    /// <summary>
    /// Converts a <see cref="Microseconds" /> to a <see cref="TimeSpan" />.
    /// </summary>
    /// <param name="microseconds">The value to convert.</param>
    public static implicit operator TimeSpan(Microseconds microseconds)
    {
        return microseconds.AsTimeSpan();
    }

    /// <summary>
    /// Converts a <see cref="TimeSpan" /> to a <see cref="Microseconds" />.
    /// </summary>
    /// <param name="timeSpan">The  time span to convert.</param>
    public static implicit operator Microseconds(TimeSpan timeSpan)
    {
        return new Microseconds(timeSpan.Ticks / TimeSpan.TicksPerMicrosecond);
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return _value.ToString();
    }
}