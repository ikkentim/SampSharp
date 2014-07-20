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
using SampSharp.Streamer.Definitions;

namespace SampSharp.Streamer.Natives
{
    public static partial class StreamerNative
    {
        public static int GetFloatData(StreamType type, int id, StreamerDataType data, out float result)
        {
            return Native.CallNative("Streamer_GetFloatData", __arglist((int) type, id, (int) data, out result));
        }

        public static int SetFloatData(StreamType type, int id, StreamerDataType data, float value)
        {
            return Native.CallNative("Streamer_SetFloatData", __arglist((int) type, id, (int) data, value));
        }

        public static int GetIntData(StreamType type, int id, StreamerDataType data)
        {
            return Native.CallNative("Streamer_GetIntData", __arglist((int) type, id, (int) data));
        }

        public static int SetIntData(StreamType type, int id, StreamerDataType data, int value)
        {
            return Native.CallNative("Streamer_SetIntData", __arglist((int) type, id, (int) data, value));
        }

        public static int GetArrayData(StreamType type, int id, StreamerDataType data, out int[] dest, int maxlength)
        {
            return Native.CallNative("Streamer_GetArrayData", __arglist((int) type, id, (int) data, out dest, maxlength));
        }

        public static int SetArrayData(StreamType type, int id, StreamerDataType data, int[] src, int maxlength = -1)
        {
            if (src == null)
            {
                throw new ArgumentNullException("src");
            }

            if (maxlength == -1)
            {
                maxlength = src.Length;
            }

            return Native.CallNative("Streamer_SetArrayData", __arglist((int) type, id, (int) data, src, maxlength));
        }

        public static bool IsInArrayData(StreamType type, int id, StreamerDataType data, int value)
        {
            return Native.CallNativeAsBool("Streamer_IsInArrayData", __arglist((int) type, id, (int) data, value));
        }

        public static int AppendArrayData(StreamType type, int id, StreamerDataType data, int value)
        {
            return Native.CallNative("Streamer_AppendArrayData", __arglist((int) type, id, (int) data, value));
        }

        public static int RemoveArrayData(StreamType type, int id, StreamerDataType data, int value)
        {
            return Native.CallNative("Streamer_RemoveArrayData", __arglist((int) type, id, (int) data, value));
        }

        public static int GetUpperBound(StreamType type)
        {
            return Native.CallNative("Streamer_GetUpperBound", __arglist((int) type));
        }
    }
}