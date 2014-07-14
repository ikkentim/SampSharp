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

using SampSharp.GameMode.Natives;

namespace SampSharp.Streamer.Natives
{
    public static partial class StreamerNative
    {
        public static int CreateDynamicRaceCP(int type, float x, float y, float z, float nextx, float nexty, float nextz,
            float size, int worldid = -1, int interiorid = -1, int playerid = -1, float streamdistance = 100.0f)
        {
            return Native.CallNative("CreateDynamicRaceCP",
                __arglist(type, x, y, z, nextx, nexty, nextz, size, worldid, interiorid, playerid, streamdistance));
        }

        public static int DestroyDynamicRaceCP(int checkpointid)
        {
            return Native.CallNative("DestroyDynamicRaceCP", __arglist(checkpointid));
        }

        public static bool IsValidDynamicRaceCP(int checkpointid)
        {
            return Native.CallNativeBool("IsValidDynamicRaceCP", __arglist(checkpointid));
        }

        public static int TogglePlayerDynamicRaceCP(int playerid, int checkpointid, bool toggle)
        {
            return Native.CallNative("TogglePlayerDynamicRaceCP", __arglist(playerid, checkpointid, toggle));
        }

        public static int TogglePlayerAllDynamicRaceCPs(int playerid, bool toggle)
        {
            return Native.CallNative("TogglePlayerAllDynamicRaceCPs", __arglist(playerid, toggle));
        }

        public static bool IsPlayerInDynamicRaceCP(int playerid, int checkpointid)
        {
            return Native.CallNativeBool("IsPlayerInDynamicRaceCP", __arglist(playerid, checkpointid));
        }

        public static int GetPlayerVisibleDynamicRaceCP(int playerid)
        {
            return Native.CallNative("GetPlayerVisibleDynamicRaceCP", __arglist(playerid));
        }
    }
}