// SampSharp
// Copyright 2018 Tim Potze
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
using System.IO;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;

namespace TestMode
{
    internal class GameMode : BaseMode
    {
        #region Overrides of BaseMode
        
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            
            Console.WriteLine("The game mode has loaded.");
            AddPlayerClass(0, Vector3.Zero, 0);

            var sampleVehicle = BaseVehicle.Create(VehicleModelType.Alpha, Vector3.One * 10, 0, -1, -1);

            Console.WriteLine("Spawned sample vehicle " + sampleVehicle.Model);

            var cfg = new ServerConfig(Path.Combine(Client.ServerPath, "server.cfg"));

            foreach (var kv in cfg)
            {
                Console.WriteLine(kv.Key + " " + kv.Value);
            }
        }

        #endregion
    }
}