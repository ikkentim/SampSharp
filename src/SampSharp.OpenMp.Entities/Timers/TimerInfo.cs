namespace SampSharp.Entities;

internal sealed class TimerInfo(long intervalTicks, long nextTick, Action invoke, bool isActive)
{
    public long IntervalTicks = intervalTicks;
    public Action Invoke = invoke;
    public bool IsActive = isActive;
    public long NextTick = nextTick;
    public TimerReference? Reference;
}