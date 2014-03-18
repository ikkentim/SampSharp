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
            //Console.WriteLine("Player test: {0}", MyPlayer.Find(1));
            Console.WriteLine(new Color(255, 255, 255, 255));
            return true;
        }
    }
}