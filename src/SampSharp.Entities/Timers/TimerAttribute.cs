using System;

namespace SampSharp.Entities
{
    /// <summary>
    /// An attribute which indicates the method should be invoked at a specified interval.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class TimerAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimerAttribute"/> class.
        /// </summary>
        /// <param name="interval">The interval of the timer.</param>
        public TimerAttribute(double interval)
        {
            Interval = interval;
        }

        /// <summary>
        /// Gets or sets the interval of the timer.
        /// </summary>
        public double Interval { get; set; }

        internal TimeSpan IntervalTimeSpan => TimeSpan.FromMilliseconds(Interval);
    }
}