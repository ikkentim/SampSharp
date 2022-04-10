using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace TestMode.GameMode
{
    public static class BenchmarkCommands
    {
        [Command("benchmark")]
        public static void BenchmarkCommand(BasePlayer player, int runs = 10)
        {
            var sw = new Stopwatch();
            var time = new List<TimeSpan>();

            player.SendClientMessage("Benchmark starting...");
            
            var inf = VehicleModelInfo.ForVehicle(VehicleModelType.AT400);

            for (var i = 0; i < runs; i++)
            {
                sw.Reset();

                sw.Start();

                for (var j = 0; j < 1000; j++)
                {
                    _ = Server.NetworkStats;
                    Server.SetWeather(10);
                    Server.SetWorldTime(14);
                    _ = player.Name;
                    player.GetAnimationName(out var lib, out var name);
                    _ = inf[VehicleModelInfoType.FrontSeat];
                    _ = player.Position;
                }

                sw.Stop();

                time.Add(sw.Elapsed);
            }

            for (var i = 0; i < runs; i++)
            {
                player.SendClientMessage($"{i}: {time[i]}");
                Console.WriteLine($"{i}: {time[i]}");
            }

            var avg = time.Aggregate((a, b) => a + b) / time.Count;

            player.SendClientMessage($"AVG: {avg}");
            Console.WriteLine($"AVG: {avg}");
        }
    }
}
