using SampSharp.GameMode.World;

namespace BenchmarkMode.Tests
{
    public class CreateDestroyVehicle : ITest
    {
        public void Run(GameMode gameMode)
        {
            var v = Vehicle.Create(400, new Vector(), 0, -1, -1);
            v.Dispose();
        }
    }
}
