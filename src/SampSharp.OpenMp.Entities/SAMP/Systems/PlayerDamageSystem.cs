using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

internal class PlayerDamageSystem : DisposableSystem, IPlayerDamageEventHandler
{
    private readonly IOmpEntityProvider _entityProvider;
    private readonly IEventDispatcher _eventDispatcher;

    public PlayerDamageSystem(IEventDispatcher eventDispatcher, IOmpEntityProvider entityProvider, SampSharpEnvironment environment)
    {
        _eventDispatcher = eventDispatcher;
        _entityProvider = entityProvider;
        AddDisposable(environment.AddEventHandler(x => x.GetPlayers().GetPlayerDamageDispatcher(), this));
    }

    public void OnPlayerDeath(IPlayer player, IPlayer killer, int reason)
    {
        _eventDispatcher.Invoke("OnPlayerDeath", _entityProvider.GetEntity(player), _entityProvider.GetEntity(killer), reason);
    }

    public void OnPlayerTakeDamage(IPlayer player, IPlayer from, float amount, uint weapon, OpenMp.Core.Api.BodyPart part)
    {
        _eventDispatcher.Invoke("OnPlayerTakeDamage", _entityProvider.GetEntity(player), _entityProvider.GetEntity(from), amount, weapon, part);
    }

    public void OnPlayerGiveDamage(IPlayer player, IPlayer to, float amount, uint weapon, OpenMp.Core.Api.BodyPart part)
    {
        _eventDispatcher.Invoke("OnPlayerGiveDamage", _entityProvider.GetEntity(player), _entityProvider.GetEntity(to), amount, weapon, part);
    }
}