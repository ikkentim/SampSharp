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
using GameMode.Controllers;
using GameMode.World;
using TestMode.Controllers;

namespace TestMode
{
    public class GameMode : BaseMode
    {
        protected override void LoadControllers(ControllerCollection controllers)
        {
            controllers.Remove<PlayerController>();
            controllers.Add(new MyPlayerContoller());
        }

        public override bool OnGameModeInit()
        {
            Console.WriteLine("OnGameModeInit");

            new TextLabel("Test123", Color.Blue, new Vector(0, 0, 5), 100);

            var obj = new GlobalObject(18764, new Vector(111.44f, -77.37f, 13.18f), new Vector(0.00f, 0.00f, 0.00f));
            new GlobalObject(18764, new Vector(111.44f, -77.37f, 23.18f), new Vector(0.00f, 0.00f, 0.00f));
            var m = new Vector(0, 0, 0.01f);
            var t = new Timer(10, true);
            t.Tick += (sender, args) =>
            {
                m = -m;
                obj.Move(obj.Position.Add(m), 1.0f, obj.Rotation.Add(new Vector(5)));
            };

            var t2 = new Timer(2000, true);
            t2.Tick += (sender, args) =>
            {
                Console.WriteLine("GlobalObject:");
                foreach (var go in GlobalObject.AllIden)
                    Console.WriteLine(go);

                Console.WriteLine("Timer:");
                foreach (var p in Timer.AllIden)
                    Console.WriteLine(p);


            };

            return base.OnGameModeInit();

        }
    }
}