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
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.World;
using SampSharp.Streamer.Definitions;
using SampSharp.Streamer.Events;
using SampSharp.Streamer.Natives;

namespace SampSharp.Streamer.World
{
    public class DynamicRaceCheckpoint : DynamicWorldObject<DynamicRaceCheckpoint>
    {
        public DynamicRaceCheckpoint(int id)
        {
            Id = id;
        }

        public DynamicRaceCheckpoint(CheckpointType type, Vector position, Vector nextPosition,
            float size = 3.0f, int worldid = -1,
            int interiorid = -1, GtaPlayer player = null, float streamdistance = 100.0f)
        {
            Id = StreamerNative.CreateDynamicRaceCP(type, position.X, position.Y, position.Z, nextPosition.X,
                nextPosition.Y, nextPosition.Z, size, worldid, interiorid, player == null ? -1 : player.Id,
                streamdistance);
        }

        public DynamicRaceCheckpoint(CheckpointType type, Vector position, Vector nextPosition,
            float size, float streamdistance, int[] worlds = null, int[] interiors = null,
            GtaPlayer[] players = null)
        {
            Id = StreamerNative.CreateDynamicRaceCPEx(type, position.X, position.Y, position.Z, nextPosition.X,
                nextPosition.Y, nextPosition.Z, size, streamdistance, worlds, interiors,
                players == null ? null : players.Select(p => p.Id).ToArray());
        }

        public bool IsValid
        {
            get { return StreamerNative.IsValidDynamicRaceCP(Id); }
        }

        public override StreamType StreamType
        {
            get { return StreamType.RaceCheckpoint; }
        }

        public float Size
        {
            get { return GetFloat(StreamerDataType.Size); }
            set { SetFloat(StreamerDataType.Size, value); }
        }

        public virtual Vector NextPosition
        {
            get
            {
                float x = GetFloat(StreamerDataType.NextX);
                float y = GetFloat(StreamerDataType.NextY);
                float z = GetFloat(StreamerDataType.NextZ);

                return new Vector(x, y, z);
            }
            set
            {
                SetFloat(StreamerDataType.NextX, value.X);
                SetFloat(StreamerDataType.NextY, value.Y);
                SetFloat(StreamerDataType.NextZ, value.Z);
            }
        }

        public event EventHandler<PlayerDynamicRaceCheckpointEventArgs> Enter;
        public event EventHandler<PlayerDynamicRaceCheckpointEventArgs> Leave;

        public void ToggleForPlayer(GtaPlayer player, bool toggle)
        {
            CheckDisposure();

            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            StreamerNative.TogglePlayerDynamicRaceCP(player.Id, Id, toggle);
        }

        public bool IsPlayerInCheckpoint(GtaPlayer player)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            return StreamerNative.IsPlayerInDynamicRaceCP(player.Id, Id);
        }

        public static void ToggleAllForPlayer(GtaPlayer player, bool toggle)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            StreamerNative.TogglePlayerAllDynamicRaceCPs(player.Id, toggle);
        }

        public static DynamicRaceCheckpoint GetPlayerVisibleDynamicCheckpoint(GtaPlayer player)
        {
            int id = StreamerNative.GetPlayerVisibleDynamicRaceCP(player.Id);

            return id < 0 ? null : FindOrCreate(id);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            StreamerNative.DestroyDynamicRaceCP(Id);
        }

        public virtual void OnEnter(PlayerDynamicRaceCheckpointEventArgs e)
        {
            if (Enter != null)
                Enter(this, e);
        }

        public virtual void OnLeave(PlayerDynamicRaceCheckpointEventArgs e)
        {
            if (Leave != null)
                Leave(this, e);
        }
    }
}