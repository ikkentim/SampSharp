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
            Vehicle.RegisterEvents(this, Vehicle.Find);
        }

        public override bool OnGameModeInit()
        {
            Console.WriteLine("OnGameModeInit");
            return true;
        }
    }
}