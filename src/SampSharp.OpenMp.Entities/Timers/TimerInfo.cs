namespace SampSharp.Entities;

internal class TimerInfo
{
    public long IntervalTicks;
    public Action Invoke;
    public bool IsActive;
    public long NextTick;
    public TimerReference? Reference;

    public TimerInfo(long intervalTicks, long nextTick, Action invoke, bool isActive)
    {
        IntervalTicks = intervalTicks;
        NextTick = nextTick;
        Invoke = invoke;
        IsActive = isActive;
    }
}