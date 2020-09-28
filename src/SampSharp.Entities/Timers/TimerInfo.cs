using System;

namespace SampSharp.Entities
{
    internal class TimerInfo
    {
        public long IntervalTicks;
        public long NextTick;
        public Action Invoke;
        public bool IsActive;
    }
}