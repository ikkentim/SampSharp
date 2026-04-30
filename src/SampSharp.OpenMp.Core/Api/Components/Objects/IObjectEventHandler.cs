using System.Numerics;

namespace SampSharp.OpenMp.Core.Api;

/// <summary>
/// Provides the events for <see cref="IObjectsComponent.GetEventDispatcher" />.
/// </summary>
[OpenMpEventHandler]
public partial interface IObjectEventHandler
{
    /// <summary>
    /// Called when an object has finished moving.
    /// </summary>
    /// <param name="objekt">The object that moved.</param>
    void OnMoved(IObject objekt);

    /// <summary>
    /// Called when a player object has finished moving.
    /// </summary>
    /// <param name="player">The player associated with the object.</param>
    /// <param name="objekt">The player object that moved.</param>
    void OnPlayerObjectMoved(IPlayer player, IPlayerObject objekt);

    /// <summary>
    /// Called when an object is selected by a player.
    /// </summary>
    /// <param name="player">The player who selected the object.</param>
    /// <param name="objekt">The object that was selected.</param>
    /// <param name="model">The model ID of the selected object.</param>
    /// <param name="position">The position of the selected object.</param>
    void OnObjectSelected(IPlayer player, IObject objekt, int model, Vector3 position);

    /// <summary>
    /// Called when a player object is selected by a player.
    /// </summary>
    /// <param name="player">The player who selected the object.</param>
    /// <param name="objekt">The player object that was selected.</param>
    /// <param name="model">The model ID of the selected object.</param>
    /// <param name="position">The position of the selected object.</param>
    void OnPlayerObjectSelected(IPlayer player, IPlayerObject objekt, int model, Vector3 position);

    /// <summary>
    /// Called when an object is edited by a player.
    /// </summary>
    /// <param name="player">The player who edited the object.</param>
    /// <param name="objekt">The object that was edited.</param>
    /// <param name="response">The response of the edit operation.</param>
    /// <param name="offset">The offset of the edited object.</param>
    /// <param name="rotation">The rotation of the edited object.</param>
    void OnObjectEdited(IPlayer player, IObject objekt, ObjectEditResponse response, Vector3 offset, Vector3 rotation);

    /// <summary>
    /// Called when a player object is edited by a player.
    /// </summary>
    /// <param name="player">The player who edited the object.</param>
    /// <param name="objekt">The player object that was edited.</param>
    /// <param name="response">The response of the edit operation.</param>
    /// <param name="offset">The offset of the edited object.</param>
    /// <param name="rotation">The rotation of the edited object.</param>
    void OnPlayerObjectEdited(IPlayer player, IPlayerObject objekt, ObjectEditResponse response, Vector3 offset, Vector3 rotation);

    /// <summary>
    /// Called when a player edits an attached object.
    /// </summary>
    /// <param name="player">The player who edited the attached object.</param>
    /// <param name="index">The index of the attachment slot.</param>
    /// <param name="saved">A value indicating whether the changes were saved.</param>
    /// <param name="data">The data of the edited attachment slot.</param>
    void OnPlayerAttachedObjectEdited(IPlayer player, int index, bool saved, ref ObjectAttachmentSlotData data);
}