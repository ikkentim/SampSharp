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
using SampSharp.Streamer;
using SampSharp.Streamer.Natives;

namespace TestMode.Tests
{
    public class StreamerTest : ITest, IControllerTest
    {
        public void LoadControllers(ControllerCollection controllers)
        {
            Streamer.LoadControllers(controllers);
        }

        public void Start(GameMode gameMode)
        {
            Console.WriteLine("StreamerTest started...");
            Console.WriteLine("Streamer tick rate: {0}", StreamerNative.GetTickRate());

            float[] points;
            int polygon = StreamerNative.CreateDynamicPolygon(new[] {1f, 2f, 3.333f, 4f}, -1, 1, 4);
            int length = StreamerNative.GetDynamicPolygonNumberPoints(polygon)*2;
            StreamerNative.GetDynamicPolygonPoints(polygon, out points, length);

            if (points == null) Console.WriteLine("NULL ARRAY");
            else foreach (var p in points) Console.WriteLine("ARRAY VALUE: {0}", p);
        }
    }
}