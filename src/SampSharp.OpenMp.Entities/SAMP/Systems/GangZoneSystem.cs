using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

internal class GangZoneSystem : DisposableSystem, IGangZoneEventHandler
{
    private readonly IEventDispatcher _eventDispatcher;
    private readonly IOmpEntityProvider _entityProvider;

    public GangZoneSystem(IEventDispatcher eventDispatcher, IOmpEntityProvider entityProvider, SampSharpEnvironment environment)
    {
        _eventDispatcher = eventDispatcher;
        _entityProvider = entityProvider;
        AddDisposable(environment.AddEventHandler<IGangZonesComponent, IGangZoneEventHandler>(x => x.GetEventDispatcher(), this));
    }

    public void OnPlayerEnterGangZone(IPlayer player, IGangZone zone) =>
        Dispatch("OnPlayerEnterGangZone", "OnPlayerEnterPlayerGangZone", player, zone);

    public void OnPlayerLeaveGangZone(IPlayer player, IGangZone zone) =>
        Dispatch("OnPlayerLeaveGangZone", "OnPlayerLeavePlayerGangZone", player, zone);

    public void OnPlayerClickGangZone(IPlayer player, IGangZone zone) =>
        Dispatch("OnPlayerClickGangZone", "OnPlayerClickPlayerGangZone", player, zone);

    private void Dispatch(string globalName, string perPlayerName, IPlayer player, IGangZone zone)
    {
        var name = zone.GetLegacyPlayer().HasValue ? perPlayerName : globalName;
        _eventDispatcher.Invoke(name, _entityProvider.GetEntity(player), _entityProvider.GetEntity(zone));
    }
}