using System;
using GameMode;
using GameMode.World;
using TestMode.World;

namespace TestMode
{
    public class GameMode : BaseMode
    {
        public override void RegisterEvents()
        {
            MyPlayer.RegisterEvents(this);
            Vehicle.RegisterEvents(this);
            Dialog.RegisterEvents(this);
        }

        public override bool OnGameModeInit()
        {
            Console.WriteLine("OnGameModeInit");
            return true;
        }
    }
}