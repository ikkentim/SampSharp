namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the type of a component.
/// </summary>
public enum ComponentType
{
    /// <summary>
    /// The component is of an unknown type.
    /// </summary>
    Other,
    /// <summary>
    /// The component is a network component.
    /// </summary>
    Network,
    /// <summary>
    /// The component is a pool component.
    /// </summary>
    Pool,
}