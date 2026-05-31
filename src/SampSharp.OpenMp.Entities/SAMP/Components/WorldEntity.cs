using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a component which exists in the 3D world.
/// </summary>
public abstract class WorldEntity : IdProvider
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WorldEntity" /> class.
    /// </summary>
    /// <param name="entity">The open.mp entity this component represents.</param>
    protected WorldEntity(IEntity entity) : base(entity.HasValue ? (IIDProvider)entity : default)
    {
        Resource = entity;
    }

    private IEntity Resource
    {
        get
        {
            ObjectDisposedException.ThrowIf(!IsComponentAlive, typeof(WorldEntity));
            return field;
        }
    }

    /// <summary>
    /// Gets or sets the position of this component.
    /// </summary>
    public virtual Vector3 Position
    {
        get => Resource.GetPosition();
        set => Resource.SetPosition(value);
    }

    /// <summary>
    /// Gets or sets the rotation of this component.
    /// </summary>
    public virtual Quaternion Rotation
    {
        get => Resource.GetRotation();
        set => Resource.SetRotation(value);
    }

    /// <summary>
    /// Gets or sets the rotation of this component in Euler angles. Note: this is less accurate than the quaternion
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
        get => Resource.GetVirtualWorld();
        set => Resource.SetVirtualWorld(value);
    }
}