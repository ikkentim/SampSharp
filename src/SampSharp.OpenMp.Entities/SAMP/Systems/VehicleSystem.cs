using SampSharp.OpenMp.Core.Api;

namespace SampSharp.Entities.SAMP;

internal class VehicleSystem : DisposableSystem, IVehicleEventHandler
{
    private readonly IOmpEntityProvider _entityProvider;
    private readonly IEventDispatcher _eventDispatcher;

    public VehicleSystem(IEventDispatcher eventDispatcher, IOmpEntityProvider entityProvider, SampSharpEnvironment environment)
    {
        _eventDispatcher = eventDispatcher;
        _entityProvider = entityProvider;
        AddDisposable(environment.AddEventHandler<IVehiclesComponent, IVehicleEventHandler>(x => x.GetEventDispatcher(), this));
    }

    public void OnVehicleStreamIn(IVehicle vehicle, IPlayer player)
    {
        _eventDispatcher.Invoke("OnVehicleStreamIn", 
            _entityProvider.GetEntity(vehicle), 
            _entityProvider.GetEntity(player));
    }

    public void OnVehicleStreamOut(IVehicle vehicle, IPlayer player)
    {
        _eventDispatcher.Invoke("OnVehicleStreamOut", 
            _entityProvider.GetEntity(vehicle), 
            _entityProvider.GetEntity(player));
    }

    public void OnVehicleDeath(IVehicle vehicle, IPlayer player)
    {
        _eventDispatcher.Invoke("OnVehicleDeath", 
            _entityProvider.GetEntity(vehicle), 
            _entityProvider.GetEntity(player));
    }

    public void OnPlayerEnterVehicle(IPlayer player, IVehicle vehicle, bool passenger)
    {
        _eventDispatcher.Invoke("OnPlayerEnterVehicle",
            _entityProvider.GetEntity(player),
            _entityProvider.GetEntity(vehicle),
            passenger);
    }

    public void OnPlayerExitVehicle(IPlayer player, IVehicle vehicle)
    {
        _eventDispatcher.Invoke("OnPlayerExitVehicle",
            _entityProvider.GetEntity(player),
            _entityProvider.GetEntity(vehicle));
    }

    public void OnVehicleDamageStatusUpdate(IVehicle vehicle, IPlayer player)
    {
        _eventDispatcher.Invoke("OnVehicleDamageStatusUpdate",
            _entityProvider.GetEntity(vehicle),
            _entityProvider.GetEntity(player));
    }

    public bool OnVehiclePaintJob(IPlayer player, IVehicle vehicle, int paintJob)
    {
        return _eventDispatcher.InvokeAs("OnVehiclePaintJob", true,
            _entityProvider.GetEntity(player),
            _entityProvider.GetEntity(vehicle),
            paintJob);
    }

    public bool OnVehicleMod(IPlayer player, IVehicle vehicle, int component)
    {
        return _eventDispatcher.InvokeAs("OnVehicleMod", true,
            _entityProvider.GetEntity(player),
            _entityProvider.GetEntity(vehicle),
            component);
    }

    public bool OnVehicleRespray(IPlayer player, IVehicle vehicle, int colour1, int colour2)
    {
        return _eventDispatcher.InvokeAs("OnVehicleRespray", true,
            _entityProvider.GetEntity(player),
            _entityProvider.GetEntity(vehicle),
            colour1,
            colour2);
    }

    public void OnEnterExitModShop(IPlayer player, bool enterexit, int interiorId)
    {
        _eventDispatcher.Invoke("OnEnterExitModShop",
            _entityProvider.GetEntity(player),
            enterexit,
            interiorId);
    }

    public void OnVehicleSpawn(IVehicle vehicle)
    {
        _eventDispatcher.Invoke("OnVehicleSpawn", _entityProvider.GetEntity(vehicle));
    }

    public bool OnUnoccupiedVehicleUpdate(IVehicle vehicle, IPlayer player, UnoccupiedVehicleUpdate updateData)
    {
        return _eventDispatcher.InvokeAs("OnUnoccupiedVehicleUpdate", true,
            _entityProvider.GetEntity(vehicle),
            _entityProvider.GetEntity(player),
            updateData.position,
            updateData.velocity,
            (int)updateData.seat);
    }

    public bool OnTrailerUpdate(IPlayer player, IVehicle trailer)
    {
        return _eventDispatcher.InvokeAs("OnTrailerUpdate", true,
            _entityProvider.GetEntity(player),
            _entityProvider.GetEntity(trailer));
    }

    public bool OnVehicleSirenStateChange(IPlayer player, IVehicle vehicle, byte sirenState)
    {
        return _eventDispatcher.InvokeAs("OnVehicleSirenStateChange", true,
            _entityProvider.GetEntity(player),
            _entityProvider.GetEntity(vehicle),
            sirenState);
    }
}