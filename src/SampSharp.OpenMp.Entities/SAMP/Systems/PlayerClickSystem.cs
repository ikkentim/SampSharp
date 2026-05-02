using System.Numerics;
using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

internal class PlayerClickSystem : DisposableSystem, IPlayerClickEventHandler
{
    private readonly IEventDispatcher _eventDispatcher;
    private readonly IOmpEntityProvider _entityProvider;

    public PlayerClickSystem(IEventDispatcher eventDispatcher, IOmpEntityProvider entityProvider, SampSharpEnvironment environment)
    {
        _eventDispatcher = eventDispatcher;
        _entityProvider = entityProvider;
        AddDisposable(environment.AddEventHandler(x => x.GetPlayers().GetPlayerClickDispatcher(), this));
    }

    public void OnPlayerClickMap(IPlayer player, Vector3 pos)
    {
        _eventDispatcher.Invoke("OnPlayerClickMap", _entityProvider.GetEntity(player), pos);
    }

    public void OnPlayerClickPlayer(IPlayer player, IPlayer clicked, OpenMp.Core.Api.PlayerClickSource source)
    {
        _eventDispatcher.Invoke("OnPlayerClickPlayer", _entityProvider.GetEntity(player), _entityProvider.GetEntity(clicked), source);
    }
}