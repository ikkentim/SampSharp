using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

internal class ObjectSystem : DisposableSystem, IObjectEventHandler
{
    private readonly IOmpEntityProvider _entityProvider;
    private readonly IEventDispatcher _eventDispatcher;

    public ObjectSystem(IEventDispatcher eventDispatcher, IOmpEntityProvider entityProvider, SampSharpEnvironment environment)
    {
        _eventDispatcher = eventDispatcher;
        _entityProvider = entityProvider;
        AddDisposable(environment.AddEventHandler<IObjectsComponent, IObjectEventHandler>(x => x.GetEventDispatcher(), this));
    }

    public void OnMoved(IObject objekt)
    {
        _eventDispatcher.Invoke("OnObjectMoved", _entityProvider.GetEntity(objekt));
    }

    public void OnPlayerObjectMoved(IPlayer player, IPlayerObject objekt)
    {
        _eventDispatcher.Invoke("OnPlayerObjectMoved", _entityProvider.GetEntity(player), _entityProvider.GetEntity(objekt, player));
    }

    public void OnObjectSelected(IPlayer player, IObject objekt, int model, Vector3 position)
    {
        _eventDispatcher.Invoke("OnObjectSelected", _entityProvider.GetEntity(player), _entityProvider.GetEntity(objekt), model, position);
    }

    public void OnPlayerObjectSelected(IPlayer player, IPlayerObject objekt, int model, Vector3 position)
    {
        _eventDispatcher.Invoke("OnPlayerObjectSelected", _entityProvider.GetEntity(player), _entityProvider.GetEntity(objekt, player), model, position);
    }

    public void OnObjectEdited(IPlayer player, IObject objekt, ObjectEditResponse response, Vector3 offset, Vector3 rotation)
    {
        _eventDispatcher.Invoke("OnObjectEdited", _entityProvider.GetEntity(player), _entityProvider.GetEntity(objekt), response, offset, rotation);
    }

    public void OnPlayerObjectEdited(IPlayer player, IPlayerObject objekt, ObjectEditResponse response, Vector3 offset, Vector3 rotation)
    {
        _eventDispatcher.Invoke("OnPlayerObjectEdited", _entityProvider.GetEntity(player), _entityProvider.GetEntity(objekt, player), response, offset, rotation);
    }

    public void OnPlayerAttachedObjectEdited(IPlayer player, int index, bool saved, ref ObjectAttachmentSlotData data)
    {
        _eventDispatcher.Invoke("OnPlayerAttachedObjectEdited", _entityProvider.GetEntity(player), index, saved, data);
    }
}