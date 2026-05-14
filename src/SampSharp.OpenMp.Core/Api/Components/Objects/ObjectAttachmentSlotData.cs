using System.Numerics;
using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents attachment data for an object attached to a player bone.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="ObjectAttachmentSlotData" /> struct.
/// </remarks>
/// <param name="model">The model ID of the attached object.</param>
/// <param name="bone">The bone ID to which the object is attached.</param>
/// <param name="offset">The offset of the object relative to the bone.</param>
/// <param name="rotation">The rotation of the object relative to the bone.</param>
/// <param name="scale">The scale of the attached object.</param>
/// <param name="colour1">The primary color of the attached object.</param>
/// <param name="colour2">The secondary color of the attached object.</param>
[StructLayout(LayoutKind.Sequential)]
public readonly struct ObjectAttachmentSlotData(int model, int bone, Vector3 offset, Vector3 rotation, Vector3 scale, Colour colour1, Colour colour2)
{
    /// <summary>
    /// Gets the model ID of the attached object.
    /// </summary>
    public readonly int Model = model;

    /// <summary>
    /// Gets the bone ID to which the object is attached.
    /// </summary>
    public readonly int Bone = bone;

    /// <summary>
    /// Gets the offset of the object relative to the bone.
    /// </summary>
    public readonly Vector3 Offset = offset;

    /// <summary>
    /// Gets the rotation of the object relative to the bone.
    /// </summary>
    public readonly Vector3 Rotation = rotation;

    /// <summary>
    /// Gets the scale of the attached object.
    /// </summary>
    public readonly Vector3 Scale = scale;

    /// <summary>
    /// Gets the primary color of the attached object.
    /// </summary>
    public readonly Colour Colour1 = colour1;

    /// <summary>
    /// Gets the secondary color of the attached object.
    /// </summary>
    public readonly Colour Colour2 = colour2;
}
