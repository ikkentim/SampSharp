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
    public class DynamicCheckpoint : DynamicWorldObject<DynamicCheckpoint>
    {
        public DynamicCheckpoint(int id)
        {
            Id = id;
        }

        public DynamicCheckpoint(Vector position, float size = 1.0f, int worldid = -1, int interiorid = -1,
            Player player = null, float streamdistance = 100.0f)
        {
            Id = StreamerNative.CreateDynamicCP(position.X, position.Y, position.Z, size, worldid, interiorid,
                player == null ? -1 : player.Id, streamdistance);
        }

        public DynamicCheckpoint(Vector position, float size, float streamdistance, int[] worlds = null,
            int[] interiors = null,
            Player[] players = null)
        {
            Id = StreamerNative.CreateDynamicCPEx(position.X, position.Y, position.Z, size, streamdistance, worlds,
                interiors,
                players == null ? null : players.Select(p => p.Id).ToArray());
        }

        public bool IsValid
        {
            get { return StreamerNative.IsValidDynamicCP(Id); }
        }

        public override StreamType StreamType
        {
            get { return StreamType.Checkpoint; }
        }

        public float Size
        {
            get { return GetFloat(StreamerDataType.Size); }
            set { SetFloat(StreamerDataType.Size, value); }
        }

        public event EventHandler<PlayerDynamicCheckpointEventArgs> Enter;
        public event EventHandler<PlayerDynamicCheckpointEventArgs> Leave;

        public void ToggleForPlayer(Player player, bool toggle)
        {
            CheckDisposure();

            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            StreamerNative.TogglePlayerDynamicCP(player.Id, Id, toggle);
        }

        public bool IsPlayerInCheckpoint(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            return StreamerNative.IsPlayerInDynamicCP(player.Id, Id);
        }

        public static void ToggleAllForPlayer(Player player, bool toggle)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            StreamerNative.TogglePlayerAllDynamicCPs(player.Id, toggle);
        }

        public static DynamicCheckpoint GetPlayerVisibleDynamicCheckpoint(Player player)
        {
            int id = StreamerNative.GetPlayerVisibleDynamicCP(player.Id);

            return id < 0 ? null : FindOrCreate(id);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            StreamerNative.DestroyDynamicCP(Id);
        }

        public virtual void OnEnter(PlayerDynamicCheckpointEventArgs e)
        {
            if (Enter != null)
                Enter(this, e);
        }

        public virtual void OnLeave(PlayerDynamicCheckpointEventArgs e)
        {
            if (Leave != null)
                Leave(this, e);
        }
    }
}