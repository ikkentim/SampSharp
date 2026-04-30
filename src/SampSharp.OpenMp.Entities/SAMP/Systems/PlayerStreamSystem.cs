using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

internal class PlayerStreamSystem : DisposableSystem, IPlayerStreamEventHandler
{
    private readonly IEventDispatcher _eventDispatcher;
    private readonly IOmpEntityProvider _entityProvider;

    public PlayerStreamSystem(IEventDispatcher eventDispatcher, IOmpEntityProvider entityProvider, SampSharpEnvironment environment)
    {
        _eventDispatcher = eventDispatcher;
        _entityProvider = entityProvider;
        AddDisposable(environment.AddEventHandler(x => x.GetPlayers().GetPlayerStreamDispatcher(), this));
    }

    public void OnPlayerStreamIn(IPlayer player, IPlayer forPlayer)
    {
        _eventDispatcher.Invoke("OnPlayerStreamIn", _entityProvider.GetEntity(player), _entityProvider.GetEntity(forPlayer));
    }

    public void OnPlayerStreamOut(IPlayer player, IPlayer forPlayer)
    {
        _eventDispatcher.Invoke("OnPlayerStreamOut", _entityProvider.GetEntity(player), _entityProvider.GetEntity(forPlayer));
    }
}