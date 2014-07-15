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
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.World;
using SampSharp.Streamer.Definitions;

namespace SampSharp.Streamer.Natives
{
    public static partial class StreamerNative
    {
        public static int CreateDynamicCircle(float x, float y, float size, int worldid = -1, int interiorid = -1,
            int playerid = -1)
        {
            return Native.CallNative("CreateDynamicCircle", __arglist(x, y, size, worldid, interiorid, playerid));
        }

        public static int CreateDynamicRectangle(float minx, float miny, float maxx, float maxy, int worldid = -1,
            int interiorid = -1, int playerid = -1)
        {
            return Native.CallNative("CreateDynamicRectangle",
                __arglist(minx, miny, maxx, maxy, worldid, interiorid, playerid));
        }

        public static int CreateDynamicSphere(float x, float y, float z, float size, int worldid = -1,
            int interiorid = -1, int playerid = -1)
        {
            return Native.CallNative("CreateDynamicSphere", __arglist(x, y, z, size, worldid, interiorid, playerid));
        }

        public static int CreateDynamicCube(float minx, float miny, float minz, float maxx, float maxy, float maxz,
            int worldid = -1, int interiorid = -1, int playerid = -1)
        {
            return Native.CallNative("CreateDynamicCube",
                __arglist(minx, miny, minz, maxx, maxy, maxz, worldid, interiorid, playerid));
        }

        public static int CreateDynamicPolygon(float[] points, float minz = float.NegativeInfinity,
            float maxz = float.PositiveInfinity, int maxpoints = -1, int worldid = -1, int interiorid = -1,
            int playerid = -1)
        {
            //Check defaults
            if (maxpoints < 0) maxpoints = points.Length;

            return Native.CallNative("CreateDynamicPolygon", new[] {3},
                __arglist(points, minz, maxz, maxpoints, worldid, interiorid, playerid));
        }

        public static int DestroyDynamicArea(int areaid)
        {
            return Native.CallNative("DestroyDynamicArea", __arglist(areaid));
        }

        public static bool IsValidDynamicArea(int areaid)
        {
            return Native.CallNativeBool("IsValidDynamicArea", __arglist(areaid));
        }

        public static int GetDynamicPolygonPoints(int areaid, out float[] points, int maxlength)
        {
            return Native.CallNative("GetDynamicPolygonPoints", __arglist(areaid, out points, maxlength));
        }

        public static int GetDynamicPolygonNumberPoints(int areaid)
        {
            return Native.CallNative("GetDynamicPolygonNumberPoints", __arglist(areaid));
        }

        public static int TogglePlayerDynamicArea(int playerid, int areaid, bool toggle)
        {
            return Native.CallNative("TogglePlayerDynamicArea", __arglist(playerid, areaid, toggle));
        }

        public static int TogglePlayerAllDynamicAreas(int playerid, bool toggle)
        {
            return Native.CallNative("TogglePlayerAllDynamicAreas", __arglist(playerid, toggle));
        }

        public static bool IsPlayerInDynamicArea(int playerid, int areaid, bool recheck = false)
        {
            return Native.CallNativeBool("IsPlayerInDynamicArea", __arglist(playerid, areaid, recheck));
        }

        public static bool IsPlayerInAnyDynamicArea(int playerid, bool recheck = false)
        {
            return Native.CallNativeBool("IsPlayerInAnyDynamicArea", __arglist(playerid, recheck));
        }

        public static bool IsAnyPlayerInDynamicArea(int areaid, bool recheck = false)
        {
            return Native.CallNativeBool("IsAnyPlayerInDynamicArea", __arglist(areaid, recheck));
        }

        public static bool IsAnyPlayerInAnyDynamicArea(bool recheck = false)
        {
            return Native.CallNativeBool("IsAnyPlayerInAnyDynamicArea", __arglist(recheck));
        }

        public static int GetPlayerDynamicAreas(int playerid, out int[] areas, int maxlength)
        {
            return Native.CallNative("GetPlayerDynamicAreas", __arglist(playerid, out areas, maxlength));
        }

        public static int GetPlayerNumberDynamicAreas(int playerid)
        {
            return Native.CallNative("GetPlayerNumberDynamicAreas", __arglist(playerid));
        }

        public static bool IsPointInDynamicArea(int areaid, float x, float y, float z)
        {
            return Native.CallNativeBool("IsPointInDynamicArea", __arglist(areaid, x, y, z));
        }

        public static bool IsPointInAnyDynamicArea(float x, float y, float z)
        {
            return Native.CallNativeBool("IsPointInAnyDynamicArea", __arglist(x, y, z));
        }

        public static int AttachDynamicAreaToObject(int areaid, int objectid,
            StreamerObjectType type = StreamerObjectType.Dynamic, int playerid = Player.InvalidId)
        {
            return Native.CallNative("AttachDynamicAreaToObject", __arglist(areaid, objectid, (int) type, playerid));
        }

        public static int AttachDynamicAreaToPlayer(int areaid, int playerid)
        {
            return Native.CallNative("AttachDynamicAreaToPlayer", __arglist(areaid, playerid));
        }

        public static int AttachDynamicAreaToVehicle(int areaid, int vehicleid)
        {
            return Native.CallNative("AttachDynamicAreaToVehicle", __arglist(areaid, vehicleid));
        }
    }
}