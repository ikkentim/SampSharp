using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

internal class PlayerChangeSystem : DisposableSystem, IPlayerChangeEventHandler
{
    private readonly IEventDispatcher _eventDispatcher;
    private readonly IOmpEntityProvider _entityProvider;

    public PlayerChangeSystem(IEventDispatcher eventDispatcher, IOmpEntityProvider entityProvider, SampSharpEnvironment environment)
    {
        _eventDispatcher = eventDispatcher;
        _entityProvider = entityProvider;
        AddDisposable(environment.AddEventHandler(x => x.GetPlayers().GetPlayerChangeDispatcher(), this));
    }

    public void OnPlayerScoreChange(IPlayer player, int score)
    {
        _eventDispatcher.Invoke("OnPlayerScoreChange", _entityProvider.GetEntity(player), score);
    }

    public void OnPlayerNameChange(IPlayer player, string oldName)
    {
        _eventDispatcher.Invoke("OnPlayerNameChange", _entityProvider.GetEntity(player), oldName);
    }

    public void OnPlayerInteriorChange(IPlayer player, uint newInterior, uint oldInterior)
    {
        _eventDispatcher.Invoke("OnPlayerInteriorChange", _entityProvider.GetEntity(player), newInterior, oldInterior);
    }

    public void OnPlayerStateChange(IPlayer player, SampSharp.OpenMp.Core.Api.PlayerState newState, SampSharp.OpenMp.Core.Api.PlayerState oldState)
    {
        _eventDispatcher.Invoke("OnPlayerStateChange", _entityProvider.GetEntity(player), newState, oldState);
    }

    public void OnPlayerKeyStateChange(IPlayer player, uint newKeys, uint oldKeys)
    {
        _eventDispatcher.Invoke("OnPlayerKeyStateChange", _entityProvider.GetEntity(player), newKeys, oldKeys);
    }
}