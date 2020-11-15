using System;
using System.Text;
using SampSharp.Core.Communication;
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

        public static Span<int> ArrayToIntSpan(Array array, int length)
        {
            if (array == null)
                return new int[length];

            if (array.Length < length)
            {
                throw new Exception("Array length does not match length specified in length argument");
            }

            Span<int> result;
            switch (array)
            {
                case int[] a:
                    return new Span<int>(a, 0, length);
                case float[] a:
                    result = new int[length];
                    for (var i = 0; i < length; i++)
                        result[i] = ValueConverter.ToInt32(a[i]);
                    return result;
                case bool[] a:
                    result = new int[length];
                    for (var i = 0; i < length; i++)
                        result[i] = ValueConverter.ToInt32(a[i]);
                    return result;
                default:
                    throw new Exception("Unsupported array type");
            }
        }

        public static T[] IntSpanToArray<T>(Array array, Span<int> span)
        {
            array ??= new T[span.Length];

            if (!(array is T[] result))
            {
                throw new Exception("Array is not of specified type");
            }
            
            if (array.Length < span.Length)
            {
                throw new Exception("Array length does not match length of native result");
            }
            
            if(typeof(T) == typeof(int))
                CopySpan(span, (int[])(object)result);
            else if(typeof(T) == typeof(float))
                CopySpan(span, (float[])(object)result);
            else if(typeof(T) == typeof(bool))
                CopySpan(span, (bool[])(object)result);
            else
                throw new Exception("Unsupported parameter type");

            return result;
        }
        
        private static void CopySpan(Span<int> span, int[] arr)
        {
            span.CopyTo(new Span<int>(arr));
        }
        private static void CopySpan(Span<int> span, float[] arr)
        {
            for (var i = 0; i < span.Length; i++)
            {
                arr[i] = ValueConverter.ToSingle(span[i]);
            }
        }
        private static void CopySpan(Span<int> span, bool[] arr)
        {
            for (var i = 0; i < span.Length; i++)
            {
                arr[i] = ValueConverter.ToBoolean(span[i]);
            }
        }
    }
}