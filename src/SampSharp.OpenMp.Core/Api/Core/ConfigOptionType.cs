namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the type of a configuration option.
/// </summary>
public enum ConfigOptionType
{
    /// <summary>
    /// No type.
    /// </summary>
    None = -1,

    /// <summary>
    /// An integer number.
    /// </summary>
    Int = 0,

    /// <summary>
    /// A string.
    /// </summary>
    String = 1,

    /// <summary>
    /// A floating point number.
    /// </summary>
    Float = 2,

    /// <summary>
    /// A list of strings.
    /// </summary>
    Strings = 3,

    /// <summary>
    /// A boolean value.
    /// </summary>
    Bool = 4
}