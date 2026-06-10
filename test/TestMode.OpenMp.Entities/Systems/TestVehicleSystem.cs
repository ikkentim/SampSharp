using System.Numerics;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TestMode.OpenMp.Entities.Components;

namespace TestMode.OpenMp.Entities.Systems;

public class TestVehicleSystem : ISystem
{

    [Event]
    public void OnGameModeInit(IWorldService world)
    {
        var vehicle = world.CreateVehicle(VehicleModelType.Landstalker, new Vector3(0, 6, 15), 45, 4, 4);
        vehicle.Colors = (5, 12);
        vehicle.Bonnet = true;
        vehicle.SetNumberPlate("SampSharp");
        vehicle.AddComponent<LicensePlateComponent>("SampSharp");
    }
}