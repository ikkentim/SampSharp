using JetBrains.Annotations;

namespace SampSharp.Entities;

/// <summary>Indicates a method is to be invoked when an event occurs.</summary>
[AttributeUsage(AttributeTargets.Method)]
[MeansImplicitUse]
public class EventAttribute : Attribute
{
    /// <summary>Gets or sets the name of the event which should invoke the method. If this value is null, the method name is used as the event name.</summary>
    public string? Name { get; set; }
}