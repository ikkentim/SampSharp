using System;
using System.Diagnostics;
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
        public static void PositionCommand(BasePlayer player)
        {
            player.SendClientMessage(Color.Yellow, $"Position: {player.Position}");
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
        
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Console.WriteLine("LOADED!");

            SpeedTest();
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
            Console.WriteLine(player.Position);
            base.OnPlayerConnected(player, e);
        }
        
        protected override void OnTick(EventArgs e)
        {
            base.OnTick(e);
        }
        
        #endregion
    }
}