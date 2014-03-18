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
            return true;
        }
    }
}