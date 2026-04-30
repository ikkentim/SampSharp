namespace SampSharp.Entities;

/// <summary>Contains context information about a fired event.</summary>
public abstract class EventContext
{
    /// <summary>Gets the name of the event.</summary>
    public abstract string Name { get; }

    /// <summary>Gets the arguments of the event.</summary>
    public abstract object[] Arguments { get; }

    /// <summary>Gets the service provider which can be used for providing services for events.</summary>
    public abstract IServiceProvider EventServices { get; }
}