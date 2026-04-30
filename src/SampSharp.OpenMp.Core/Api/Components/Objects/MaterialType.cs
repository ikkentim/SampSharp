namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the type of a material.
/// </summary>
public enum MaterialType : byte
{
    /// <summary>
    /// Indicates that no material is applied.
    /// </summary>
    None,

    /// <summary>
    /// Indicates that the default material is applied.
    /// </summary>
    Default,

    /// <summary>
    /// Indicates that a text material is applied.
    /// </summary>
    Text
}