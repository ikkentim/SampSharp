using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

/// <summary>
/// Represents a component which provides the data and functionality of a player object.
/// </summary>
public class PlayerObject : WorldEntity
{
    private readonly IPlayerObjectData _playerObjects;
    private readonly IPlayerObject _playerObject;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerObject" /> class.
    /// </summary>
    protected PlayerObject(IPlayerObjectData playerObjects, IPlayerObject playerObject) : base((IEntity)playerObject)
    {
        _playerObjects = playerObjects;
        _playerObject = playerObject;
    }

    /// <summary>
    /// Gets a value indicating whether the open.mp entity counterpart has been destroyed.
    /// </summary>
    protected bool IsOmpEntityDestroyed => _playerObject.TryGetExtension<ComponentExtension>()?.IsOmpEntityDestroyed ?? true;

    /// <summary>
    /// Gets a value indicating whether this player object is moving.
    /// </summary>
    public virtual bool IsMoving => _playerObject.IsMoving();

    /// <summary>
    /// Gets the model ID of this player object.
    /// </summary>
    public virtual int ModelId => _playerObject.GetModel();

    /// <summary>
    /// Gets the draw distance of this player object.
    /// </summary>
    public virtual float DrawDistance => _playerObject.GetDrawDistance();

    /// <summary>
    /// Moves this player object to the specified <paramref name="position" /> and <paramref name="rotation" /> with the specified <paramref name="speed" />.
    /// </summary>
    /// <param name="position">The position to move this player object to as a <see cref="Vector3" />.</param>
    /// <param name="speed">The speed at which to move this player object.</param>
    /// <param name="rotation">The rotation to move this player object to as a <see cref="Vector3" />.</param>
    /// <returns>The time in milliseconds for the object to complete the move.</returns>
    public virtual int Move(Vector3 position, float speed, Vector3 rotation)
    {
        var time = (position - Position).Length() / speed * 1000f;

        var moveDat = new ObjectMoveData(position, rotation, speed);
        _playerObject.Move(ref moveDat);

        return (int)time;
    }

    /// <summary>
    /// Moves this player object to the specified <paramref name="position" /> with the specified <paramref name="speed" />.
    /// </summary>
    /// <param name="position">The position to move this player object to as a <see cref="Vector3" />.</param>
    /// <param name="speed">The speed at which to move this player object.</param>
    /// <returns>The time in milliseconds for the object to complete the move.</returns>
    public virtual int Move(Vector3 position, float speed)
    {
        return Move(position, speed, new Vector3(-1000));
    }

    /// <summary>
    /// Stops this player object from moving.
    /// </summary>
    public virtual void Stop()
    {
        _playerObject.Stop();
    }

    /// <summary>
    /// Sets the material color of this player object.
    /// </summary>
    /// <param name="materialIndex">The material index on the object to change.</param>
    /// <param name="modelId">The model ID on which the replacement texture is located. Use 0 for alpha. Use -1 to change the material color without altering the texture.</param>
    /// <param name="txdName">The name of the TXD file containing the replacement texture (use "none" if not required).</param>
    /// <param name="textureName">The name of the texture to use as the replacement (use "none" if not required).</param>
    /// <param name="materialColor">The <see cref="Color" /> of the material to set (use <see cref="default(Color)" /> to keep the existing material color).</param>
    public virtual void SetMaterial(int materialIndex, int modelId, string txdName, string textureName, Color materialColor)
    {
        _playerObject.SetMaterial((uint)materialIndex, modelId, txdName, textureName, materialColor);
    }

    /// <summary>
    /// Sets the material text of this player object.
    /// </summary>
    /// <param name="materialIndex">The material index on the object to change.</param>
    /// <param name="text">The text to display on the object (maximum 2048 characters).</param>
    /// <param name="materialSize">The object's material size.</param>
    /// <param name="fontface">The font to use for the text.</param>
    /// <param name="fontSize">The size of the text (maximum 255).</param>
    /// <param name="bold">A value indicating whether to write the text in bold.</param>
    /// <param name="foreColor">The color of the text.</param>
    /// <param name="backColor">The background color of the text.</param>
    /// <param name="textAlignment">The alignment of the text.</param>
    public virtual void SetMaterialText(int materialIndex, string text, ObjectMaterialSize materialSize, string fontface, int fontSize, bool bold, Color foreColor,
        Color backColor, ObjectMaterialTextAlign textAlignment)
    {
        _playerObject.SetMaterialText((uint)materialIndex, text, (SampSharp.OpenMp.Core.Api.ObjectMaterialSize)materialSize, fontface, fontSize, bold, foreColor, backColor,
            (SampSharp.OpenMp.Core.Api.ObjectMaterialTextAlign)textAlignment);
    }

    /// <summary>
    /// Disables collisions between players' cameras and this player object.
    /// </summary>
    public virtual void DisableCameraCollisions()
    {
        _playerObject.SetCameraCollision(false);
    }

    /// <summary>
    /// Attaches this object to the specified <paramref name="target" /> <see cref="Player" />.
    /// </summary>
    /// <param name="target">The player to attach this object to.</param>
    /// <param name="offset">The offset position relative to the player.</param>
    /// <param name="rotation">The rotation to apply to this object.</param>
    public virtual void AttachTo(Player target, Vector3 offset, Vector3 rotation)
    {
        _playerObject.AttachToPlayer(target, offset, rotation);
    }

    /// <summary>
    /// Attaches this object to the specified <paramref name="target" /> <see cref="Vehicle" />.
    /// </summary>
    /// <param name="target">The vehicle to attach this object to.</param>
    /// <param name="offset">The offset position relative to the vehicle.</param>
    /// <param name="rotation">The rotation to apply to this object.</param>
    public virtual void AttachTo(Vehicle target, Vector3 offset, Vector3 rotation)
    {
        _playerObject.AttachToVehicle(target, offset, rotation);
    }

    /// <summary>
    /// Attaches this object to the specified <paramref name="target" /> <see cref="PlayerObject" />.
    /// </summary>
    /// <param name="target">The player object to attach this object to.</param>
    /// <param name="offset">The offset position relative to the target object.</param>
    /// <param name="rotation">The rotation to apply to this object.</param>
    public virtual void AttachTo(PlayerObject target, Vector3 offset, Vector3 rotation)
    {
        _playerObject.AttachToObject(target, offset, rotation);
    }

    /// <inheritdoc />
    protected override void OnDestroyComponent()
    {
        if (!IsOmpEntityDestroyed)
        {
            _playerObjects.Release(Id);
        }
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"(Id: {Id}, Model: {ModelId})";
    }

    /// <summary>
    /// Performs an implicit conversion from <see cref="PlayerObject" /> to <see cref="IPlayerObject" />.
    /// </summary>
    public static implicit operator IPlayerObject(PlayerObject playerObject)
    {
        return playerObject._playerObject;
    }
}