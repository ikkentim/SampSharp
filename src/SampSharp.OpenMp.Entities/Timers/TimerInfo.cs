namespace SampSharp.Entities;

internal class TimerInfo
{
    public TimerInfo(long intervalTicks, long nextTick, Action invoke, bool isActive)
    {
        IntervalTicks = intervalTicks;
        NextTick = nextTick;
        Invoke = invoke;
        IsActive = isActive;
    }

    public long IntervalTicks;
    public long NextTick;
    public Action Invoke;
    public bool IsActive;
    public TimerReference? Reference;
}