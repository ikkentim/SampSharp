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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Components;
using SampSharp.Entities.SAMP.Definitions;
using SampSharp.Entities.SAMP.Dialogs;
using TestMode.Entities.Components;
using TestMode.Entities.Services;

namespace TestMode.Entities.Systems
{
    public class TestSystem : ISystem
    {
        private TextDraw _welcome;
        private GangZone _zone;
        private Menu _menu;

        [Event]
        public void OnGameModeInit(IVehicleRepository vehiclesRepository,  IWorldService worldService, IServerService serverService) 
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

            var obj = worldService.CreateObject(16638, new Vector3(10, 10, 40), Vector3.Zero, 1000);
            obj.DisableCameraCollisions();

            worldService.CreateVehicle(VehicleModelType.Alpha, new Vector3(40, 40, 10), 0, 0, 0);

            var green = Color.Green;
            green.A = 128;
            worldService.CreateTextLabel("text", green, new Vector3(10, 10, 10), 1000);

            _welcome = worldService.CreateTextDraw(new Vector2(20, 40), "Hello, world");
            _welcome.Alignment = TextDrawAlignment.Left;
            _welcome.Font = TextDrawFont.Diploma;
            _welcome.Proportional = true;
            Console.WriteLine("TD pos: " + _welcome.Position);
            Console.WriteLine(_welcome.Entity.Id.ToString());

            _menu = worldService.CreateMenu("Test menu", new Vector2(200, 300), 100);
            _menu.AddItem("Hello!!!");
            _menu.AddItem("Hello!!");
            _menu.AddItem("Hello!");

            var ctx = SynchronizationContext.Current;
            Task.Run(() =>
            {
                // ... Run things on a worker thread.

                ctx.Send(_ =>
                {
                    // ... Run things on the main thead.
                }, null);
            });

            serverService.SetGameModeText("SampSharp.Entities");
        }

        [Event]
        public void OnPlayerWeaponShot(Player player, Weapon weapon, int hitType, Entity hit, float x, float y, float z)
        {
            var pos = new Vector3(x, y, z);
            player.SendClientMessage($"You shot {hit} at {pos} with {weapon}");
        }

        [Event]
        public bool OnRconCommand(string cmd)
        {
            Console.WriteLine("RCON");

            if (cmd == "ret")
                return true;

            if(cmd == "err")
                throw new Exception("RCON threw an error");

            return false;
        }

        [Event]
        public async Task<bool> OnPlayerCommandText(Player player, string text, IDialogService dialogService, IWorldService worldService, IEntityManager entityManager)
        {
            if (text == "/menu")
            {
                _menu.Show(player.Entity);
                return true;
            }
            if (text == "/addcomponent")
            {
                if(player.GetComponent<TestComponent>() == null)
                    player.Entity.AddComponent<TestComponent>();

                player.SendClientMessage("Added TestComponent!");
                return true;
            }
            if (text == "/removecomponent")
            {
                player.Entity.Destroy<TestComponent>();

                player.SendClientMessage("Remove TestComponent!");
                return true;
            }
            if (text == "/getcomponents")
            {
                foreach (var comp in player.GetComponents<Component>())
                    player.SendClientMessage(comp.GetType().FullName);
                return true;
            }
            if (text == "/tablist")
            {
                dialogService.Show(player.Entity,
                    new TablistDialog("Hello", "Left", "right", "Column1", "Column2", "Column3")
                    {
                        {"r1c1", "r1c2", "r1c3"},
                        {new[] {"r2c1", "r2c2", "r2c3"}, @"Tag!!!"},
                        {"r3c1", "r3c2", $"{Color.Red}r3c3"},
                    },
                    r =>
                    {
                        player.SendClientMessage(
                            $"Resp: {r.Response} {r.ItemIndex}: ({string.Join(" ", r.Item?.Columns)},{r.Item?.Tag})");
                    });
                player.PlaySound(1083);
                return true;
            }
            if (text == "/list")
            {
                dialogService.Show(player.Entity, new ListDialog("Hello", "Left", "right")
                {
                    "item1",
                    "item2",
                    $"{Color.Red}item3"
                }, r =>
                {
                    player.SendClientMessage($"Resp: {r.Response} {r.ItemIndex}: ({r.Item?.Text},{r.Item?.Tag})");

                });
                player.PlaySound(1083);
                return true;
            }
            if (text == "/dialog")
            {
                dialogService.Show(player.Entity, new MessageDialog("Hello", "Hello, world! " + DateTime.Now, "Left", "right"), (r) =>
                {
                    player.SendClientMessage($"Resp: {r.Response}");
                });
                player.PlaySound(1083);
                return true;
            }
            if (text == "/2dialogs")
            {
                dialogService.Show(player.Entity, new MessageDialog("Hello", "Hello, world! " + DateTime.Now, "Left", "right"), (r) =>
                {
                    player.SendClientMessage($"Resp 1: {r.Response}");
                });
                player.PlaySound(1083);

                await Task.Delay(2000);
                
                var task = dialogService.Show(player.Entity, new MessageDialog("Hello", "Hello, world! x2 " + DateTime.Now, "Left", "right"));
                player.PlaySound(1083);

                var r2 = await task;
                player.SendClientMessage($"Resp 2: {r2.Response}");

                return true;
            }
            if (text == "/entities")
            {
                void Print(int indent, Entity entity)
                {
                    var ind = string.Concat(Enumerable.Repeat(' ', indent));

                    player.SendClientMessage($"{ind}{entity}");
                    foreach (var com in entity.GetComponents<Component>())
                        player.SendClientMessage($"{ind}::>{com.GetType().Name}");

                    foreach (var child in entity.Children)
                        Print(indent + 2, child);
                }

                Print(0, entityManager.Get(WorldService.WorldId));
                return true;
            }
            if (text == "/weapon")
            {
                player.GiveWeapon(Weapon.AK47, 100);
                player.SetArmedWeapon(Weapon.AK47);
                player.PlaySound(1083);
                return true;
            }
            if (text == "/actor")
            {
                worldService.CreateActor(0, player.Position + Vector3.Up, 0);
                player.SendClientMessage("Actor created!");
                player.PlaySound(1083);
                return true;
            }
            if (text == "/pos")
            {
                player.SendClientMessage(-1, $"You are at {player.Position}");
                return true;
            }

            return false;
        }

        [Event]
        public void OnPlayerConnect(Entity player, IVehicleRepository vehiclesRepository)
        {
            Console.WriteLine("I connected! " + player.Id);

            player.AddComponent<TestComponent>();

            _zone.Show(player);
            _welcome.Show(player);

            vehiclesRepository.FooForPlayer(player);
        }

        [Event]
        public void OnPlayerConnect(Player player, IScopedFunnyService scoped, IFunnyService transient, IServiceProvider serviceProvider)
        {
            Console.WriteLine("T: " + transient.FunnyGuid);
            Console.WriteLine("S: " + scoped.FunnyGuid);
            var s2 = serviceProvider.GetService<IScopedFunnyService>().FunnyGuid;
            var t2 = serviceProvider.GetService<IFunnyService>().FunnyGuid;
            Console.WriteLine("T2: " + t2);
            Console.WriteLine("S2: " + s2);
            player.SendClientMessage($"Hey there, {player.Name}");
        }

        [Event]
        public void OnPlayerText(TestComponent test, string text, IScopedFunnyService scoped, IFunnyService transient, IServiceProvider serviceProvider)
        {
            Console.WriteLine("T: " + transient.FunnyGuid);
            Console.WriteLine("S: " + scoped.FunnyGuid);
            var s2 = serviceProvider.GetService<IScopedFunnyService>().FunnyGuid;
            var t2 = serviceProvider.GetService<IFunnyService>().FunnyGuid;
            Console.WriteLine("T2: " + t2);
            Console.WriteLine("S2: " + s2);
            Console.WriteLine(test.WelcomingMessage);
        }

        [Event]
        public void OnPlayerText(Player player, string text)
        {
            Console.WriteLine(player + ": " + text);
        }
    }
}