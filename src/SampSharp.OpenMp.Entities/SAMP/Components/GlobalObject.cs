using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;


/// <summary>Represents a component which provides the data and functionality of an object.</summary>
public class GlobalObject : WorldEntity
{
    private readonly IObjectsComponent _objects;
    private readonly IObject _object;

    /// <summary>Constructs an instance of GlobalObject, should be used internally.</summary>
    protected GlobalObject(IObjectsComponent objects, IObject @object) : base((IEntity)@object)
    {
        _objects = objects;
        _object = @object;
    }
    
    /// <summary>
    /// Gets a value indicating whether the open.mp entity counterpart has been destroyed.
    /// </summary>
    protected bool IsOmpEntityDestroyed => _object.TryGetExtension<ComponentExtension>()?.IsOmpEntityDestroyed ?? true;

    /// <summary>Gets whether this object is moving.</summary>
    public virtual bool IsMoving => _object.IsMoving();

    /// <summary>Gets the model of this object.</summary>
    public virtual int ModelId => _object.GetModel();

    /// <summary>Gets the draw distance of this object.</summary>
    public virtual float DrawDistance => _object.GetDrawDistance();

    /// <summary>Moves this object to the given position and rotation with the given speed.</summary>
    /// <param name="position">The position to which to move this object.</param>
    /// <param name="speed">The speed at which to move this object.</param>
    /// <param name="rotation">The rotation to which to move this object.</param>
    /// <returns>The time it will take for the object to move in milliseconds.</returns>
    public virtual int Move(Vector3 position, float speed, Vector3 rotation)
    {
        var time = (position - Position).Length() / speed * 1000f;

        var moveDat = new ObjectMoveData(position, rotation, speed);
        _object.Move(ref moveDat);

        return (int)time;
    }

    /// <summary>Moves this object to the given position with the given speed.</summary>
    /// <param name="position">The position to which to move this object.</param>
    /// <param name="speed">The speed at which to move this object.</param>
    /// <returns>The time it will take for the object to move in milliseconds.</returns>
    public virtual int Move(Vector3 position, float speed)
    {
        return Move(position, speed, new Vector3(-1000));
    }

    /// <summary>Stop this object from moving any further.</summary>
    public virtual void Stop()
    {
        _object.Stop();
    }

    /// <summary>Sets the material of this object.</summary>
    /// <param name="materialIndex">The material index on the object to change.</param>
    /// <param name="modelId">
    /// The model ID on which the replacement texture is located. Use 0 for alpha. Use -1 to change the material color without altering the
    /// texture.
    /// </param>
    /// <param name="txdName">The name of the txd file which contains the replacement texture (use "none" if not required).</param>
    /// <param name="textureName">The name of the texture to use as the replacement (use "none" if not required).</param>
    /// <param name="materialColor">The object color to set (use default(Color) to keep the existing material color).</param>
    public virtual void SetMaterial(int materialIndex, int modelId, string txdName, string textureName, Color materialColor)
    {
        _object.SetMaterial((uint)materialIndex, modelId, txdName, textureName, materialColor);
    }

    /// <summary>Sets the material text of this object.</summary>
    /// <param name="materialIndex">The material index on the object to change.</param>
    /// <param name="text">The text to show on the object. (MAX 2048 characters)</param>
    /// <param name="materialSize">The object's material index to replace with text.</param>
    /// <param name="fontface">The font to use.</param>
    /// <param name="fontSize">The size of the text (max 255).</param>
    /// <param name="bold">Whether to write in bold.</param>
    /// <param name="foreColor">The color of the text.</param>
    /// <param name="backColor">The background color of the text.</param>
    /// <param name="textAlignment">The alignment of the text.</param>
    public virtual void SetMaterialText(int materialIndex, string text, ObjectMaterialSize materialSize, string fontface, int fontSize, bool bold, Color foreColor,
        Color backColor, ObjectMaterialTextAlign textAlignment)
    {
        _object.SetMaterialText((uint)materialIndex, text, (SampSharp.OpenMp.Core.Api.ObjectMaterialSize)materialSize, fontface, fontSize, bold, foreColor, backColor, (SampSharp.OpenMp.Core.Api.ObjectMaterialTextAlign)textAlignment);
    }

    /// <summary>Disable collisions between players' cameras and this <see cref="GlobalObject" />.</summary>
    public virtual void DisableCameraCollisions()
    {
        _object.SetCameraCollision(false);
    }

    /// <summary>Attaches this object to the specified player.</summary>
    /// <param name="target">The player.</param>
    /// <param name="offset">The offset.</param>
    /// <param name="rotation">The rotation.</param>
    public virtual void AttachTo(Player target, Vector3 offset, Vector3 rotation)
    {
        _object.AttachToPlayer(target, offset, rotation);
    }
    
    /// <summary>Attaches this object to the specified vehicle.</summary>
    /// <param name="target">The vehicle.</param>
    /// <param name="offset">The offset.</param>
    /// <param name="rotation">The rotation.</param>
    public virtual void AttachTo(Vehicle target, Vector3 offset, Vector3 rotation)
    {
        _object.AttachToVehicle(target, offset, rotation);
    }

    /// <summary>Attaches this object to the specified object.</summary>
    /// <param name="target">The object.</param>
    /// <param name="offset">The offset.</param>
    /// <param name="rotation">The rotation.</param>
    /// <param name="syncRotation">if set to <see langword="true" /> synchronize rotation with objects attached to.</param>
    public virtual void AttachTo(GlobalObject target, Vector3 offset, Vector3 rotation, bool syncRotation = false)
    {
        _object.AttachToObject(target, offset, rotation, syncRotation);
    }
    
    /// <inheritdoc />
    protected override void OnDestroyComponent()
    {
        if (!IsOmpEntityDestroyed)
        {
            _objects.AsPool().Release(Id);
        }
    }
    
    /// <inheritdoc />
    public override string ToString()
    {
        return $"(Id: {Id}, Model: {ModelId})";
    }
    
    /// <summary>Performs an implicit conversion from <see cref="GlobalObject" /> to <see cref="IObject" />.</summary>
    public static implicit operator IObject(GlobalObject @object)
    {
        return @object._object;
    }
}