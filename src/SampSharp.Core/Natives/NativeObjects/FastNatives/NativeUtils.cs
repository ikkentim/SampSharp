using System;
using System.Text;
using SampSharp.Core.Hosting;

namespace SampSharp.Core.Natives.NativeObjects.FastNatives
{
    public class NativeUtils
    {
        public static unsafe int IntPointerToInt(int* ptr)
        {
            return (int) (IntPtr) ptr;
        }

        public static unsafe int BytePointerToInt(byte* ptr)
        {
            return (int) (IntPtr) ptr;
        }

        public static int GetByteCount(string input)
        {
            var enc = InternalStorage.RunningClient.Encoding ?? Encoding.ASCII;
            return enc.GetByteCount(input) + 1;
        }
        
        public static unsafe void GetBytes(string input, byte* ptr, int len)
        {
            var enc = InternalStorage.RunningClient.Encoding ?? Encoding.ASCII;
            enc.GetBytes(input.AsSpan(), new Span<byte>(ptr, len));
            ptr[len - 1] = 0;
        }
        public static unsafe void GetBytes2(string input, Span<byte> output)
        {
            var enc = InternalStorage.RunningClient.Encoding ?? Encoding.ASCII;
            enc.GetBytes(input.AsSpan(), output);
            output[^1] = 0;
        }

        public static string GetString(Span<byte> bytes)
        {
            var enc = InternalStorage.RunningClient.Encoding ?? Encoding.ASCII;

            return enc.GetString(bytes).TrimEnd('\0');
        }

        public static unsafe int SynchronizeInvoke(ISynchronizationProvider synchronizationProvider, IntPtr native,
            string format, int* data)
        {
            int result = default;
            synchronizationProvider.Invoke(() =>
                result = Interop.FastNativeInvoke(native, format, data));
            return result;
        }
    }
}