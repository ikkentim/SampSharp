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
using System.Diagnostics;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;

namespace TestMode.Entities.Systems.IssueTests;

public class Issue326Timers : ISystem
{
    private readonly Stopwatch _stopwatch1 = new();
    private readonly Stopwatch _stopwatch2 = new();
    private int _readonlyTicks;

    [Timer(60000)]
    public void Every60Sec(IServerService serverService)
    {
        Console.WriteLine($"Every 60 seconds timer! {_stopwatch1.Elapsed}, tick rate: {serverService.TickRate}");
        _stopwatch1.Restart();
    }

    [Event]
    public void OnGameModeInit(ITimerService timerService)
    {
        _stopwatch1.Start();
        _stopwatch2.Start();

        timerService.Start((_, timer) =>
        {
            if (++_readonlyTicks == 3)
            {
                Console.WriteLine("Stop timer");
                timerService.Stop(timer);
            }

            Console.WriteLine($"Manual timer {_stopwatch2.Elapsed}");
            _stopwatch2.Restart();
        }, TimeSpan.FromSeconds(0.1));
    }
}