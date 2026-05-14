using JetBrains.Annotations;

namespace SampSharp.Entities;

/// <summary>
/// An attribute which indicates the method should be invoked at a specified interval.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="TimerAttribute" /> class.
/// </remarks>
/// <param name="interval">The interval of the timer in milliseconds.</param>
[AttributeUsage(AttributeTargets.Method)]
[MeansImplicitUse]
public class TimerAttribute(double interval) : Attribute
{
    /// <summary>
    /// Gets or sets the interval of the timer in milliseconds.
    /// </summary>
    public double Interval { get; set; } = interval;

    internal TimeSpan IntervalTimeSpan => TimeSpan.FromMilliseconds(Interval);
}
