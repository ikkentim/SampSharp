using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.Helpers;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace TestMode
{
    internal class Commands
    {
        [Command]
        public static void DefaultNameCommand(BasePlayer player)
        {
            player.SendClientMessage("default name command");
        }

        [Command]
        public static void DefaultGroupCommand(BasePlayer player)
        {
            player.SendClientMessage("Group contains: /defaultgroup defaultname");
        }

        [CommandGroup]
        internal class DefaultGroupCommandGroup
        {
            [Command]
            public static void DefaultNameCommand(BasePlayer player)
            {
                player.SendClientMessage("default group default name command");
            }
        }

        [Command("kickme")]
        public static async void KickMeCommand(BasePlayer player)
        {
            player.SendClientMessage("Bye!");
            await Task.Delay(10);
            player.Kick();
        }

        private static TextDraw _td1;
        private static PlayerTextDraw _td2;

        [Command("tst")]
        public static async void TdSelTest(BasePlayer player)
        {
            _td1?.Dispose();
            _td2?.Dispose();

            _td1 = new TextDraw(new Vector2(220, 120), "E")
            {
                Selectable = true,
                UseBox = true,
                BackColor = Color.Green,
                BoxColor = Color.Blue,
                Alignment = TextDrawAlignment.Right,
                IsApplyFixes = true,
                Proportional = true,
                Shadow = 3,
                Font = TextDrawFont.Pricedown,
                Outline = 2,
                ForeColor = Color.White,
                Width = 400f,
                Height = 400f
            };
            _td2 = new PlayerTextDraw(player, new Vector2(220, 180), "Q")
            {
                Selectable = true,
                UseBox = true,
                BackColor = Color.Green,
                BoxColor = Color.Blue,
                Alignment = TextDrawAlignment.Right,
                IsApplyFixes = true,
                Proportional = true,
                Shadow = 3,
                Font = TextDrawFont.Pricedown,
                Outline = 2,
                ForeColor = Color.White,
                Width = 400f,
                Height = 400f
            };

            _td1.Show(player);
            _td2.Show();

            await Task.Delay(5);
            player.SelectTextDraw(Color.Red);
            
            player.ClickTextDraw += (sender, args) =>
            {
                player.SendClientMessage($"Selected TD: {_td1 == args.TextDraw}");
            };
            player.ClickPlayerTextDraw += (sender, args) =>
            {
                player.SendClientMessage($"Selected PTD: {_td2 == args.PlayerTextDraw}");
            };
            player.CancelClickTextDraw += (sender, args) =>
            {
                player.SendClientMessage("Canceled TD selection");
            };
        }

        [Command("del")]
        public static void Del(BasePlayer p)
        {
            _td1?.Dispose();
            _td2?.Dispose();
        }
        
        [Command("quattest")]
        public static void QuatTestCommand(BasePlayer player)
        {
            var v = player.Vehicle;
            if (v == null) return;

            var q1 = v.GetRotationQuat();
            var m = Matrix.CreateFromQuaternion(q1);
            var q2 = m.Rotation;

            player.SendClientMessage("1: " + q1.ToVector4());
            player.SendClientMessage("2: " + q2.ToVector4());
        }

        [Command("rear")]
        public static async void RearCommand(BasePlayer player)
        {
            var labels = new List<TextLabel>();

            foreach (var vehicle in BaseVehicle.All)
            {
                var model = vehicle.Model;

                var size = BaseVehicle.GetModelInfo(model, VehicleModelInfoType.Size);
                var bumper = BaseVehicle.GetModelInfo(model, VehicleModelInfoType.RearBumperZ);
                var offset = new Vector3(0, -size.Y / 2, bumper.Z);

                var rotation = vehicle.GetRotationQuat();

                //rotation = new Quaternion(-rotation.X, -rotation.Y, -rotation.Z, rotation.W);

                var mRotation = rotation.LengthSquared > 10000 // Unoccupied vehicle updates corrupt the internal vehicle world matrix
                    ? Matrix.CreateRotationZ(MathHelper.ToRadians(vehicle.Angle))
                    : Matrix.CreateFromQuaternion(rotation);

                var matrix = Matrix.CreateTranslation(offset) *
                             mRotation *
                             Matrix.CreateTranslation(vehicle.Position);

                var point = matrix.Translation;

                labels.Add(new TextLabel("[x]", Color.Blue, point, 100, 0, false));
            }
            
            await Task.Delay(10000);

            foreach(var l in labels)
                l.Dispose();
            
        }
        
        [Command("rear2")]
        public static async void Rear2Command(BasePlayer player)
        {
            var labels = new List<TextLabel>();

            foreach (var vehicle in BaseVehicle.All)
            {
                var model = vehicle.Model;

                var size = BaseVehicle.GetModelInfo(model, VehicleModelInfoType.Size);
                var bumper = BaseVehicle.GetModelInfo(model, VehicleModelInfoType.RearBumperZ);
                var offset = new Vector3(0, -size.Y / 2, bumper.Z);

                var rotation = vehicle.GetRotationQuat();

                //rotation = new Quaternion(-rotation.X, -rotation.Y, -rotation.Z, rotation.W);

                var point = vehicle.Position + Vector3.Transform(offset, rotation);

                labels.Add(new TextLabel("[x]", Color.Red, point, 100, 0, false));
            }
            
            await Task.Delay(10000);

            foreach(var l in labels)
                l.Dispose();
            
        }

        [Command("spawnat")]
        public static void SpawnCommand(BasePlayer player, VehicleModelType type, float x, float y, float z, float a)
        {
            BaseVehicle.Create(type, new Vector3(x, y, z), a, -1, -1);
        }

        [Command("spawn")]
        public static void SpawnCommand(BasePlayer player, VehicleModelType? type)
        {
            if (type == null)
            {
                player.SendClientMessage("That's not a valid vehicle model");
                return;
            }

            var vehicle = BaseVehicle.Create(type.Value, player.Position + Vector3.Up, player.Angle, -1, -1);
            player.PutInVehicle(vehicle);
        }
        
        [Command("spawn2")]
        public static void SpawnCommand(BasePlayer player, VehicleModelType type)
        {
            var vehicle = BaseVehicle.Create(type, player.Position + Vector3.Up, player.Angle, -1, -1);
            player.PutInVehicle(vehicle);
        }

        [Command("status")]
        public static void StatusCommand(BasePlayer player, int vehicleid)
        {
            var vehicle = BaseVehicle.Find(vehicleid);
            int panels = 99;
            int doors = 100;
            int lights = 101;
            int tires = 102;
            vehicle.GetDamageStatus(out panels, out doors, out lights, out tires);
            Console.WriteLine(panels.ToString());
            Console.WriteLine(doors.ToString());
            Console.WriteLine(lights.ToString());
            Console.WriteLine(tires.ToString());
        }

        [Command("setstatus")]
        public static void SetStatusCommand(BasePlayer player, int vehicleid)
        {
            var vehicle = BaseVehicle.Find(vehicleid);
            vehicle.SetDoorsParameters(true, true, true, true);

            vehicle.GetDamageStatus(out var panels, out var doors, out var lights, out var tires);
            Console.WriteLine(panels.ToString());
            Console.WriteLine(doors.ToString());
            Console.WriteLine(lights.ToString());
            Console.WriteLine(tires.ToString());
        }

        [Command("give")]
        public static void GiveCommand(BasePlayer player, Weapon weapon, int ammo)
        {
            player.GiveWeapon(weapon, ammo);
        }

        [Command("enter")]
        public static void EnterCommand(BasePlayer player, BaseVehicle vehicle)
        {
            player.PutInVehicle(vehicle);
        }

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

        [Command("dialogtest")]
        public static async void DialogTest(BasePlayer player)
        {
            var dialog = new MessageDialog("Test dialog", "This message should hide in 2 seconds.", "Don't click me!");
            dialog.Response += (sender, args) =>
            {
                player.SendClientMessage("You responed to the dialog with button" + args.DialogButton);
            };

            player.SendClientMessage("Showing dialog");
            dialog.Show(player);

            await Task.Delay(2000);

            player.SendClientMessage("Hiding dialog");
            Dialog.Hide(player);
        }

        [Command("asyncdialog")]
        public static async void DialogAsyncTest(BasePlayer player)
        {
            var dialog = new MessageDialog("Async dialog test", "Quit with this dialog still open.", "Don't click me!");

            Console.WriteLine("Showing dialog");
            try
            {

                await dialog.ShowAsync(player);
                Console.WriteLine("Dialog ended");
            }
            catch (PlayerDisconnectedException e)
            {
                Console.WriteLine($"{player} left.");
                Console.WriteLine(e);
            }
        }
        [Command("weapon")]
        public static void WeaponCommand(BasePlayer player, Weapon weapon, int ammo = 30)
        {
            player.GiveWeapon(weapon, ammo);
        }

        [Command("kick")]
        public static void Kick(BasePlayer player, BasePlayer target)
        {
            target.Kick();
        }

        [Command("help")]
        private static void Help(BasePlayer player)
        {
            player.SendClientMessage("/reverse, /help");
        }

        [Command("reverse")]
        private static void Reverse(BasePlayer player, string message)
        {
            player.SendClientMessage($"{message} reversed: ");
            message = new string(message.Reverse().ToArray());
            player.SendClientMessage(message);
        }

    }
}