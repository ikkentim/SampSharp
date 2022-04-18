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
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using TestMode.Entities.Components;
using TestMode.Entities.Services;

namespace TestMode.Entities.Systems.Tests;

public class SystemDiTest : ISystem
{
    [Event]
    public void OnGameModeInit(IVehicleRepository vehiclesRepository)
    {
        vehiclesRepository.Foo();
    }

    [Event]
    public void OnPlayerConnect(Player player, IVehicleRepository vehiclesRepository)
    {
        player.AddComponent<TestComponent>();

        vehiclesRepository.FooForPlayer(player.Entity);
    }


    [Event]
    public void OnPlayerConnect(Player player, IScopedFunnyService scoped, IFunnyService transient,
        IServiceProvider serviceProvider)
    {
        Console.WriteLine("T: " + transient.FunnyGuid);
        Console.WriteLine("S: " + scoped.FunnyGuid);
        var s2 = serviceProvider.GetRequiredService<IScopedFunnyService>().FunnyGuid;
        var t2 = serviceProvider.GetRequiredService<IFunnyService>().FunnyGuid;
        Console.WriteLine("T2: " + t2);
        Console.WriteLine("S2: " + s2);
    }

    [Event]
    public void OnPlayerText(TestComponent test, string text, IScopedFunnyService scoped, IFunnyService transient,
        IServiceProvider serviceProvider)
    {
        Console.WriteLine("T: " + transient.FunnyGuid);
        Console.WriteLine("S: " + scoped.FunnyGuid);
        var s2 = serviceProvider.GetRequiredService<IScopedFunnyService>().FunnyGuid;
        var t2 = serviceProvider.GetRequiredService<IFunnyService>().FunnyGuid;
        Console.WriteLine("T2: " + t2);
        Console.WriteLine("S2: " + s2);
        Console.WriteLine(test.WelcomingMessage);
    }
}