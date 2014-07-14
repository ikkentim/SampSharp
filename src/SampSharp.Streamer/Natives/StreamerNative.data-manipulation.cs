using System;
using SampSharp.GameMode.Natives;
using SampSharp.Streamer.Definitions;

namespace SampSharp.Streamer.Natives
{
    public static partial class StreamerNative
    {
        public static int GetFloatData(StreamType type, int id, StreamerDataType data, out float result)
        {
            return Native.CallNative("Streamer_GetFloatData", __arglist((int) type, id, (int)data, out result));
        }

        public static int SetFloatData(StreamType type, int id, StreamerDataType data, float value)
        {
            return Native.CallNative("Streamer_SetFloatData", __arglist((int)type, id, (int)data, value));
        }

        public static int GetIntData(StreamType type, int id, StreamerDataType data)
        {
            return Native.CallNative("Streamer_GetIntData", __arglist((int) type, id, (int) data));
        }

        public static int SetIntData(StreamType type, int id, StreamerDataType data, int value)
        {
            return Native.CallNative("Streamer_SetIntData", __arglist((int)type, id, (int)data, value));
        }

        public static int GetArrayData(StreamType type, int id, StreamerDataType data, out int[] dest, int maxlength)
        {
            //TODO: array types are not yet supported in CallNative
            throw new NotImplementedException();
            //return Native.CallNative("Streamer_GetArrayData", __arglist((int) type, id, (int) data, out dest, maxlength));
        }

        public static int SetArrayData(StreamType type, int id, StreamerDataType data, int[] src, int maxlength)
        {
            //TODO: array types are not yet supported in CallNative
            throw new NotImplementedException();
            //return Native.CallNative("Streamer_SetArrayData", __arglist((int) type, id, (int) data, src, maxlength));
        }

        public static bool IsInArrayData(StreamType type, int id, StreamerDataType data, int value)
        {
            return Native.CallNativeBool("Streamer_IsInArrayData", __arglist((int) type, id, (int) data, value));
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
