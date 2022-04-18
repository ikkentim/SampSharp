// SampSharp
// Copyright 2022 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace TestMode.GameMode;

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
                player.GetAnimationName(out _, out _);
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