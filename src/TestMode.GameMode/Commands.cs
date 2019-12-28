using System;
using System.Linq;
using System.Threading.Tasks;
using SampSharp.GameMode;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Display;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace TestMode
{
    internal class Commands
    {
        [Command("kickme")]
        public static async void KickMeCommand(BasePlayer player)
        {
            player.SendClientMessage("Bye!");
            await Task.Delay(10);
            player.Kick();
        }


        [Command("spawn")]
        public static void SpawnCommand(BasePlayer player, VehicleModelType type)
        {
            var vehicle = BaseVehicle.Create(type, player.Position + Vector3.Up, player.Angle, -1, -1);
            player.PutInVehicle(vehicle);
            vehicle.GetDamageStatus(out var panels, out var doors, out var lights, out var tires);
            Console.WriteLine(panels.ToString());
            Console.WriteLine(doors.ToString());
            Console.WriteLine(lights.ToString());
            Console.WriteLine(tires.ToString());
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