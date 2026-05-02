using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

internal class PlayerShotSystem : DisposableSystem, IPlayerShotEventHandler
{
    private readonly IOmpEntityProvider _entityProvider;
    private readonly IEventDispatcher _eventDispatcher;

    public PlayerShotSystem(IEventDispatcher eventDispatcher, IOmpEntityProvider entityProvider, SampSharpEnvironment environment)
    {
        _eventDispatcher = eventDispatcher;
        _entityProvider = entityProvider;
        AddDisposable(environment.AddEventHandler(x => x.GetPlayers().GetPlayerShotDispatcher(), this));
    }

    public bool OnPlayerShotMissed(IPlayer player, ref PlayerBulletData bulletData)
    {
        return _eventDispatcher.InvokeAs("OnPlayerShotMissed", true, _entityProvider.GetEntity(player), bulletData);
    }

    public bool OnPlayerShotPlayer(IPlayer player, IPlayer target, ref PlayerBulletData bulletData)
    {
        return _eventDispatcher.InvokeAs("OnPlayerShotPlayer", true, _entityProvider.GetEntity(player), _entityProvider.GetEntity(target), bulletData);
    }

    public bool OnPlayerShotVehicle(IPlayer player, IVehicle target, ref PlayerBulletData bulletData)
    {
        return _eventDispatcher.InvokeAs("OnPlayerShotVehicle", true, _entityProvider.GetEntity(player), _entityProvider.GetEntity(target), bulletData);
    }

    public bool OnPlayerShotObject(IPlayer player, IObject target, ref PlayerBulletData bulletData)
    {
        return _eventDispatcher.InvokeAs("OnPlayerShotObject", true, _entityProvider.GetEntity(player), _entityProvider.GetEntity(target), bulletData);
    }

    public bool OnPlayerShotPlayerObject(IPlayer player, IPlayerObject target, ref PlayerBulletData bulletData)
    {
        return _eventDispatcher.InvokeAs("OnPlayerShotPlayerObject", true, _entityProvider.GetEntity(player), _entityProvider.GetEntity(target, player), bulletData);
    }
}