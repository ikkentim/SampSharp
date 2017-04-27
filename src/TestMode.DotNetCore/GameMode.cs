using System;
using System.Diagnostics;
using System.Threading.Tasks;
using SampSharp.GameMode;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace TestMode.DotNetCore
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

        private void SpeedTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            var a = Server.GetTickCount();
            var b = Server.GetTickCount();
            var c = Server.GetTickCount();
            Console.WriteLine($"In {sw.Elapsed.TotalMilliseconds} ms got {a} {b} {c}");
        }
        
        #region Overrides of BaseMode
        
        protected override async void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            //Console.WriteLine("LOADED!");

            //SpeedTest();

            SetGameModeText("Before delay");
            await Task.Delay(2000);

            Console.WriteLine("waited 2");
            this.SetGameModeText("After delay");
        }
        
        protected override void OnRconCommand(RconEventArgs e)
        {
            Console.WriteLine($"Received RCON Command: {e.Command}");

            if (e.Command == "speedtest")
            {
                SpeedTest();
                e.Success = true;
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

        protected override void OnTick(EventArgs e)
        {
            base.OnTick(e);
        }
        
        #endregion
    }
}