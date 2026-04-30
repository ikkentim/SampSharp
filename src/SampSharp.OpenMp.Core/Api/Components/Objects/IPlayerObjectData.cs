using System.Numerics;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// This type represents a pointer to an unmanaged open.mp <see cref="IPlayerObjectData" /> interface.
/// </summary>
[OpenMpApi(typeof(IExtension), typeof(IPool<IPlayerObject>))]
public readonly partial struct IPlayerObjectData
{
    /// <inheritdoc />
    public static UID ExtensionId => new(0x93d4ed2344b07456);

    /// <summary>
    /// Creates a new player object with the specified model ID, position, and rotation.
    /// </summary>
    /// <param name="modelID">The model ID of the object.</param>
    /// <param name="position">The position of the object.</param>
    /// <param name="rotation">The rotation of the object.</param>
    /// <param name="drawDist">The draw distance of the object. Defaults to 0.</param>
    /// <returns>A new instance of <see cref="IPlayerObject" />.</returns>
    public partial IPlayerObject Create(int modelID, Vector3 position, Vector3 rotation, float drawDist = 0);

    /// <summary>
    /// Sets the attached object data for the specified index.
    /// </summary>
    /// <param name="index">The index of the attachment slot.</param>
    /// <param name="data">The attachment data to set.</param>
    public partial void SetAttachedObject(int index, ref ObjectAttachmentSlotData data);

    /// <summary>
    /// Removes the attached object at the specified index.
    /// </summary>
    /// <param name="index">The index of the attachment slot to remove.</param>
    public partial void RemoveAttachedObject(int index);

    /// <summary>
    /// Checks if an object is attached at the specified index.
    /// </summary>
    /// <param name="index">The index to check.</param>
    /// <returns><see langword="true" /> if an object is attached; otherwise, <see langword="false" />.</returns>
    public partial bool HasAttachedObject(int index);

    /// <summary>
    /// Gets the attached object data for the specified index.
    /// </summary>
    /// <param name="index">The index of the attachment slot.</param>
    /// <returns>A reference to the <see cref="ObjectAttachmentSlotData" /> for the specified index.</returns>
    public partial ref ObjectAttachmentSlotData GetAttachedObject(int index);

    /// <summary>
    /// Begins the object selection process.
    /// </summary>
    public partial void BeginSelecting();

    /// <summary>
    /// Checks if an object is currently being selected.
    /// </summary>
    /// <returns><see langword="true" /> if an object is being selected; otherwise, <see langword="false" />.</returns>
    public partial bool SelectingObject();

    /// <summary>
    /// Ends the object editing process.
    /// </summary>
    public partial void EndEditing();

    /// <summary>
    /// Begins editing the specified object.
    /// </summary>
    /// <param name="objekt">The object to edit.</param>
    public partial void BeginEditing(IObject objekt);

    /// <summary>
    /// Begins editing the specified player object.
    /// </summary>
    /// <param name="objekt">The player object to edit.</param>
    public partial void BeginEditing(IPlayerObject objekt);

    /// <summary>
    /// Checks if an object is currently being edited.
    /// </summary>
    /// <returns><see langword="true" /> if an object is being edited; otherwise, <see langword="false" />.</returns>
    public partial bool EditingObject();

    /// <summary>
    /// Edits the attached object at the specified index.
    /// </summary>
    /// <param name="index">The index of the attachment slot to edit.</param>
    public partial void EditAttachedObject(int index);

    /// <summary>
    /// Gets the pool interface for managing player objects.
    /// </summary>
    /// <returns>A pool interface for player objects.</returns>
    public IPool<IPlayerObject> AsPool()
    {
        return (IPool<IPlayerObject>)this;
    }
}