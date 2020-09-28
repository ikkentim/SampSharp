using System;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;

namespace TestMode.Entities.Systems.Tests
{
    public class SystemVehicleInfoTest : ISystem
    {
        [Event]
        public void OnGameModeInit(IVehicleInfoService vehicleInfoService)
        {
            var size = vehicleInfoService.GetModelInfo(VehicleModelType.AT400, VehicleModelInfoType.Size);
            Console.WriteLine($"AT400 size {size}");
        }
    }
}