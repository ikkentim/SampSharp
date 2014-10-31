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
using System.Linq;
using SampSharp.GameMode.World;
using SampSharp.Streamer.Definitions;
using SampSharp.Streamer.Events;
using SampSharp.Streamer.Natives;

namespace SampSharp.Streamer.World
{
    public class DynamicPickup : DynamicWorldObject<DynamicPickup>
    {
        public DynamicPickup(int id)
        {
            Id = id;
        }

        public DynamicPickup(int modelid, int type, Vector position, int worldid = -1, int interiorid = -1,
            Player player = null, float streamdistance = 100.0f)
        {
            Id = StreamerNative.CreateDynamicPickup(modelid, type, position.X, position.Y, position.Z, worldid,
                interiorid, player == null ? -1 : player.Id, streamdistance);
        }

        public DynamicPickup(int modelid, int type, Vector position, float streamdistance, int[] worlds = null,
            int[] interiors = null, Player[] players = null)
        {
            Id = StreamerNative.CreateDynamicPickupEx(modelid, type, position.X, position.Y, position.Z, streamdistance,
                worlds, interiors, players == null ? null : players.Select(p => p.Id).ToArray());
        }

        public override StreamType StreamType
        {
            get { return StreamType.Pickup; }
        }

        public virtual int Type
        {
            get { return GetInteger(StreamerDataType.Type); }
            set { SetInteger(StreamerDataType.Type, value); }
        }

        public virtual int ModelId
        {
            get { return GetInteger(StreamerDataType.ModelId); }
            set { SetInteger(StreamerDataType.ModelId, value); }
        }

        public virtual bool IsValid
        {
            get { return StreamerNative.IsValidDynamicPickup(Id); }
        }

        public event EventHandler<PlayerDynamicPickupEventArgs> PickedUp;

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            StreamerNative.DestroyDynamicPickup(Id);
        }

        public virtual void OnPickedUp(PlayerDynamicPickupEventArgs e)
        {
            if (PickedUp != null)
                PickedUp(this, e);
        }
    }
}