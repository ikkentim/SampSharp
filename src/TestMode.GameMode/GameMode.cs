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
using System.Threading.Tasks;
using SampSharp.Core;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.World;

namespace TestMode.GameMode
{
    public class GameMode : BaseMode
    {
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Client.UnhandledException += (sender, args) => Console.WriteLine($"ERROR! {args.Exception}");

            Console.WriteLine("The game mode has loaded.");
            
            AddPlayerClass(0, new Vector3(1482.9055, 1504.2122, 10.5474), 0);

            BaseVehicle.Create(VehicleModelType.BF400, new Vector3(1489.9055, 1520.2122, 11), 0, 3, 3);
            BaseVehicle.Create(VehicleModelType.Banshee, new Vector3(1449.9055, 1520.2122, 11), 0, 3, 3);
            BaseVehicle.Create(VehicleModelType.Cabbie, new Vector3(1449.9055, 1550.2122, 11), 0, 3, 3);
            
            Task.Run(Testing);
        }

        private async Task Testing()
        {
            Console.WriteLine("Invoke required? before " + Client.SynchronizationProvider.InvokeRequired);
            await TaskHelper.SwitchToMainThread();
            Console.WriteLine("Invoke required? after " + Client.SynchronizationProvider.InvokeRequired);
        }
    }
}