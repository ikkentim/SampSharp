using System.Numerics;
using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents the data required to attach an object or entity to another object or entity.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct ObjectAttachmentData
{
    /// <summary>
    /// Gets the type of attachment.
    /// </summary>
    public readonly AttachmentType Type;

    /// <summary>
    /// Gets a value indicating whether the rotation of the attachment is synchronized.
    /// </summary>
    public readonly byte SyncRotation;

    /// <summary>
    /// Gets the identifier of the attached object or entity.
    /// </summary>
    public readonly int Id;

    /// <summary>
    /// Gets the offset of the attachment relative to its parent.
    /// </summary>
    public readonly Vector3 Offset;

    /// <summary>
    /// Gets the rotation of the attachment relative to its parent.
    /// </summary>
    public readonly Vector3 Rotation;
}