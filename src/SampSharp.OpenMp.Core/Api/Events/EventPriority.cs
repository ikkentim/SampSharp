namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the priority of receiving events in an <see cref="IEventDispatcher{T}" />.
/// </summary>
public enum EventPriority : sbyte
{
    /// <summary>
    /// The highest priority. For handlers that must receive events first.
    /// </summary>
    Highest = sbyte.MinValue,

    /// <summary>
    /// A fairly high priority. For handlers that must receive events before most other handlers.
    /// </summary>
    FairlyHigh = Highest / 2,

    /// <summary>
    /// The default priority. For handlers that must receive events at the default priority.
    /// </summary>
    Default = 0,

    /// <summary>
    /// A fairly low priority. For handlers that must receive events after most other handlers.
    /// </summary>
    FairlyLow = Lowest / 2,

    /// <summary>
    /// The lowest priority. For handlers that must receive events last.
    /// </summary>
    Lowest = sbyte.MaxValue
}