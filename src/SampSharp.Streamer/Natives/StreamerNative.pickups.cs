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
        public static int CreateDynamicPickup(int modelid, int type, float x, float y, float z, int worldid = -1,
            int interiorid = -1, int playerid = -1, float streamdistance = 100.0f)
        {
            return Native.CallNative("CreateDynamicPickup",
                __arglist(modelid, type, x, y, z, worldid, interiorid, playerid, streamdistance));
        }

        public static int DestroyDynamicPickup(int pickupid)
        {
            return Native.CallNative("DestroyDynamicPickup", __arglist(pickupid));
        }

        public static bool IsValidDynamicPickup(int pickupid)
        {
            return Native.CallNativeAsBool("IsValidDynamicPickup", __arglist(pickupid));
        }
    }
}