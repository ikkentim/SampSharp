using System;
using System.Diagnostics;
using System.Reflection;

namespace SampSharp.Entities
{
    /// <summary>
    /// Represents a reference to an interval or timeout.
    /// </summary>
    public class TimerReference
    {
        internal TimerReference(TimerInfo info, object target, MethodInfo method)
        {
            Info = info;
            Target = target;
            Method = method;
        }

        /// <summary>
        /// Gets the time span until the next tick of this timer.
        /// </summary>
        public TimeSpan NextTick => new TimeSpan(Info.NextTick - Stopwatch.GetTimestamp());

        internal TimerInfo Info { get; set; }

        /// <summary>
        /// Gets a value indicating whether the timer is active.
        /// </summary>
        public bool IsActive => Info.IsActive;

        /// <summary>
        /// Gets the target on which the timer is invoked.
        /// </summary>
        public object Target { get; }

        /// <summary>
        /// Gets the method to be invoked with this timer.
        /// </summary>
        public MethodInfo Method { get; }
    }
}