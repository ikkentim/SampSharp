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

using SampSharp.Entities;
using SampSharp.Entities.SAMP;

namespace TestMode.Entities.Systems.Tests;

public class SystemVehicleTest : ISystem
{
    [Event]
    public void OnGameModeInit(IWorldService worldService)
    {
        worldService.CreateVehicle(VehicleModelType.Alpha, new Vector3(40, 40, 10), 0, 0, 0);
    }
}