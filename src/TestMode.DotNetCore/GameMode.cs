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
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
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
        }

        protected override void OnRconCommand(RconEventArgs e)
        {
            Console.WriteLine($"Received RCON Command: {e.Command}");
            
            if (e.Command == "sd")
            {
                Client.ShutDown();
                e.Success = true;
                Console.WriteLine("Shutting down client...");
            }

            if (e.Command.StartsWith("msg"))
            {
                var msg = e.Command.Substring(4);

                Console.WriteLine("Received: " + msg);
                msg = new string(msg.Reverse().ToArray());

                Console.WriteLine("Reversed: " + msg);

                e.Success = true;
                Server.Print(msg);
            }

            base.OnRconCommand(e);
        }
        
        protected override void OnIncomingConnection(ConnectionEventArgs e)
        {
            Console.WriteLine($"Incomming connection: Player {e.PlayerId} from {e.IpAddress}:{e.Port}");
            base.OnIncomingConnection(e);
        }

        protected override void OnPlayerConnected(BasePlayer player, EventArgs e)
        {
            Console.WriteLine($"Player {player.Name} connected.");
            base.OnPlayerConnected(player, e);
        }

        protected override void OnPlayerDisconnected(BasePlayer player, DisconnectEventArgs e)
        {
            Console.WriteLine($"Player {player.Name} disconnected. Reason: {e.Reason}.");
            base.OnPlayerDisconnected(player, e);
        }

        #endregion
    }
}