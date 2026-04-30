using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Std.Chrono;

/// <summary>
/// Represents a duration in seconds which is represented in memory like an <c>std::chrono::Seconds</c> from the C++
/// standard library.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct Seconds
{
    private readonly long _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Seconds" /> struct.
    /// </summary>
    /// <param name="value"></param>
    public Seconds(long value)
    {
        _value = value;
    }
    
    /// <summary>
    /// Converts this duration to a <see cref="TimeSpan" />.
    /// </summary>
    /// <returns>The duration as a <see cref="TimeSpan" />.</returns>
    public TimeSpan AsTimeSpan()
    {
        return TimeSpan.FromSeconds(_value);
    }
    
    /// <summary>
    /// Converts a <see cref="Seconds" /> to a <see cref="TimeSpan" />.
    /// </summary>
    /// <param name="seconds">The value to convert.</param>
    public static implicit operator TimeSpan(Seconds seconds)
    {
        return seconds.AsTimeSpan();
    }
    
    /// <summary>
    /// Converts a <see cref="TimeSpan" /> to a <see cref="Seconds" />.
    /// </summary>
    /// <param name="timeSpan">The  time span to convert.</param>
    public static implicit operator Seconds(TimeSpan timeSpan)
    {
        return new Seconds(timeSpan.Ticks / TimeSpan.TicksPerSecond);
    }
    
    /// <inheritdoc />
    public override string ToString()
    {
        return _value.ToString();
    }
}