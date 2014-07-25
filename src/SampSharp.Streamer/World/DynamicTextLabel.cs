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

using System.Linq;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;
using SampSharp.Streamer.Definitions;
using SampSharp.Streamer.Natives;

namespace SampSharp.Streamer.World
{
    public class DynamicTextLabel : DynamicWorldObject<DynamicTextLabel>
    {
        public DynamicTextLabel(int id)
        {
            Id = id;
        }

        public DynamicTextLabel(string text, Color color, Vector position, float drawdistance,
            Player attachedPlayer = null, Vehicle attachedVehicle = null, bool testLOS = false, int worldid = -1,
            int interiorid = -1, Player player = null, float streamdistance = 100.0f)
        {
            Id = StreamerNative.CreateDynamic3DTextLabel(text, color, position.X, position.Y, position.Z, drawdistance,
                attachedPlayer == null ? Player.InvalidId : attachedPlayer.Id,
                attachedVehicle == null ? Vehicle.InvalidId : attachedVehicle.Id, testLOS, worldid, interiorid,
                player == null ? -1 : player.Id, streamdistance);
        }

        public DynamicTextLabel(string text, Color color, Vector position,
            float drawdistance, float streamdistance, Player attachedPlayer = null, Vehicle attachedVehicle = null, bool testLOS = false,
            int[] worlds = null, int[] interiors = null, Player[] players = null)
        {
            Id = StreamerNative.CreateDynamic3DTextLabelEx(text, color, position.X, position.Y, position.Z, drawdistance,
                attachedPlayer == null ? Player.InvalidId : attachedPlayer.Id,
                attachedVehicle == null ? Vehicle.InvalidId : attachedVehicle.Id, testLOS, streamdistance, worlds,
                interiors, players == null ? null : players.Select(p => p.Id).ToArray());
        }

        public override StreamType StreamType
        {
            get { return StreamType.TextLabel; }
        }

        public string Text
        {
            get
            {
                string value;
                StreamerNative.GetDynamic3DTextLabelText(Id, out value, 1024);
                return value;
            }
            set { StreamerNative.UpdateDynamic3DTextLabelText(Id, Color, value); }
        }

        public Color Color
        {
            get { return GetInteger(StreamerDataType.Color); }
            set { StreamerNative.UpdateDynamic3DTextLabelText(Id, value, Text); }
        }

        public bool IsValid
        {
            get { return StreamerNative.IsValidDynamic3DTextLabel(Id); }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            StreamerNative.DestroyDynamic3DTextLabel(Id);
        }
    }
}