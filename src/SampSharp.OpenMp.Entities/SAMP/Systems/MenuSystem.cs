using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

internal class MenuSystem : DisposableSystem, IMenuEventHandler
{
    private readonly IEventDispatcher _eventDispatcher;
    private readonly IOmpEntityProvider _entityProvider;

    public MenuSystem(IEventDispatcher eventDispatcher, IOmpEntityProvider entityProvider, SampSharpEnvironment environment)
    {
        _eventDispatcher = eventDispatcher;
        _entityProvider = entityProvider;
        AddDisposable(environment.AddEventHandler<IMenusComponent, IMenuEventHandler>(x => x.GetEventDispatcher(), this));
    }

    public void OnPlayerSelectedMenuRow(IPlayer player, byte row)
    {
        _eventDispatcher.Invoke("OnPlayerSelectedMenuRow",
            _entityProvider.GetEntity(player),
            row);
    }

    public void OnPlayerExitedMenu(IPlayer player)
    {
        _eventDispatcher.Invoke("OnPlayerExitedMenu",
            _entityProvider.GetEntity(player));
    }
}