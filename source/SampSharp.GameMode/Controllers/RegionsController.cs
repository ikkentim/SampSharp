using System;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Controllers
{
    public class RegionsController : IEventListener
    {
        private int _tick;

        public RegionsController()
        {
            TickRate = 10;
        }

        public virtual int TickRate { get; set; }

        public void RegisterEvents(BaseMode gameMode)
        {
            gameMode.Tick += Tick;
        }

        private void Tick(object sender, EventArgs e)
        {
            if (++_tick < TickRate) return;

            _tick = 0;

            foreach (var region in Region.All)
                region.Test();
        }
    }
}
