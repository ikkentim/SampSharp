using System;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.World;

namespace TestMode
{
    [PooledType]
    public class Player : BasePlayer
    {
        public override void OnConnected(EventArgs e)
        {
            Spawn();
            base.OnConnected(e);
        }
    }
}
