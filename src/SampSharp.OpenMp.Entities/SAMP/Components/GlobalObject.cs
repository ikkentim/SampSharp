using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a component which provides the data and functionality of an object.
/// </summary>
public class GlobalObject : WorldEntity
{
    private readonly IObjectsComponent _objects;
    private readonly IObject _object;

    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalObject" /> class.
    /// </summary>
    protected GlobalObject(IObjectsComponent objects, IObject @object) : base((IEntity)@object)
    {
        _objects = objects;
        _object = @object;
    }

    /// <summary>
    /// Gets a value indicating whether the open.mp entity counterpart has been destroyed.
    /// </summary>
    protected bool IsOmpEntityDestroyed => _object.TryGetExtension<ComponentExtension>()?.IsOmpEntityDestroyed ?? true;

    /// <summary>
    /// Gets a value indicating whether this object is moving.
    /// </summary>
    public virtual bool IsMoving => _object.IsMoving();

    /// <summary>
    /// Gets the model ID of this object.
    /// </summary>
    public virtual int ModelId => _object.GetModel();

    /// <summary>
    /// Gets the draw distance of this object.
    /// </summary>
    public virtual float DrawDistance => _object.GetDrawDistance();

    /// <summary>
    /// Moves this global object to the specified <paramref name="position" /> and <paramref name="rotation" /> with the specified <paramref name="speed" />.
    /// </summary>
    /// <param name="position">The position to move this object to as a <see cref="Vector3" />.</param>
    /// <param name="speed">The speed at which to move this object.</param>
    /// <param name="rotation">The rotation to move this object to as a <see cref="Vector3" />.</param>
    /// <returns>The time in milliseconds for the object to complete the move.</returns>
    public virtual int Move(Vector3 position, float speed, Vector3 rotation)
    {
        var time = (position - Position).Length() / speed * 1000f;

        var moveDat = new ObjectMoveData(position, rotation, speed);
        _object.Move(ref moveDat);

        return (int)time;
    }

    /// <summary>
    /// Moves this global object to the specified <paramref name="position" /> with the specified <paramref name="speed" />.
    /// </summary>
    /// <param name="position">The position to move this object to as a <see cref="Vector3" />.</param>
    /// <param name="speed">The speed at which to move this object.</param>
    /// <returns>The time in milliseconds for the object to complete the move.</returns>
    public virtual int Move(Vector3 position, float speed)
    {
        return Move(position, speed, new Vector3(-1000));
    }

    /// <summary>
    /// Stops this object from moving.
    /// </summary>
    public virtual void Stop()
    {
        _object.Stop();
    }

    /// <summary>
    /// Sets the material color of this global object.
    /// </summary>
    /// <param name="materialIndex">The material index on the object to change.</param>
    /// <param name="modelId">The model ID on which the replacement texture is located. Use 0 for alpha. Use -1 to change the material color without altering the texture.</param>
    /// <param name="txdName">The name of the TXD file containing the replacement texture (use "none" if not required).</param>
    /// <param name="textureName">The name of the texture to use as the replacement (use "none" if not required).</param>
    /// <param name="materialColor">The <see cref="Color" /> of the material to set (use <see cref="default(Color)" /> to keep the existing material color).</param>
    public virtual void SetMaterial(int materialIndex, int modelId, string txdName, string textureName, Color materialColor)
    {
        ArgumentNullException.ThrowIfNull(txdName);
        ArgumentNullException.ThrowIfNull(textureName);
        
        _object.SetMaterial((uint)materialIndex, modelId, txdName, textureName, materialColor);
    }

    /// <summary>
    /// Sets the material text of this global object.
    /// </summary>
    /// <param name="materialIndex">The material index on the object to change.</param>
    /// <param name="text">The text to display on the object (maximum 2048 characters).</param>
    /// <param name="materialSize">The object's material size.</param>
    /// <param name="fontface">The font to use for the text.</param>
    /// <param name="fontSize">The size of the text (maximum 255).</param>
    /// <param name="bold">A value indicating whether to write the text in bold.</param>
    /// <param name="foreColor">The <see cref="Color" /> of the text.</param>
    /// <param name="backColor">The background <see cref="Color" /> of the text.</param>
    /// <param name="textAlignment">The alignment of the text.</param>
    public virtual void SetMaterialText(int materialIndex, string text, ObjectMaterialSize materialSize, string fontface, int fontSize, bool bold, Color foreColor,
        Color backColor, ObjectMaterialTextAlign textAlignment)
    {
        ArgumentNullException.ThrowIfNull(text);
        ArgumentNullException.ThrowIfNull(fontface);
        
        _object.SetMaterialText((uint)materialIndex, text, (SampSharp.OpenMp.Core.Api.ObjectMaterialSize)materialSize, fontface, fontSize, bold, foreColor, backColor, (SampSharp.OpenMp.Core.Api.ObjectMaterialTextAlign)textAlignment);
    }

    /// <summary>
    /// Disables collisions between players' cameras and this global object.
    /// </summary>
    public virtual void DisableCameraCollisions()
    {
        _object.SetCameraCollision(false);
    }

    /// <summary>
    /// Sets whether this global object collides with players' cameras.
    /// </summary>
    /// <param name="enable"><see langword="true" /> to enable camera collision; <see langword="false" /> to disable.</param>
    public virtual void SetCameraCollision(bool enable)
    {
        _object.SetCameraCollision(enable);
    }

    /// <summary>
    /// Gets a value indicating whether this global object collides with players' cameras.
    /// </summary>
    public virtual bool HasCameraCollision => _object.GetCameraCollision();

    /// <summary>
    /// Gets the current movement data of this global object.
    /// </summary>
    /// <returns>The <see cref="ObjectMoveData" /> describing the current move target.</returns>
    public virtual ObjectMoveData GetMovingData()
    {
        return _object.GetMovingData();
    }

    /// <summary>
    /// Gets the attachment data of this global object.
    /// </summary>
    /// <returns>The <see cref="ObjectAttachmentData" /> describing the current attachment.</returns>
    public virtual ObjectAttachmentData GetAttachmentData()
    {
        return _object.GetAttachmentData();
    }

    /// <summary>
    /// Resets any current attachment of this global object (to a player, vehicle, or another object).
    /// </summary>
    public virtual void ResetAttachment()
    {
        _object.ResetAttachment();
    }

    /// <summary>
    /// Gets the material data for the specified material slot, if any has been set.
    /// </summary>
    /// <param name="materialIndex">The material slot index.</param>
    /// <returns>The material data, or <see langword="null" /> if no material has been set in that slot.</returns>
    public virtual ObjectMaterialData? GetMaterialData(int materialIndex)
    {
        return _object.GetMaterialData((uint)materialIndex, out var data) ? data : null;
    }

    /// <summary>
    /// Attaches this global object to the specified <paramref name="target" /> <see cref="Player" />.
    /// </summary>
    /// <param name="target">The <see cref="Player" /> to attach this object to.</param>
    /// <param name="offset">The offset position relative to the player as a <see cref="Vector3" />.</param>
    /// <param name="rotation">The rotation to apply to this object as a <see cref="Vector3" />.</param>
    public virtual void AttachTo(Player target, Vector3 offset, Vector3 rotation)
    {
        ArgumentNullException.ThrowIfNull(target);
        
        _object.AttachToPlayer(target, offset, rotation);
    }

    /// <summary>
    /// Attaches this global object to the specified <paramref name="target" /> <see cref="Vehicle" />.
    /// </summary>
    /// <param name="target">The <see cref="Vehicle" /> to attach this object to.</param>
    /// <param name="offset">The offset position relative to the vehicle as a <see cref="Vector3" />.</param>
    /// <param name="rotation">The rotation to apply to this object as a <see cref="Vector3" />.</param>
    public virtual void AttachTo(Vehicle target, Vector3 offset, Vector3 rotation)
    {
        ArgumentNullException.ThrowIfNull(target);
        
        _object.AttachToVehicle(target, offset, rotation);
    }

    /// <summary>
    /// Attaches this global object to the specified <paramref name="target" /> <see cref="GlobalObject" />.
    /// </summary>
    /// <param name="target">The <see cref="GlobalObject" /> to attach this object to.</param>
    /// <param name="offset">The offset position relative to the target object as a <see cref="Vector3" />.</param>
    /// <param name="rotation">The rotation to apply to this object as a <see cref="Vector3" />.</param>
    /// <param name="syncRotation">A value indicating whether to synchronize the rotation with the target object.</param>
    public virtual void AttachTo(GlobalObject target, Vector3 offset, Vector3 rotation, bool syncRotation = false)
    {
        ArgumentNullException.ThrowIfNull(target);
        
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

    /// <summary>
    /// Performs an implicit conversion from <see cref="GlobalObject" /> to <see cref="IObject" />.
    /// </summary>
    public static implicit operator IObject(GlobalObject @object)
    {
        return @object._object;
    }
}