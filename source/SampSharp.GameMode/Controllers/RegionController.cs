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
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Controllers
{
    public class RegionController : IEventListener
    {
        private int _tick;

        public RegionController()
        {
            TickRate = 10;
        }

        public int TickRate { get; set; }

        public void RegisterEvents(BaseMode gameMode)
        {
            gameMode.Tick += Tick;
        }

        private void Tick(object sender, EventArgs e)
        {
            if (++_tick < TickRate) return;

            _tick = 0;

            foreach (Region region in Region.All)
                region.Test();
        }
    }
}