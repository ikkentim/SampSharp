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