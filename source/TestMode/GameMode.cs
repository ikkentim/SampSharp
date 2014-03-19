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

        public override bool OnGameModeInit()
        {
            Console.WriteLine("OnGameModeInit");
            var timer = new Timer(2500, true);
            timer.Tick += (sender, args) => Console.WriteLine("Tick");
            return true;
        }
    }
}