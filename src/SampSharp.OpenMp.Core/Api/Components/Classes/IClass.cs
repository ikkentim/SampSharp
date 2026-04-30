namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IClass" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtensible), typeof(IIDProvider))]
public readonly partial struct IClass
{
    /// <summary>
    /// Gets the player class data.
    /// </summary>
    /// <returns>A reference to the player class information.</returns>
    public partial ref PlayerClass GetClass();

    /// <summary>
    /// Sets the player class data.
    /// </summary>
    /// <param name="data">The player class data to set.</param>
    public partial void SetClass(ref PlayerClass data);
}