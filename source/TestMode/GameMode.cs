using System;
using GameMode;
using GameMode.World;
using TestMode.World;

namespace TestMode
{
    public class GameMode : Server
    {
        public GameMode()
        {
            Player.RegisterEvents(this, MyPlayer.Find);
        }

        public override bool OnGameModeInit()
        {
            Console.WriteLine("Player test: {0}", MyPlayer.Find(1));
            return true;
        }
    }
}