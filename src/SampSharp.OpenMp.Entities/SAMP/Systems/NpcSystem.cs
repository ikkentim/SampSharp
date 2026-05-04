using SampSharp.OpenMp.Core.Api;
using INPC = SampSharp.OpenMp.Core.Api.INPC;
using INPCComponent = SampSharp.OpenMp.Core.Api.INPCComponent;
using INPCEventHandler = SampSharp.OpenMp.Core.Api.INPCEventHandler;

namespace SampSharp.Entities.SAMP;

internal class NpcSystem : DisposableSystem, INPCEventHandler
{
    private readonly IOmpEntityProvider _entityProvider;
    private readonly IEventDispatcher _eventDispatcher;

    public NpcSystem(IOmpEntityProvider entityProvider, IEventDispatcher eventDispatcher, SampSharpEnvironment environment)
    {
        _entityProvider = entityProvider;
        _eventDispatcher = eventDispatcher;

        AddDisposable(environment.TryAddEventHandler<INPCComponent, INPCEventHandler>(x => x.GetEventDispatcher(), this));
    }

    public void OnNPCFinishMove(INPC npc)
    {
        _eventDispatcher.Invoke("OnNPCFinishMove", _entityProvider.GetEntity(npc));
    }

    public void OnNPCCreate(INPC npc)
    {
        _eventDispatcher.Invoke("OnNPCCreate", _entityProvider.GetEntity(npc));
    }

    public void OnNPCDestroy(INPC npc)
    {
        _eventDispatcher.Invoke("OnNPCDestroy", _entityProvider.GetEntity(npc));
    }

    public void OnNPCSpawn(INPC npc)
    {
        _eventDispatcher.Invoke("OnNPCSpawn", _entityProvider.GetEntity(npc));
    }

    public void OnNPCRespawn(INPC npc)
    {
        _eventDispatcher.Invoke("OnNPCRespawn", _entityProvider.GetEntity(npc));
    }

    public void OnNPCWeaponStateChange(INPC npc, PlayerWeaponState newState, PlayerWeaponState oldState)
    {
        _eventDispatcher.Invoke("OnNPCWeaponStateChange",
            _entityProvider.GetEntity(npc),
            newState,
            oldState);
    }

    public bool OnNPCTakeDamage(INPC npc, IPlayer damager, float damage, byte weapon, OpenMp.Core.Api.BodyPart bodyPart)
    {
        return _eventDispatcher.InvokeAs("OnNPCTakeDamage", true,
            _entityProvider.GetEntity(npc),
            _entityProvider.GetEntity(damager),
            damage,
            weapon,
            bodyPart);
    }

    public bool OnNPCGiveDamage(INPC npc, IPlayer damaged, float damage, byte weapon, OpenMp.Core.Api.BodyPart bodyPart)
    {
        return _eventDispatcher.InvokeAs("OnNPCGiveDamage", true,
            _entityProvider.GetEntity(npc),
            _entityProvider.GetEntity(damaged),
            damage,
            weapon,
            bodyPart);
    }

    public void OnNPCDeath(INPC npc, IPlayer killer, int reason)
    {
        _eventDispatcher.Invoke("OnNPCDeath",
            _entityProvider.GetEntity(npc),
            _entityProvider.GetEntity(killer),
            reason);
    }

    public bool OnNPCShotMissed(INPC npc, ref PlayerBulletData bulletData)
    {
        return _eventDispatcher.InvokeAs("OnNPCShotMissed", true,
            _entityProvider.GetEntity(npc),
            bulletData);
    }

    public bool OnNPCShotPlayer(INPC npc, IPlayer target, ref PlayerBulletData bulletData)
    {
        return _eventDispatcher.InvokeAs("OnNPCShotPlayer", true,
            _entityProvider.GetEntity(npc),
            _entityProvider.GetEntity(target),
            bulletData);
    }

    public bool OnNPCShotNPC(INPC npc, INPC target, ref PlayerBulletData bulletData)
    {
        return _eventDispatcher.InvokeAs("OnNPCShotNPC", true,
            _entityProvider.GetEntity(npc),
            _entityProvider.GetEntity(target),
            bulletData);
    }

    public bool OnNPCShotVehicle(INPC npc, IVehicle target, ref PlayerBulletData bulletData)
    {
        return _eventDispatcher.InvokeAs("OnNPCShotVehicle", true,
            _entityProvider.GetEntity(npc),
            _entityProvider.GetEntity(target),
            bulletData);
    }

    public bool OnNPCShotObject(INPC npc, IObject target, ref PlayerBulletData bulletData)
    {
        return _eventDispatcher.InvokeAs("OnNPCShotObject", true,
            _entityProvider.GetEntity(npc),
            _entityProvider.GetEntity(target),
            bulletData);
    }

    public bool OnNPCShotPlayerObject(INPC npc, IPlayerObject target, ref PlayerBulletData bulletData)
    {
        return _eventDispatcher.InvokeAs("OnNPCShotPlayerObject", true,
            _entityProvider.GetEntity(npc),
            _entityProvider.GetEntity(target),
            bulletData);
    }

    public void OnNPCPlaybackStart(INPC npc, int recordId)
    {
        _eventDispatcher.Invoke("OnNPCPlaybackStart",
            _entityProvider.GetEntity(npc),
            recordId);
    }

    public void OnNPCPlaybackEnd(INPC npc, int recordId)
    {
        _eventDispatcher.Invoke("OnNPCPlaybackEnd",
            _entityProvider.GetEntity(npc),
            recordId);
    }

    public void OnNPCFinishNodePoint(INPC npc, int nodeId, ushort pointId)
    {
        _eventDispatcher.Invoke("OnNPCFinishNodePoint",
            _entityProvider.GetEntity(npc),
            nodeId,
            pointId);
    }

    public void OnNPCFinishNode(INPC npc, int nodeId)
    {
        _eventDispatcher.Invoke("OnNPCFinishNode",
            _entityProvider.GetEntity(npc),
            nodeId);
    }

    public bool OnNPCChangeNode(INPC npc, int newNodeId, int oldNodeId)
    {
        return _eventDispatcher.InvokeAs("OnNPCChangeNode", true,
            _entityProvider.GetEntity(npc),
            newNodeId,
            oldNodeId);
    }

    public void OnNPCFinishMovePathPoint(INPC npc, int pathId, int pointId)
    {
        _eventDispatcher.Invoke("OnNPCFinishMovePathPoint",
            _entityProvider.GetEntity(npc),
            pathId,
            pointId);
    }

    public void OnNPCFinishMovePath(INPC npc, int pathId)
    {
        _eventDispatcher.Invoke("OnNPCFinishMovePath",
            _entityProvider.GetEntity(npc),
            pathId);
    }
}