namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Specifies the type of custom model file being downloaded.
/// </summary>
public enum ModelDownloadType : byte
{
    /// <summary>
    /// No model download type.
    /// </summary>
    NONE = 0,

    /// <summary>
    /// DFF (Draw File Format) - model/collision data file.
    /// </summary>
    DFF = 1,

    /// <summary>
    /// TXD (Texture Dictionary) - texture file.
    /// </summary>
    TXD = 2
}