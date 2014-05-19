// SampSharp
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

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

        public EventHandler<PlayerEventArgs> Enter;

        public EventHandler<PlayerEventArgs> Leave;

        public Region(IShape shape)
        {
            Shape = shape;
        }

        public IShape Shape { get; set; }

        public void Test()
        {
            foreach (Player player in Player.All)
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