using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

internal class PlayerTextSystem : DisposableSystem, IPlayerTextEventHandler
{
    private readonly IEventDispatcher _eventDispatcher;
    private readonly IOmpEntityProvider _entityProvider;

    public PlayerTextSystem(IEventDispatcher eventDispatcher, IOmpEntityProvider entityProvider, SampSharpEnvironment environment)
    {
        _eventDispatcher = eventDispatcher;
        _entityProvider = entityProvider;
        AddDisposable(environment.AddEventHandler(x => x.GetPlayers().GetPlayerTextDispatcher(), this));
    }

    public bool OnPlayerText(IPlayer player, string message)
    {
        return _eventDispatcher.InvokeAs("OnPlayerText", true, _entityProvider.GetEntity(player), message);
    }

    public bool OnPlayerCommandText(IPlayer player, string message)
    {
        return _eventDispatcher.InvokeAs("OnPlayerCommandText", false, _entityProvider.GetEntity(player), message);
    }
}