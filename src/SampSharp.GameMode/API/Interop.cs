using System.Runtime.CompilerServices;

namespace SampSharp.GameMode.API
{
    internal static class Interop
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int LoadNative(string name, string format, int[] sizes);

        [MethodImpl((MethodImplOptions.InternalCall))]
        public static extern int InvokeNative(int handle, object[] args);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool NativeExists(string name);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool RegisterExtension(object extension);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern int SetTimer(int interval, bool repeat, object args);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern bool KillTimer(int timerid);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void Print(string msg);

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern void SetCodepage(string codepage);
    }
}