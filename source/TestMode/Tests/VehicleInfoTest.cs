using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.World;

namespace TestMode.Tests
{
    public class VehicleInfoTest : ITest
    {
        public void Start(GameMode gameMode)
        {
            var veh = Vehicle.Create(VehicleModelType.Burrito, new Vector(5), 0, -1, -1);
            Console.WriteLine("Vehiclesize of {1}: {0}", veh.Info[VehicleModelInfoType.Size], veh.Info.Name);
        }
    }
}
