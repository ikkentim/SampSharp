// SampSharp
// Copyright 2020 Tim Potze
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
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;
using TestMode.Entities.Components;
using TestMode.Entities.Services;

namespace TestMode.Entities.Systems
{
    public class TestSystem : ISystem
    {
        private Menu _menu;
        private TextDraw _welcome;
        private GangZone _zone;

        [PlayerCommand]
        public async void RearCommand(Player player, IEntityManager entityManager, IWorldService worldService,
            IVehicleInfoService vehicleInfoService)
        {
            var labels = new List<EntityId>();

            foreach (var vehicle in entityManager.GetComponents<Vehicle>())
            {
                var model = vehicle.Model;

                var size = vehicleInfoService.GetModelInfo(model, VehicleModelInfoType.Size);
                var bumper = vehicleInfoService.GetModelInfo(model, VehicleModelInfoType.RearBumperZ);
                var offset = new Vector3(0, -size.Y / 2, bumper.Z);

                var rotation = vehicle.RotationQuaternion;

                var mRotation =
                    rotation.LengthSquared >
                    10000 // Unoccupied vehicle updates corrupt the internal vehicle world matrix
                        ? Matrix.CreateRotationZ(MathHelper.ToRadians(vehicle.Angle))
                        : Matrix.CreateFromQuaternion(rotation);

                var matrix = Matrix.CreateTranslation(offset) *
                             mRotation *
                             Matrix.CreateTranslation(vehicle.Position);

                var point = matrix.Translation;

                var label = worldService.CreateTextLabel("[x]", Color.Blue, point, 100, 0, false);
                labels.Add(label.Entity);
            }

            player.SendClientMessage("Points added");

            await Task.Delay(10000);

            foreach (var l in labels)
                entityManager.Destroy(l);

            player.SendClientMessage("Points removed");
        }

        [PlayerCommand]
        public void SpawnCommand(Player sender, VehicleModelType model, IWorldService worldService)
        {
            var vehicle = worldService.CreateVehicle(model, sender.Position + Vector3.Up, 0, -1, -1);
            sender.PutInVehicle(vehicle.Entity);
            sender.SendClientMessage($"{model} spawned!");
        }

        [PlayerCommand]
        public void TestCommand(Player sender, int a, int b, int c)
        {
            sender.SendClientMessage($"Hello, world! {a} {b} {c}");
        }

        [PlayerCommand]
        public void Test2Command(Player sender, int a, int b, int c, string d)
        {
            sender.SendClientMessage($"Hello, world! {a} {b} {c} {d}");
        }

        [PlayerCommand]
        public void Test3Command(Player sender, int a, int b, int c, string d = "a sensible default")
        {
            sender.SendClientMessage($"Hello, world! {a} {b} {c} {d}");
        }

        [Event]
        public void OnGameModeInit(IVehicleRepository vehiclesRepository, IWorldService worldService,
            IServerService serverService, IVehicleInfoService vehicleInfoService)
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
            Console.WriteLine(_welcome.Entity.ToString());

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

            var size = vehicleInfoService.GetModelInfo(VehicleModelType.AT400, VehicleModelInfoType.Size);
            Console.WriteLine($"AT400 size {size}");
        }

        [Event]
        public void OnPlayerWeaponShot(Player player, Weapon weapon, EntityId hit, Vector3 position)
        {
            player.SendClientMessage($"You shot {hit} at {position} with {weapon}");
        }
        
        [RconCommand]
        public bool RetCommand()
        {
            return true;
        }
        [RconCommand]
        public bool RetFalseCommand()
        {
            return false;
        }

        [RconCommand]
        public bool ErrCommand()
        {
            throw new Exception("RCON threw an error");
        }
        
        [RconCommand]
        public void ArgsCommand(int a, int b, int c)
        {
            Console.WriteLine($"{a} {b} {c}");
        }

        [Event]
        public bool OnRconCommand(string cmd)
        {
            Console.WriteLine("RCON");

            return false;
        }

        [PlayerCommand]
        public void MenuCommand(EntityId sender)
        {
            _menu.Show(sender);
        }

        [PlayerCommand]
        public void AddComponentCommand(Player player)
        {
            if (player.GetComponent<TestComponent>() == null)
                player.AddComponent<TestComponent>();

            player.SendClientMessage("Added TestComponent!");
        }

        [PlayerCommand]
        public void RemoveComponentCommand(Player player)
        {
            player.DestroyComponents<TestComponent>();

            player.SendClientMessage("Remove TestComponent!");
        }

        [PlayerCommand]
        public void GetComponentsCommand(Player player)
        {
            foreach (var comp in player.GetComponents<Component>())
                player.SendClientMessage(comp.GetType().FullName);
        }

        [PlayerCommand]
        public void TablistCommand(Player player, IDialogService dialogService)
        {
            dialogService.Show(player.Entity,
                new TablistDialog("Hello", "Left", "right", "Column1", "Column2", "Column3")
                {
                    {"r1c1", "r1c2", "r1c3"},
                    {new[] {"r2c1", "r2c2", "r2c3"}, @"Tag!!!"},
                    {"r3c1", "r3c2", $"{Color.Red}r3c3"}
                },
                r =>
                {
                    player.SendClientMessage(
                        $"Resp: {r.Response} {r.ItemIndex}: ({string.Join(" ", r.Item?.Columns)},{r.Item?.Tag})");
                });
            player.PlaySound(1083);
        }

        [PlayerCommand]
        public void ListCommand(Player player, IDialogService dialogService)
        {
            dialogService.Show(player.Entity, new ListDialog("Hello", "Left", "right")
            {
                "item1",
                "item2",
                $"{Color.Red}item3"
            }, r => { player.SendClientMessage($"Resp: {r.Response} {r.ItemIndex}: ({r.Item?.Text},{r.Item?.Tag})"); });
            player.PlaySound(1083);
        }

        [PlayerCommand]
        public void DialogCommand(Player player, IDialogService dialogService)
        {
            dialogService.Show(player.Entity,
                new MessageDialog("Hello", "Hello, world! " + DateTime.Now, "Left", "right"),
                r => { player.SendClientMessage($"Resp: {r.Response}"); });
            player.PlaySound(1083);
        }

        [PlayerCommand("2dialogs")]
        public async void TwoDialogsCommand(Player player, IDialogService dialogService)
        {
            dialogService.Show(player.Entity,
                new MessageDialog("Hello", "Hello, world! " + DateTime.Now, "Left", "right"),
                r => { player.SendClientMessage($"Resp 1: {r.Response}"); });
            player.PlaySound(1083);

            await Task.Delay(2000);

            var task = dialogService.Show(player.Entity,
                new MessageDialog("Hello", "Hello, world! x2 " + DateTime.Now, "Left", "right"));
            player.PlaySound(1083);

            var r2 = await task;
            player.SendClientMessage($"Resp 2: {r2.Response}");
        }

        [PlayerCommand]
        public void EntitiesCommand(Player player, IEntityManager entityManager)
        {
            void Print(int indent, EntityId entity)
            {
                var ind = string.Concat(Enumerable.Repeat(' ', indent));

                player.SendClientMessage($"{ind}{entity}");
                foreach (var com in entityManager.GetComponents<Component>(entity))
                    player.SendClientMessage($"{ind}::>{com.GetType().Name}");

                foreach (var child in entityManager.GetChildren(entity))
                    Print(indent + 2, child);
            }

            Print(0, WorldService.World);
        }

        [PlayerCommand]
        public void WeaponCommand(Player player)
        {
            player.GiveWeapon(Weapon.AK47, 100);
            player.SetArmedWeapon(Weapon.AK47);
            player.PlaySound(1083);
        }

        [PlayerCommand]
        public void ActorCommand(Player player, IWorldService worldService)
        {
            worldService.CreateActor(0, player.Position + Vector3.Up, 0);
            player.SendClientMessage("Actor created!");
            player.PlaySound(1083);
        }

        [PlayerCommand("pos")]
        public void PositionCommand(Player player)
        {
            player.SendClientMessage(-1, $"You are at {player.Position}");
        }

        [Event]
        public void OnPlayerConnect(Player player, IVehicleRepository vehiclesRepository)
        {
            Console.WriteLine("I connected! " + player.Entity);

            player.AddComponent<TestComponent>();

            _zone.Show(player.Entity);
            _welcome.Show(player.Entity);

            vehiclesRepository.FooForPlayer(player.Entity);
        }

        [Event]
        public void OnPlayerConnect(Player player, IScopedFunnyService scoped, IFunnyService transient,
            IServiceProvider serviceProvider)
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
        public void OnPlayerText(TestComponent test, string text, IScopedFunnyService scoped, IFunnyService transient,
            IServiceProvider serviceProvider)
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