using Microsoft.Extensions.DependencyInjection;

namespace SampSharp.Entities.SAMP;

internal static class ServiceCollectionSampExtensions
{
    public static IServiceCollection AddSamp(this IServiceCollection services)
    {
        return services.AddSingleton<IOmpEntityProvider, OmpEntityProvider>()
            .AddSingleton<IServerService, ServerService>()
            .AddSingleton<IWorldService, WorldService>()
            .AddSingleton<IVehicleInfoService, VehicleInfoService>()
            .AddSingleton<IDialogService, DialogService>()
            .AddSystem<CheckpointSystem>()
            .AddSystem<ClassSystem>()
            .AddSystem<CustomModelsSystem>()
            .AddSystem<DialogSystem>()
            .AddSystem<ActorSystem>()
            .AddSystem<ConsoleSystem>()
            .AddSystem<GangZoneSystem>()
            .AddSystem<MenuSystem>()
            .AddSystem<NpcSystem>()
            .AddSystem<ObjectSystem>()
            .AddSystem<PickupSystem>()
            .AddSystem<PlayerChangeSystem>()
            .AddSystem<PlayerCheckSystem>()
            .AddSystem<PlayerClickSystem>()
            .AddSystem<PlayerConnectSystem>()
            .AddSystem<PlayerDamageSystem>()
            .AddSystem<PlayerShotSystem>()
            .AddSystem<PlayerSpawnSystem>()
            .AddSystem<PlayerStreamSystem>()
            .AddSystem<PlayerTextSystem>()
            .AddSystem<PlayerUpdateSystem>()
            .AddSystem<TextDrawSystem>()
            .AddSystem<VehicleSystem>();
    }
}
