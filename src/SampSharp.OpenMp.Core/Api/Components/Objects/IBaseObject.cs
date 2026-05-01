using System.Numerics;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IBaseObject" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtensible), typeof(IEntity))]
public readonly partial struct IBaseObject
{
    /// <summary>
    /// Sets the draw distance for the object.
    /// </summary>
    /// <param name="drawDistance">The draw distance in units.</param>
    public partial void SetDrawDistance(float drawDistance);

    /// <summary>
    /// Gets the draw distance of the object.
    /// </summary>
    /// <returns>The draw distance in units.</returns>
    public partial float GetDrawDistance();

    /// <summary>
    /// Sets the model ID of the object.
    /// </summary>
    /// <param name="model">The model ID of the object.</param>
    public partial void SetModel(int model);

    /// <summary>
    /// Gets the model ID of the object.
    /// </summary>
    /// <returns>The model ID of the object.</returns>
    public partial int GetModel();

    /// <summary>
    /// Sets whether the object has camera collision enabled.
    /// </summary>
    /// <param name="collision"><c>true</c> to enable camera collision; otherwise, <c>false</c>.</param>
    public partial void SetCameraCollision(bool collision);

    /// <summary>
    /// Gets whether the object has camera collision enabled.
    /// </summary>
    /// <returns><c>true</c> if the object has camera collision enabled; otherwise, <c>false</c>.</returns>
    public partial bool GetCameraCollision();

    /// <summary>
    /// Moves the object to a new position and rotation over a specified time.
    /// </summary>
    /// <param name="data">The movement data containing the target position, rotation, and time.</param>
    public partial void Move(ref ObjectMoveData data);

    /// <summary>
    /// Checks if the object is currently moving.
    /// </summary>
    /// <returns><c>true</c> if the object is moving; otherwise, <c>false</c>.</returns>
    public partial bool IsMoving();

    /// <summary>
    /// Stops the object's current movement.
    /// </summary>
    public partial void Stop();

    /// <summary>
    /// Gets the current movement data of the object.
    /// </summary>
    /// <returns>A reference to the <see cref="ObjectMoveData" /> structure containing the movement information.</returns>
    public partial ref ObjectMoveData GetMovingData();

    /// <summary>
    /// Attaches the object to a vehicle at the specified offset and rotation.
    /// </summary>
    /// <param name="vehicle">The vehicle to attach the object to.</param>
    /// <param name="offset">The offset from the vehicle's center.</param>
    /// <param name="rotation">The rotation of the object relative to the vehicle.</param>
    public partial void AttachToVehicle(IVehicle vehicle, Vector3 offset, Vector3 rotation);

    /// <summary>
    /// Resets the object's attachment, removing it from any vehicle or object it was attached to.
    /// </summary>
    public partial void ResetAttachment();

    /// <summary>
    /// Gets the attachment data of the object.
    /// </summary>
    /// <returns>A reference to the <see cref="ObjectAttachmentData" /> structure containing the attachment information.</returns>
    public partial ref ObjectAttachmentData GetAttachmentData();

    /// <summary>
    /// Gets the material data for a specific material index on the object.
    /// </summary>
    /// <param name="materialIndex">The index of the material to retrieve.</param>
    /// <param name="output">When the method returns, contains the material data if successful; otherwise, <c>null</c>.</param>
    /// <returns><c>true</c> if the material data was retrieved successfully; otherwise, <c>false</c>.</returns>
    public partial bool GetMaterialData(uint materialIndex, out ObjectMaterialData? output);

    /// <summary>
    /// Sets the material for a specific material index on the object.
    /// </summary>
    /// <param name="materialIndex">The index of the material to set.</param>
    /// <param name="model">The model ID to use for the texture library.</param>
    /// <param name="textureLibrary">The texture library (TXD) name.</param>
    /// <param name="textureName">The texture name from the library.</param>
    /// <param name="colour">The colour to apply to the material.</param>
    public partial void SetMaterial(uint materialIndex, int model, string textureLibrary, string textureName, Colour colour);

    /// <summary>
    /// Sets text material on a specific material index of the object.
    /// </summary>
    /// <param name="materialIndex">The index of the material to set text on.</param>
    /// <param name="text">The text to display on the material.</param>
    /// <param name="materialSize">The size of the material.</param>
    /// <param name="fontFace">The font face to use for the text.</param>
    /// <param name="fontSize">The font size for the text.</param>
    /// <param name="bold"><c>true</c> to make the text bold; otherwise, <c>false</c>.</param>
    /// <param name="fontColour">The colour of the text.</param>
    /// <param name="backgroundColour">The background colour of the material.</param>
    /// <param name="align">The text alignment on the material.</param>
    public partial void SetMaterialText(uint materialIndex, string text, ObjectMaterialSize materialSize, string fontFace, int fontSize, bool bold, Colour fontColour,
        Colour backgroundColour, ObjectMaterialTextAlign align);
}