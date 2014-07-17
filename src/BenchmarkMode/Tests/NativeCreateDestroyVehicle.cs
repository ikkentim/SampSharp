using SampSharp.GameMode.Natives;

namespace BenchmarkMode.Tests
{
    public class NativeCreateDestroyVehicle : ITest
    {
        public void Run(GameMode gameMode)
        {
            int id = Native.CreateVehicle(400, 0, 0, 0, 0, -1, -1, 0);
            Native.DestroyVehicle(id);
        }
    }
}
