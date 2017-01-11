// SampSharp
// Copyright 2017 Tim Potze
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
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace TestMode.World
{
    [PooledType]
    [CommandGroup("playertest")]
    public class Player : BasePlayer
    {
        [Command("spawnbmx")]
        public void SpawnVehicle()
        {
            var v = BaseVehicle.Create(VehicleModelType.BMX, Position + new Vector3(0, 0, 0.5f), 0, -1, -1);
            PutInVehicle(v);
        }

        [Command("spawn")]
        public void SpawnVehicle(VehicleModelType model)
        {
            var v = BaseVehicle.Create(model, Position + new Vector3(0, 0, 0.5f), 0, -1, -1);
            PutInVehicle(v);
        }
    }
}