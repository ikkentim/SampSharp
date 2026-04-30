namespace SampSharp.Entities;

/// <summary>
/// Indicates that the class should be handled as a parameter for an event instead of as an injectable service.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class EventParameterAttribute : Attribute;