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
    public class BenchmarkCommands
    {
        [Command("benchmark")]
        public static void BenchmarkCommand(BasePlayer player, int runs = 10)
        {
            Stopwatch sw = new Stopwatch();
            List<TimeSpan> time = new List<TimeSpan>();

            player.SendClientMessage("Benchmark starting...");
            
            VehicleModelInfo inf = VehicleModelInfo.ForVehicle(VehicleModelType.AT400);

            for (var i = 0; i < runs; i++)
            {
                sw.Reset();

                sw.Start();

                for (var j = 0; j < 1000; j++)
                {
                    var netstat = Server.NetworkStats;
                    Server.SetWeather(10);
                    Server.SetWorldTime(14);
                    var plnam = player.Name;
                    player.GetAnimationName(out var lib, out var name);
                    var offset = inf[VehicleModelInfoType.FrontSeat];
                    var ppos = player.Position;
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
