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
        public static int GetTickRate()
        {
            return Native.CallNative("Streamer_GetTickRate");
        }

        public static int SetTickRate(int rate)
        {
            return Native.CallNative("Streamer_SetTickRate", __arglist(rate));
        }

        public static int GetMaxItems(StreamType type)
        {
            return Native.CallNative("Streamer_GetMaxItems", __arglist((int) type));
        }

        public static int SetMaxItems(StreamType type, int items)
        {
            return Native.CallNative("Streamer_SetMaxItems", __arglist((int) type, items));
        }

        public static int GetVisibleItems(StreamType type)
        {
            return Native.CallNative("Streamer_GetVisibleItems", __arglist((int) type));
        }

        public static int SetVisibleItems(StreamType type, int items)
        {
            return Native.CallNative("Streamer_SetVisibleItems", __arglist((int) type, items));
        }

        public static int GetCellDistance(out float distance)
        {
            return Native.CallNative("Streamer_GetCellDistance", __arglist(out distance));
        }

        public static int SetCellDistance(float distance)
        {
            return Native.CallNative("Streamer_SetCellDistance", __arglist(distance));
        }

        public static int GetCellSize(out float size)
        {
            return Native.CallNative("Streamer_GetCellSize", __arglist(out size));
        }

        public static int SetCellSize(float size)
        {
            return Native.CallNative("Streamer_SetCellSize", __arglist(size));
        }
    }
}