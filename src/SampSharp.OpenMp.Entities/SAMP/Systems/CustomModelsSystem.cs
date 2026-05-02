using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

internal class CustomModelsSystem : DisposableSystem, IPlayerModelsEventHandler
{
    private readonly IOmpEntityProvider _entityProvider;
    private readonly IEventDispatcher _eventDispatcher;

    public CustomModelsSystem(IEventDispatcher eventDispatcher, IOmpEntityProvider entityProvider,
        SampSharpEnvironment environment)
    {
        _eventDispatcher = eventDispatcher;
        _entityProvider = entityProvider;

        AddDisposable(environment.TryAddEventHandler<ICustomModelsComponent, IPlayerModelsEventHandler>(x => x.GetEventDispatcher(), this));
    }

    public void OnPlayerFinishedDownloading(IPlayer player)
    {
        _eventDispatcher.Invoke("OnPlayerFinishedDownloading", _entityProvider.GetEntity(player));
    }

    public bool OnPlayerRequestDownload(IPlayer player, ModelDownloadType type, uint checksum)
    {
        return _eventDispatcher.InvokeAs("OnPlayerRequestDownload", true,
            _entityProvider.GetEntity(player), (int)type, (int)checksum);
    }
}
