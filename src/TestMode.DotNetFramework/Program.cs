using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SampSharp.Core;
using SampSharp.Core.Communication;
using SampSharp.Core.Communication.Clients;
using SampSharp.Core.Logging;
using SampSharp.GameMode;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

namespace TestMode.DotNetFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            new GameModeBuilder()
                .UseLogLevel(CoreLogLevel.Debug)
                .UseExitBehaviour(GameModeExitBehaviour.Restart)
                .UseStartBehaviour(GameModeStartBehaviour.FakeGmx)
                .Use<GameMode>()
                .UseTcpClient("127.0.0.1", 8383)
                .Run();
        }
    }

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
