using System;
using GameMode;
using GameMode.World;
using TestMode.World;

namespace TestMode
{
    public class GameMode : BaseMode
    {
        public GameMode()
        {
            Player.RegisterEvents(this, MyPlayer.Find);
        }

        public override bool OnTimerTick(int x, object obj)
        {
            return base.OnTimerTick(x, obj);
        }
        /*
        public override bool OnGameModeInit()
        {
            Console.WriteLine("OnGameModeInit");
            Native.SetGameModeText("TestMode()");

            var timer = new Timer(1000, true);
            timer.Tick += (sender, args) => Console.WriteLine("Tick");
            return true;
        }*/

        public override bool OnRconCommand(string command)
        {
            Console.WriteLine("RCON COMMAND: {0}", command);
            return base.OnRconCommand(command);
        }
    }
}