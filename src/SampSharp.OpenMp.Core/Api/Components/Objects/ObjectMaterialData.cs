using System.Runtime.InteropServices.Marshalling;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents material data applied to an object.
/// </summary>
[NativeMarshalling(typeof(ObjectMaterialDataMarshaller))]
public record ObjectMaterialData(
    int Model,
    byte MaterialSize,
    byte FontSize,
    byte Alignment,
    bool Bold,
    Colour MaterialColour,
    Colour BackgroundColour,
    string Text,
    string Font,
    MaterialType Type,
    bool Used)
{
    /// <summary>
    /// Gets the color of the font used in the material.
    /// </summary>
    public Colour FontColour => MaterialColour;

    /// <summary>
    /// Gets the texture dictionary (TXD) name used in the material.
    /// </summary>
    public string Txd => Text;

    /// <summary>
    /// Gets the texture name used in the material.
    /// </summary>
    public string Texture => Font;
}