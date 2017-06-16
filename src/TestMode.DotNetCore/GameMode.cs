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

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using SampSharp.GameMode;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace TestMode
{
    internal class GameMode : BaseMode
    {
        [Command("myfirstcommand")]
        public static void MyFirstCommand(BasePlayer player, string message)
        {
            player.SendClientMessage($"Hello, world! You said {message}");
        }

        [Command("pos")]
        public static async void PositionCommand(BasePlayer player)
        {
            player.SendClientMessage(Color.Yellow, $"Position: {player.Position}");

            await Task.Delay(1000);

            player.SendClientMessage("Still here!");
        }

        [Command("kick")]
        public static void Kick(BasePlayer player, BasePlayer target)
        {
            target.Kick();
        }
        
        #region Overrides of BaseMode

        protected override async void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Console.WriteLine("The game mode has loaded.");
            AddPlayerClass(0, Vector3.Zero, 0);
            SetGameModeText("Before delay");
            await Task.Delay(2000);

            Console.WriteLine("waited 2");
            SetGameModeText("After delay");
        }

        protected override void OnRconCommand(RconEventArgs e)
        {
            Console.WriteLine($"Received RCON Command: {e.Command}");

            if (e.Command == "sd")
            {
                Client.ShutDown();
            }
            base.OnRconCommand(e);
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