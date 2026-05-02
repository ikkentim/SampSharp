using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

internal class PickupSystem : DisposableSystem, IPickupEventHandler
{
    private readonly IOmpEntityProvider _entityProvider;
    private readonly IEventDispatcher _eventDispatcher;

    public PickupSystem(IEventDispatcher eventDispatcher, IOmpEntityProvider entityProvider, SampSharpEnvironment environment)
    {
        _eventDispatcher = eventDispatcher;
        _entityProvider = entityProvider;
        AddDisposable(environment.AddEventHandler<IPickupsComponent, IPickupEventHandler>(x => x.GetEventDispatcher(), this));
    }

    public void OnPlayerPickUpPickup(IPlayer player, IPickup pickup)
    {
        var name = pickup.GetLegacyPlayer().HasValue
            ? "OnPlayerPickUpPlayerPickup"
            : "OnPlayerPickUpPickup";
        _eventDispatcher.Invoke(name,
            _entityProvider.GetEntity(player),
            _entityProvider.GetEntity(pickup));
    }
}