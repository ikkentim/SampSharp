namespace SampSharp.Entities.SAMP;

/// <summary>
/// Contains all server variable types.
/// </summary>
public enum ServerVarType
{
    /// <summary>
    /// Var does not exist.
    /// </summary>
    None = 0,

    /// <summary>
    /// Var as an integer.
    /// </summary>
    Int = 1,

    /// <summary>
    /// Var is a string.
    /// </summary>
    String = 2,

    /// <summary>
    /// Var is a float.
    /// </summary>
    Float = 3
}