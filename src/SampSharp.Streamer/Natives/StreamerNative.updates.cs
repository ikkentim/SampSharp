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
using SampSharp.Streamer.Definitions;

namespace SampSharp.Streamer.Natives
{
    public static partial class StreamerNative
    {
        public static int ProcessActiveItems()
        {
            return Native.CallNative("Streamer_ProcessActiveItems", __arglist());
        }

        public static int ToggleIdleUpdate(int playerid, bool toggle)
        {
            return Native.CallNative("Streamer_ToggleIdleUpdate", __arglist(playerid, toggle));
        }

        public static int ToggleItemUpdate(int playerid, StreamType type, bool toggle)
        {
            return Native.CallNative("Streamer_ToggleItemUpdate", __arglist(playerid, (int) type, toggle));
        }

        public static int Update(int playerid)
        {
            return Native.CallNative("Steamer_Update", __arglist(playerid));
        }

        public static int UpdateEx(int playerid, float x, float y, float z, int worldid = -1, int interiorid = -1)
        {
            return Native.CallNative("Streamer_UpdateEx", __arglist(playerid, x, y, z, worldid, interiorid));
        }
    }
}