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
using SampSharp.GameMode.World;
using SampSharp.Streamer.Controllers;
using SampSharp.Streamer.Definitions;
using SampSharp.Streamer.Events;
using SampSharp.Streamer.Natives;

namespace SampSharp.Streamer
{
    public sealed class Streamer
    {
        public static int TickRate
        {
            get { return StreamerNative.GetTickRate(); }
            set { StreamerNative.SetTickRate(value); }
        }

        public static float CellDistance
        {
            get
            {
                float value;
                StreamerNative.GetCellDistance(out value);
                return value;
            }
            set { StreamerNative.SetCellDistance(value); }
        }

        public static float CellSize
        {
            get
            {
                float value;
                StreamerNative.GetCellSize(out value);
                return value;
            }
            set { StreamerNative.SetCellSize(value); }
        }

        public static OptionItemTypeCollection ItemType
        {
            get { return new OptionItemTypeCollection(); }
        }

        public static void ProcessActiveItems()
        {
            StreamerNative.ProcessActiveItems();
        }

        public event EventHandler<DynamicObjectEventArgs> DynamicObjectMoved;
        public event EventHandler<PlayerEditDynamicObjectEventArgs> PlayerEditDynamicObject;
        public event EventHandler<PlayerSelectDynamicObjectEventArgs> PlayerSelectDynamicObject;
        public event EventHandler<PlayerShootDynamicObjectEventArgs> PlayerShootDynamicObject;
        public event EventHandler<PlayerDynamicPickupEventArgs> PlayerPickUpDynamicPickup;
        public event EventHandler<PlayerDynamicCheckpointEventArgs> PlayerEnterDynamicCheckpoint;
        public event EventHandler<PlayerDynamicCheckpointEventArgs> PlayerLeaveDynamicCheckpoint;
        public event EventHandler<PlayerDynamicRaceCheckpointEventArgs> PlayerEnterDynamicRaceCheckpoint;
        public event EventHandler<PlayerDynamicRaceCheckpointEventArgs> PlayerLeaveDynamicRaceCheckpoint;
        public event EventHandler<PlayerDynamicAreaEventArgs> PlayerEnterDynamicArea;
        public event EventHandler<PlayerDynamicAreaEventArgs> PlayerLeaveDynamicArea;

        public static Streamer Load(ControllerCollection controllers)
        {
            var streamer = new Streamer();
            Native.RegisterExtension(streamer);
            var controller = new StreamerController();

            controller.RegisterStreamerEvents(streamer);
            controllers.Add(controller);

            return streamer;
        }

        public static void ToggleIdleUpdate(GtaPlayer player, bool toggle)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            StreamerNative.ToggleIdleUpdate(player.Id, toggle);
        }

        public static void Update(GtaPlayer player)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            StreamerNative.Update(player.Id);
        }

        public static void Update(GtaPlayer player, Vector position, int worldid = -1, int interiorid = -1)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            StreamerNative.UpdateEx(player.Id, position.X, position.Y, position.Z, worldid, interiorid);
        }

        #region Callbacks

        public void OnDynamicObjectMoved(int objectid)
        {
            if (DynamicObjectMoved != null)
            {
                DynamicObjectMoved(this, new DynamicObjectEventArgs(objectid));
            }
        }

        public void OnPlayerEditDynamicObject(int playerid, int objectid, int response, float x, float y, float z,
            float rx, float ry, float rz)
        {
            if (PlayerEditDynamicObject != null)
            {
                PlayerEditDynamicObject(this,
                    new PlayerEditDynamicObjectEventArgs(playerid, objectid, (EditObjectResponse) response,
                        new Vector(x, y, z), new Vector(rx, ry, rz)));
            }
        }

        public void OnPlayerSelectDynamicObject(int playerid, int objectid, int modelid, float x, float y, float z)
        {
            if (PlayerSelectDynamicObject != null)
            {
                PlayerSelectDynamicObject(this,
                    new PlayerSelectDynamicObjectEventArgs(playerid, objectid, modelid, new Vector(x, y, z)));
            }
        }

        public void OnPlayerShootDynamicObject(int playerid, int weaponid, int objectid, float x, float y, float z)
        {
            if (PlayerShootDynamicObject != null)
            {
                PlayerShootDynamicObject(this,
                    new PlayerShootDynamicObjectEventArgs(playerid, objectid, (Weapon) weaponid, new Vector(x, y, z)));
            }
        }

        public void OnPlayerPickUpDynamicPickup(int playerid, int pickupid)
        {
            if (PlayerPickUpDynamicPickup != null)
            {
                PlayerPickUpDynamicPickup(this, new PlayerDynamicPickupEventArgs(playerid, pickupid));
            }
        }

        public void OnPlayerEnterDynamicCP(int playerid, int checkpointid)
        {
            if (PlayerEnterDynamicCheckpoint != null)
            {
                PlayerEnterDynamicCheckpoint(this, new PlayerDynamicCheckpointEventArgs(playerid, checkpointid));
            }
        }

        public void OnPlayerLeaveDynamicCP(int playerid, int checkpointid)
        {
            if (PlayerLeaveDynamicCheckpoint != null)
            {
                PlayerLeaveDynamicCheckpoint(this, new PlayerDynamicCheckpointEventArgs(playerid, checkpointid));
            }
        }

        public void OnPlayerEnterDynamicRaceCP(int playerid, int checkpointid)
        {
            if (PlayerEnterDynamicRaceCheckpoint != null)
            {
                PlayerEnterDynamicRaceCheckpoint(this, new PlayerDynamicRaceCheckpointEventArgs(playerid, checkpointid));
            }
        }

        public void OnPlayerLeaveDynamicRaceCP(int playerid, int checkpointid)
        {
            if (PlayerLeaveDynamicRaceCheckpoint != null)
            {
                PlayerLeaveDynamicRaceCheckpoint(this, new PlayerDynamicRaceCheckpointEventArgs(playerid, checkpointid));
            }
        }

        public void OnPlayerEnterDynamicArea(int playerid, int areaid)
        {
            if (PlayerEnterDynamicArea != null)
            {
                PlayerEnterDynamicArea(this, new PlayerDynamicAreaEventArgs(playerid, areaid));
            }
        }

        public void OnPlayerLeaveDynamicArea(int playerid, int areaid)
        {
            if (PlayerLeaveDynamicArea != null)
            {
                PlayerLeaveDynamicArea(this, new PlayerDynamicAreaEventArgs(playerid, areaid));
            }
        }

        #endregion

        #region Subclasses

        public class OptionItemType
        {
            public int VisibleItems
            {
                get { return StreamerNative.GetVisibleItems(StreamType); }
                set { StreamerNative.SetVisibleItems(StreamType, value); }
            }

            public int MaxItems
            {
                get { return StreamerNative.GetMaxItems(StreamType); }
                set { StreamerNative.SetMaxItems(StreamType, value); }
            }

            public StreamType StreamType { get; set; }

            public int GetInteger(int id, StreamerDataType data)
            {
                return StreamerNative.GetIntData(StreamType, id, data);
            }

            public float GetFloat(int id, StreamerDataType data)
            {
                float value;
                StreamerNative.GetFloatData(StreamType, id, data, out value);

                return value;
            }

            public int[] GetArray(int id, StreamerDataType data, int maxlength)
            {
                int[] value;
                StreamerNative.GetArrayData(StreamType, id, data, out value, maxlength);

                return value;
            }

            public void AppendToArray(int id, StreamerDataType data, int value)
            {
                StreamerNative.AppendArrayData(StreamType, id, data, value);
            }

            public void RemoveArrayData(int id, StreamerDataType data, int value)
            {
                StreamerNative.RemoveArrayData(StreamType, id, data, value);
            }

            public bool IsInArray(int id, StreamerDataType data, int value)
            {
                return StreamerNative.IsInArrayData(StreamType, id, data, value);
            }

            public void SetInteger(int id, StreamerDataType data, int value)
            {
                StreamerNative.SetIntData(StreamType, id, data, value);
            }

            public void SetFloat(int id, StreamerDataType data, float value)
            {
                StreamerNative.SetFloatData(StreamType, id, data, value);
            }

            public void SetArray(int id, StreamerDataType data, int[] value)
            {
                StreamerNative.SetArrayData(StreamType, id, data, value);
            }

            public void ToggleUpdate(GtaPlayer player, bool toggle)
            {
                if (player == null)
                {
                    throw new ArgumentNullException("player");
                }

                StreamerNative.ToggleItemUpdate(player.Id, StreamType, toggle);
            }
        }

        public class OptionItemTypeCollection
        {
            public OptionItemType this[StreamType t]
            {
                get { return new OptionItemType {StreamType = t}; }
            }
        }

        #endregion
    }
}