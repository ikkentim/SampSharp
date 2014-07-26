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
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.SAMP.Commands;
using SampSharp.GameMode.World;
using SampSharp.Streamer;
using SampSharp.Streamer.Definitions;
using SampSharp.Streamer.Events;
using SampSharp.Streamer.Natives;
using SampSharp.Streamer.World;

namespace TestMode.Tests
{
    public class StreamerTest : ITest, IControllerTest
    {
        public void LoadControllers(ControllerCollection controllers)
        {
            Streamer.Load(controllers);
        }

        public void Start(GameMode gameMode)
        {
            var area =
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

            var pickup = new DynamicPickup(1274, 23, new Vector(0, 0, 3)); //Dollar icon

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
            var obj = new DynamicObject(12991, new Vector(15));

            obj.Moved += (sender, args) =>
            {
                obj.Move(obj.Position + poschange, 0.5f, obj.Rotation + rotate);
                poschange = -poschange;
            };

            obj.Move(obj.Position + poschange, 0.5f, obj.Rotation + rotate);
            poschange = -poschange;
        }
    }
}