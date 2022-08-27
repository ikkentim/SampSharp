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
using SampSharp.Entities;
using SampSharp.Entities.SAMP;

namespace TestMode.Entities.Systems.IssueTests;

public class Issue419VehicleHealthOnGmx : ISystem
{
    [Event]
    public void OnGameModeExit(IEntityManager entityManager)
    {
        var vehicles = entityManager.GetComponents<Vehicle>();
        foreach (var vehicle in vehicles)
        {
            Console.WriteLine(vehicle.Health);  // always return 0?
        }
    }
}