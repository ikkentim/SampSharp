// SampSharp
// Copyright 2019 Tim Potze
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
using SampSharp.Entities.Events;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Components;
using SampSharp.Entities.SAMP.Definitions;
using TestMode.Entities.Components;
using TestMode.Entities.Services;

namespace TestMode.Entities.Systems
{
    public class TestSystem : ISystem
    {
        private GangZone _zone;

        [Event]
        public void  OnGameModeInit(IVehicleRepository vehiclesRepository,  IWorldService worldService) 
        {
            // Event methods have dependency injection alongside the arguments

            Console.WriteLine("Do game mode loading goodies...");

            vehiclesRepository.Foo();

            worldService.CreateActor(101, new Vector3(0, 0, 20), 0);

            var blue = Color.Blue;
            blue.A = 128;
            _zone = worldService.CreateGangZone(0, 0, 100, 100);
            _zone.Color = blue;
            _zone.Show();

            var obj = worldService.CreateObject((int) ObjectModel.A51GateconA, new Vector3(10, 10, 40), Vector3.Zero, 1000);
            obj.DisableCameraCollisions();

            worldService.CreateVehicle(VehicleModelType.Alpha, new Vector3(40, 40, 10), 0, 0, 0);

            var green = Color.Green;
            green.A = 128;
            worldService.CreateTextLabel("text", green, new Vector3(10, 10, 10), 1000);
        }

        [Event]
        public void OnPlayerCommandText(Player player, string text, IWorldService worldService)
        {
            if (text == "/actor")
            {
                worldService.CreateActor(0, player.Position + Vector3.Up, 0);
                player.SendClientMessage(-1, "Actor created!");
            }

            if (text == "/pos") player.SendClientMessage(-1, $"You are at {player.Position}");
        }

        [Event]
        public void OnPlayerConnect(Entity player, IVehicleRepository vehiclesRepository)
        {
            Console.WriteLine("I connected! " + player.Id);

            player.AddComponent<TestComponent>();

            _zone.Show(player);
            vehiclesRepository.FooForPlayer(player);
        }

        [Event]
        public void OnPlayerConnect(Player player)
        {
            player.SendClientMessage($"Hey there, {player.Name}");
        }

        [Event]
        public void OnPlayerText(TestComponent test, string text)
        {
            Console.WriteLine(test.WelcomingMessage + ":::" + text);
        }
    }
}