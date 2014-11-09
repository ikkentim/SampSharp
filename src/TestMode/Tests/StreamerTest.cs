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
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;
using SampSharp.Streamer;
using SampSharp.Streamer.World;

namespace TestMode.Tests
{
    public class StreamerTest : ITest, IControllerTest
    {
        private static DynamicObject _obj;

        public void LoadControllers(ControllerCollection controllers)
        {
            Console.WriteLine("Loading streamer");
            Streamer.Load(controllers);
        }

        public void Start(GameMode gameMode)
        {
            DynamicArea area =
                DynamicArea.CreatePolygon(new[]
                {
                    new Vector(-1, -1, 0),
                    new Vector(1, -1, 0),
                    new Vector(1, 1, 0),
                    new Vector(-1, 1, 0)
                });
            Console.WriteLine("area.IsValid = {0}", area.IsValid);

            var icon = new DynamicMapIcon(new Vector(1500, -1500, 0), Color.Firebrick, MapIconType.Global, -1, -1, null,
                300);

            Console.WriteLine(icon.Position);
            icon.Position = new Vector(50, 50, 5);
            Console.WriteLine(icon.Position);
            

            var pickup = new DynamicPickup(1274, 23, new Vector(0, 0, 3), 100f, new []{11,22,33,44, 0,55,66,77,88,99}); //Dollar icon
            pickup.PickedUp += (sender, args) => args.Player.SendClientMessage(Color.White, "Picked Up");

            var checkpoint = new DynamicCheckpoint(new Vector(10, 10, 3));
            checkpoint.Enter += (sender, args) => args.Player.SendClientMessage(Color.White, "Entered CP");
            checkpoint.Leave += (sender, args) => args.Player.SendClientMessage(Color.White, "Left CP");


            var racecheckpoint = new DynamicRaceCheckpoint(CheckpointType.Normal, new Vector(-10, -10, 3), new Vector());
            racecheckpoint.Enter += (sender, args) => args.Player.SendClientMessage(Color.White, "Entered RCP");
            racecheckpoint.Leave += (sender, args) => args.Player.SendClientMessage(Color.White, "Left RCP");

            new DynamicTextLabel("I am maroon", Color.Maroon, new Vector(0, 0, 5), 100.0f);
 
            var rotate = new Vector(20);
            var poschange = new Vector(0, 0, 1f);
            _obj = new DynamicObject(12991, new Vector(15));
      
            _obj.SetMaterialText(1, "Test", ObjectMaterialSize.X512X512, "Arial", 24, false, Color.Red, Color.White);
            string text, font;
            int fontsize;
            bool bold;
            ObjectMaterialSize size;
            Color c1, c2;
            ObjectMaterialTextAlign ta;
         
            _obj.GetMaterialText(1, out text, out size, out font, out fontsize, out bold, out c1, out c2, out ta);
            Console.WriteLine("GetMaterialText: {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}", text, size, font, fontsize,
                bold, c1, c2,
                ta);

            _obj.Moved += (sender, args) =>
            {
                _obj.Move(_obj.Position + poschange, 0.5f, _obj.Rotation + rotate);
                poschange = -poschange;
            };

            _obj.Move(_obj.Position + poschange, 0.5f, _obj.Rotation + rotate);

            poschange = -poschange;

            var pu = new DynamicPickup(1274, 23, new Vector(111), 3);

            Console.WriteLine("World: {0}", string.Join(",", pu.Worlds));
        } 

        [Command("attachcam")]
        public static void AttachCamCommand(GtaPlayer player)
        {
            _obj.AttachCameraToObject(player);
        }
    }
}