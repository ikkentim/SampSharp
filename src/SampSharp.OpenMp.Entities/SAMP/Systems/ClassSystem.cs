using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

internal class ClassSystem : DisposableSystem, IClassEventHandler
{
    private readonly IClassesComponent _classes;
    private readonly IOmpEntityProvider _entityProvider;
    private readonly IEventDispatcher _eventDispatcher;

    public ClassSystem(IEventDispatcher eventDispatcher, IOmpEntityProvider entityProvider, SampSharpEnvironment environment)
    {
        _eventDispatcher = eventDispatcher;
        _entityProvider = entityProvider;
        _classes = environment.Components.QueryComponent<IClassesComponent>();

        AddDisposable(environment.TryAddEventHandler<IClassesComponent, IClassEventHandler>(x => x.GetEventDispatcher(), this));
    }

    public bool OnPlayerRequestClass(IPlayer player, uint classId)
    {
        return _eventDispatcher.InvokeAs("OnPlayerRequestClass", true,
            _entityProvider.GetEntity(player), _entityProvider.GetEntity(_classes.AsPool().Get((int)classId)));
    }
}
