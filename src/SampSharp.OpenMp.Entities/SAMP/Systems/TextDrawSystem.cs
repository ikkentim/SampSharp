using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

internal class TextDrawSystem : DisposableSystem, ITextDrawEventHandler
{
    private readonly IOmpEntityProvider _entityProvider;
    private readonly IEventDispatcher _eventDispatcher;

    public TextDrawSystem(IEventDispatcher eventDispatcher, IOmpEntityProvider entityProvider, SampSharpEnvironment environment)
    {
        _eventDispatcher = eventDispatcher;
        _entityProvider = entityProvider;
        AddDisposable(environment.AddEventHandler<ITextDrawsComponent, ITextDrawEventHandler>(x => x.GetEventDispatcher(), this));
    }

    public void OnPlayerClickTextDraw(IPlayer player, ITextDraw td)
    {
        _eventDispatcher.Invoke("OnPlayerClickTextDraw",
            _entityProvider.GetEntity(player),
            _entityProvider.GetEntity(td));
    }

    public void OnPlayerClickPlayerTextDraw(IPlayer player, IPlayerTextDraw td)
    {
        _eventDispatcher.Invoke("OnPlayerClickPlayerTextDraw",
            _entityProvider.GetEntity(player),
            _entityProvider.GetEntity(td, player));
    }

    public bool OnPlayerCancelTextDrawSelection(IPlayer player)
    {
        return _eventDispatcher.InvokeAs("OnPlayerCancelTextDrawSelection", true,
            _entityProvider.GetEntity(player));
    }

    public bool OnPlayerCancelPlayerTextDrawSelection(IPlayer player)
    {
        return _eventDispatcher.InvokeAs("OnPlayerCancelPlayerTextDrawSelection", true,
            _entityProvider.GetEntity(player));
    }
}