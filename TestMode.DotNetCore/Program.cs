using System;
using SampSharp.Core;
using SampSharp.Core.Natives.NativeObjects;
using SampSharp.GameMode;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;

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
        }

        #endregion

        [Command("myfirstcommand")]
        public static void MyFirstCommand(BasePlayer player, string message)
        {
            player.SendClientMessage($"Hello, world! You said {message}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(nameof(NativeHandleInvokers.InvokeHandle));
            new GameModeBuilder()
                .Use<GameMode>()
                .Run();
        }
    }
}