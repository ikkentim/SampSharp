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
using System.ComponentModel;
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.World;
using SampSharp.Streamer.Definitions;
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

        public static void ToggleIdleUpdate(Player player, bool toggle)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            StreamerNative.ToggleIdleUpdate(player.Id, toggle);
        }

        public static void Update(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            StreamerNative.Update(player.Id);
        }

        public static void Update(Player player, Vector position, int worldid = -1, int interiorid = -1)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            StreamerNative.UpdateEx(player.Id, position.X, position.Y, position.Z, worldid, interiorid);
        }

        public static void LoadControllers(ControllerCollection controllers)
        {
            Native.RegisterExtension(new Streamer());

            controllers.Add(new StreamerController());
        }

        public void OnDynamicObjectMoved(int objectid)
        {
        }

        public void OnPlayerEditDynamicObject(int playerid, int objectid, int response, float x, float y, float z,
            float rx, float ry, float rz)
        {
        }

        public void OnPlayerSelectDynamicObject(int playerid, int objectid, int modelid, float x, float y, float z)
        {
        }

        public void OnPlayerShootDynamicObject(int playerid, int weaponid, int objectid, float x, float y, float z)
        {
        }

        public void OnPlayerPickUpDynamicPickup(int playerid, int pickupid)
        {
        }

        public void OnPlayerEnterDynamicCP(int playerid, int checkpointid)
        {
        }

        public void OnPlayerLeaveDynamicCP(int playerid, int checkpointid)
        {
        }

        public void OnPlayerEnterDynamicRaceCP(int playerid, int checkpointid)
        {
        }

        public void OnPlayerLeaveDynamicRaceCP(int playerid, int checkpointid)
        {
        }

        public void OnPlayerEnterDynamicArea(int playerid, int areaid)
        {
        }

        public void OnPlayerLeaveDynamicArea(int playerid, int areaid)
        {
        }

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

            public void ToggleUpdate(Player player, bool toggle)
            {
                if (player == null)
                {
                    throw new ArgumentNullException("player");
                }

                StreamerNative.ToggleItemUpdate(player.Id, StreamType, toggle);
            }
            public StreamType StreamType { get; set; }
        }

        public class OptionItemTypeCollection
        {
            public OptionItemType this[StreamType t]
            {
                get { return new OptionItemType { StreamType = t }; }
            }
        }
    }
}