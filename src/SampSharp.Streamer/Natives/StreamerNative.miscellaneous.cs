using System.Runtime.CompilerServices;
using SampSharp.GameMode.Natives;
using SampSharp.Streamer.Definitions;

namespace SampSharp.Streamer.Natives
{
    public static partial class StreamerNative
    {
        public static int GetDistanceToItem(float x, float y, float z, StreamType type, int id, out float distance,
            int dimensions = 3)
        {
            return Native.CallNative("Streamer_GetDistanceToItem",
                __arglist(x, y, z, (int) type, id, out distance, dimensions));
        }

        public static int GetItemInternalID(int playerid, StreamType type, int streamerid)
        {
            return Native.CallNative("Streamer_GetItemInternalID", __arglist(playerid, (int) type, streamerid));
        }

        public static int GetItemStreamerID(int playerid, StreamType type, int internalid)
        {
            return Native.CallNative("Streamer_GetItemStreamerID", __arglist(playerid, (int) type, internalid));
        }

        public static bool IsItemVisible(int playerid, StreamType type, int id)
        {
            return Native.CallNativeBool("Streamer_IsItemVisible", __arglist(playerid, (int)type, id));
        }

        public static int DestroyAllVisibleItems(int playerid, StreamType type, bool serverwide = true)
        {
            return Native.CallNative("Streamer_DestroyAllVisibleItems", __arglist(playerid, (int) type, serverwide));
        }

        public static int CountVisibleItems(int playerid, StreamType type, bool serverwide = true)
        {
            return Native.CallNative("Streamer_CountVisibleItems", __arglist(playerid, (int) type, serverwide));

        }

        public static int DestroyAllItems(StreamType type, bool serverwide = true)
        {
            return Native.CallNative("Streamer_DestroyAllItems", __arglist((int) type, serverwide));
        }

        public static int CountItems(StreamType type, bool serverwide = true)
        {
            return Native.CallNative("Streamer_CountItems", __arglist((int) type, serverwide));
        }
    }
}
