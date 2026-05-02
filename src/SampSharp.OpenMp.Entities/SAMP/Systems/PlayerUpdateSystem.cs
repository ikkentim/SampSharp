using SampSharp.OpenMp.Core.Api;
using SampSharp.OpenMp.Core.Std.Chrono;

namespace SampSharp.Entities.SAMP;

internal class PlayerUpdateSystem : DisposableSystem, IPlayerUpdateEventHandler
{
    private readonly IOmpEntityProvider _entityProvider;
    private readonly IEventDispatcher _eventDispatcher;

    public PlayerUpdateSystem(IEventDispatcher eventDispatcher, IOmpEntityProvider entityProvider, SampSharpEnvironment environment)
    {
        _eventDispatcher = eventDispatcher;
        _entityProvider = entityProvider;
        AddDisposable(environment.AddEventHandler(x => x.GetPlayers().GetPlayerUpdateDispatcher(), this));
    }

    public bool OnPlayerUpdate(IPlayer player, TimePoint now)
    {
        return _eventDispatcher.InvokeAs("OnPlayerUpdate", true, _entityProvider.GetEntity(player), now);
    }
}