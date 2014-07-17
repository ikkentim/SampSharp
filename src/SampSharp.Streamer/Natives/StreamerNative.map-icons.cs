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

using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.SAMP;

namespace SampSharp.Streamer.Natives
{
    public static partial class StreamerNative
    {
        public static int CreateDynamicMapIcon(float x, float y, float z, int type, Color color, int worldid = -1,
            int interiorid = -1, int playerid = -1, float streamdistance = 100.0f, MapIconType style = MapIconType.Local)
        {
            return Native.CallNative("CreateDynamicMapIcon",
                __arglist(x, y, z, type, (int) color, worldid, interiorid, playerid, streamdistance, style));
        }


        public static int DestroyDynamicMapIcon(int iconid)
        {
            return Native.CallNative("DestroyDynamicMapIcon", __arglist(iconid));
        }

        public static bool IsValidDynamicMapIcon(int iconid)
        {
            return Native.CallNativeAsBool("IsValidDynamicMapIcon", __arglist(iconid));
        }
    }
}