using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

internal class ClassSystem : DisposableSystem, IClassEventHandler
{
    private readonly IOmpEntityProvider _entityProvider;
    private readonly IEventDispatcher _eventDispatcher;

    public ClassSystem(IEventDispatcher eventDispatcher, IOmpEntityProvider entityProvider, SampSharpEnvironment environment)
    {
        _eventDispatcher = eventDispatcher;
        _entityProvider = entityProvider;

        AddDisposable(environment.TryAddEventHandler<IClassesComponent, IClassEventHandler>(x => x.GetEventDispatcher(), this));
    }

    public bool OnPlayerRequestClass(IPlayer player, uint classId)
    {
        return _eventDispatcher.InvokeAs("OnPlayerRequestClass", true,
            _entityProvider.GetEntity(player), (int)classId);
    }
}
