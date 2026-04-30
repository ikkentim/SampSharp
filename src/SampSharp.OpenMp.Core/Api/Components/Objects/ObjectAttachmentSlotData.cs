using System.Numerics;
using System.Runtime.InteropServices;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Represents attachment data for an object attached to a player bone.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct ObjectAttachmentSlotData
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ObjectAttachmentSlotData" /> struct.
    /// </summary>
    /// <param name="model">The model ID of the attached object.</param>
    /// <param name="bone">The bone ID to which the object is attached.</param>
    /// <param name="offset">The offset of the object relative to the bone.</param>
    /// <param name="rotation">The rotation of the object relative to the bone.</param>
    /// <param name="scale">The scale of the attached object.</param>
    /// <param name="colour1">The primary color of the attached object.</param>
    /// <param name="colour2">The secondary color of the attached object.</param>
    public ObjectAttachmentSlotData(int model, int bone, Vector3 offset, Vector3 rotation, Vector3 scale, Colour colour1, Colour colour2)
    {
        Model = model;
        Bone = bone;
        Offset = offset;
        Rotation = rotation;
        Scale = scale;
        Colour1 = colour1;
        Colour2 = colour2;
    }

    /// <summary>
    /// Gets the model ID of the attached object.
    /// </summary>
    public readonly int Model;

    /// <summary>
    /// Gets the bone ID to which the object is attached.
    /// </summary>
    public readonly int Bone;

    /// <summary>
    /// Gets the offset of the object relative to the bone.
    /// </summary>
    public readonly Vector3 Offset;

    /// <summary>
    /// Gets the rotation of the object relative to the bone.
    /// </summary>
    public readonly Vector3 Rotation;

    /// <summary>
    /// Gets the scale of the attached object.
    /// </summary>
    public readonly Vector3 Scale;

    /// <summary>
    /// Gets the primary color of the attached object.
    /// </summary>
    public readonly Colour Colour1;

    /// <summary>
    /// Gets the secondary color of the attached object.
    /// </summary>
    public readonly Colour Colour2;
}