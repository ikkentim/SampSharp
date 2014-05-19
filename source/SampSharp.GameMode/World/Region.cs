using System;
using System.Collections.Generic;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.World.Shapes;

namespace SampSharp.GameMode.World
{
    public class Region : Pool<Region>
    {
        private readonly List<Player> _playersInRegion = new List<Player>(); 
        public Region(IShape shape)
        {
            Shape = shape;
        }

        public IShape Shape { get; set; }

        public EventHandler<PlayerEventArgs> Enter;

        public EventHandler<PlayerEventArgs> Leave;

        public void Test()
        {
            foreach (var player in Player.All)
            {
                if (_playersInRegion.Contains(player))
                {
                    if (Shape.Contains(player.Position)) continue;

                    _playersInRegion.Remove(player);
                    OnLeave(new PlayerEventArgs(player.Id));
                }
                else
                {
                    if (!Shape.Contains(player.Position)) continue;

                    _playersInRegion.Add(player);
                    OnEnter(new PlayerEventArgs(player.Id));
                }
            }
        }

        public virtual void OnEnter(PlayerEventArgs e)
        {
            if (Enter != null)
                Enter(this, e);
        }

        public virtual void OnLeave(PlayerEventArgs e)
        {
            if (Leave != null)
                Leave(this, e);
        }


    }
}
