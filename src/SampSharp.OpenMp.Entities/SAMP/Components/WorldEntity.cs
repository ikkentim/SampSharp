using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a component which exists in the 3D world.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="WorldEntity" /> class.
/// </remarks>
/// <param name="entity">The open.mp entity this component represents.</param>
public abstract class WorldEntity(IEntity entity) : IdProvider((IIDProvider)entity)
{
    /// <summary>
    /// Gets or sets the position of this component.
    /// </summary>
    public virtual Vector3 Position
    {
        get => entity.GetPosition();
        set => entity.SetPosition(value);
    }

    /// <summary>
    /// Gets or sets the rotation of this component.
    /// </summary>
    public virtual Quaternion Rotation
    {
        get => entity.GetRotation();
        set => entity.SetRotation(value);
    }

    /// <summary>
    /// Gets or sets the rotation of this component in euler angles. Note: this is less accurate than the quaternion
    /// representation available through the <see cref="Rotation" /> property.
    /// </summary>
    public virtual Vector3 RotationEuler
    {
        get => Vector3.RadiansToDegrees(MathHelper.CreateYawPitchRollFromQuaternion(Rotation));
        set => Rotation = MathHelper.CreateQuaternionFromYawPitchRoll(Vector3.DegreesToRadians(value));
    }

    /// <summary>
    /// Gets or sets the virtual world of this component.
    /// </summary>
    public virtual int VirtualWorld
    {
        get => entity.GetVirtualWorld();
        set => entity.SetVirtualWorld(value);
    }
}