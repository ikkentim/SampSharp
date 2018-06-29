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
using SampSharp.GameMode.Events;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;

namespace TestMode
{
    internal class GameMode : BaseMode
    {
        private int _ticks;

        #region Overrides of BaseMode

        protected override void OnTick(EventArgs e)
        {
            base.OnTick(e);

            if (_ticks++ % 5000 == 0)
                Console.WriteLine("Server is still ticking...");
        }
        
        protected override async void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            
            Mapper.Initialize(configuration =>
            {
                configuration.CreateMap<Player, PlayerDto>()
                    .ForMember(item => item.Id, options => options.Ignore())
                    .ForMember(item => item.PositionX, options => options.MapFrom(player => player.Position.X))
                    .ForMember(item => item.PositionY, options => options.MapFrom(player => player.Position.Y))
                    .ForMember(item => item.PositionZ, options => options.MapFrom(player => player.Position.Z));

                configuration.CreateMap<PlayerDto, Player>()
                    .ForMember(item => item.Id, options => options.Ignore())
                    .ForMember(item => item.Name, options => options.Ignore())
                    .ForMember(item => item.Position,
                        options => options.MapFrom(playerDto => new Vector3(playerDto.PositionX, playerDto.PositionY, playerDto.PositionZ)));
            });

            Console.WriteLine("The game mode has loaded.");
            AddPlayerClass(0, Vector3.Zero, 0);

            SetGameModeText("Before delay");
            await Task.Delay(10);

            Console.WriteLine("waited 2");
            SetGameModeText("After delay");

            Console.WriteLine("RCON commands: sd (shutdown) msg (repeat message)");
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

                Console.WriteLine("Sending: " + msg);

                e.Success = true;
                Server.Print(msg);
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