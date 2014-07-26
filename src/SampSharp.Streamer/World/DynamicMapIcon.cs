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

using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;
using SampSharp.Streamer.Definitions;
using SampSharp.Streamer.Natives;

namespace SampSharp.Streamer.World
{
    public class DynamicMapIcon : DynamicWorldObject<DynamicMapIcon>
    {
        public DynamicMapIcon(int id)
        {
            Id = id;
        }

        public DynamicMapIcon(Vector position, int type, MapIconType mapIconType = MapIconType.Local, int worldid = -1,
            int interiorid = -1,
            Player player = null, float streamDistance = 100.0f)
        {
            Id = StreamerNative.CreateDynamicMapIcon(position.X, position.Y, position.Z, type, 0, worldid, interiorid,
                player == null ? -1 : player.Id, streamDistance, mapIconType);
        }

        public DynamicMapIcon(Vector position, Color color, MapIconType mapIconType = MapIconType.Local,
            int worldid = -1, int interiorid = -1,
            Player player = null, float streamDistance = 100.0f)
        {
            Id = StreamerNative.CreateDynamicMapIcon(position.X, position.Y, position.Z, 0, color, worldid, interiorid,
                player == null ? -1 : player.Id, streamDistance, mapIconType);
        }

        public int Type
        {
            get { return GetInteger(StreamerDataType.Type); }
            set { SetInteger(StreamerDataType.Type, value); }
        }

        public Color Color
        {
            get { return GetInteger(StreamerDataType.Color); }
            set { SetInteger(StreamerDataType.Color, value); }
        }

        public bool IsValid
        {
            get { return StreamerNative.IsValidDynamicMapIcon(Id); }
        }

        public override StreamType StreamType
        {
            get { return StreamType.MapIcon; }
        }

        protected override void Dispose(bool disposing)
        {
            StreamerNative.DestroyDynamicMapIcon(Id);

            base.Dispose(disposing);
        }
    }
}