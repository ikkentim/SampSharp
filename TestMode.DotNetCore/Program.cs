using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using SampSharp.Core;
using SampSharp.Core.Logging;
using SampSharp.Core.Natives.NativeObjects;
using SampSharp.GameMode;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;
using Timer = SampSharp.GameMode.SAMP.Timer;

namespace TestMode.DotNetCore
{
    class GameMode : BaseMode
    {
        #region Overrides of BaseMode

        /// <summary>
        ///     Raises the <see cref="BaseMode.Initialized" /> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs" /> that contains the event data. </param>
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Console.WriteLine("LOADED!");

            Thread x = Thread.CurrentThread;

            var sw = new Stopwatch();
            sw.Start();
            Server.GetTickCount();
            Server.GetTickCount();
            Server.GetTickCount();
            Console.WriteLine(sw.ElapsedMilliseconds);
            Timer.Run(TimeSpan.FromSeconds(0.1), () => Console.WriteLine("beep! " + (Thread.CurrentThread == x)));
        }

        #endregion

        [Command("myfirstcommand")]
        public static void MyFirstCommand(BasePlayer player, string message)
        {
            player.SendClientMessage($"Hello, world! You said {message}");
        }

        #region Overrides of BaseMode

        /// <summary>
        ///     Raises the <see cref="BaseMode.RconCommand" /> event.
        /// </summary>
        /// <param name="e">An <see cref="RconEventArgs" /> that contains the event data. </param>
        protected override void OnRconCommand(RconEventArgs e)
        {
            Console.WriteLine($"Received RCON Command: {e.Command}");
            base.OnRconCommand(e);
        }

        #endregion
    }
    class Program
    {
        static void Main(string[] args)
        {
            new GameModeBuilder()
                .UseLogLevel(CoreLogLevel.Debug)
                .Use<GameMode>()
                .Run();
        }
    }
}