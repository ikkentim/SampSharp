using System;
using GameMode;
using GameMode.Tools;
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
            MapAndreas.Load(MapAndreasMode.Minimal);

            Console.WriteLine("[MapAndreas] Test: " + MapAndreas.Find(new Vector()));
            Console.WriteLine("[MapAndreas] Test: " + MapAndreas.Find(new Vector(1700, -1700, 0)));
            Console.WriteLine("[MapAndreas] Test: " + MapAndreas.FindAverage(new Vector()));
            Console.WriteLine("[MapAndreas] Test: " + MapAndreas.FindAverage(new Vector(1700, -1700, 0)));

            var label = new TextLabel("Test123", Color.Blue, new Vector(0, 0, 5), 100);
            return base.OnGameModeInit();
        }
    }
}