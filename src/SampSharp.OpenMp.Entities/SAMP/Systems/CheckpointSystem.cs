using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

internal class CheckpointSystem : DisposableSystem, IPlayerCheckpointEventHandler
{
    private readonly IOmpEntityProvider _entityProvider;
    private readonly IEventDispatcher _eventDispatcher;

    public CheckpointSystem(IOmpEntityProvider entityProvider, IEventDispatcher eventDispatcher,
        SampSharpEnvironment environment)
    {
        _entityProvider = entityProvider;
        _eventDispatcher = eventDispatcher;

        AddDisposable(environment.TryAddEventHandler<ICheckpointsComponent, IPlayerCheckpointEventHandler>(x => x.GetEventDispatcher(), this));
    }

    public void OnPlayerEnterCheckpoint(IPlayer player) =>
        _eventDispatcher.Invoke("OnPlayerEnterCheckpoint", _entityProvider.GetEntity(player));

    public void OnPlayerLeaveCheckpoint(IPlayer player) =>
        _eventDispatcher.Invoke("OnPlayerLeaveCheckpoint", _entityProvider.GetEntity(player));

    public void OnPlayerEnterRaceCheckpoint(IPlayer player) =>
        _eventDispatcher.Invoke("OnPlayerEnterRaceCheckpoint", _entityProvider.GetEntity(player));

    public void OnPlayerLeaveRaceCheckpoint(IPlayer player) =>
        _eventDispatcher.Invoke("OnPlayerLeaveRaceCheckpoint", _entityProvider.GetEntity(player));
}
