using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Std.Chrono;

/// <summary>
/// Represents a duration in milliseconds which is represented in memory like an <c>std::chrono::Milliseconds</c> from
/// the C++ standard library.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct Milliseconds
{
    private readonly long _value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Milliseconds" /> struct.
    /// </summary>
    /// <param name="value">The duration value in milliseconds.</param>
    public Milliseconds(long value)
    {
        _value = value;
    }
    
    /// <summary>
    /// Converts this duration to a <see cref="TimeSpan" />.
    /// </summary>
    /// <returns>The duration as a <see cref="TimeSpan" />.</returns>
    public TimeSpan AsTimeSpan()
    {
        return TimeSpan.FromMilliseconds(_value);
    }
    
    /// <summary>
    /// Converts a <see cref="Milliseconds" /> to a <see cref="TimeSpan" />.
    /// </summary>
    /// <param name="milliseconds">The value to convert.</param>
    public static implicit operator TimeSpan(Milliseconds milliseconds)
    {
        return milliseconds.AsTimeSpan();
    }
    
    /// <summary>
    /// Converts a <see cref="TimeSpan" /> to a <see cref="Milliseconds" />.
    /// </summary>
    /// <param name="timeSpan">The  time span to convert.</param>
    public static implicit operator Milliseconds(TimeSpan timeSpan)
    {
        return new Milliseconds(timeSpan.Ticks / TimeSpan.TicksPerMillisecond);
    }
    
    /// <inheritdoc />
    public override string ToString()
    {
        return _value.ToString();
    }
}