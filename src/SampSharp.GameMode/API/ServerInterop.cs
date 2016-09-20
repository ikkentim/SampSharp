namespace SampSharp.GameMode.API
{
    public sealed class ServerInterop : IInterop
    {
        public int LoadNative(string name, string format, int[] sizes)
        {
            return Interop.LoadNative(name, format, sizes);
        }

        public int InvokeNative(int handle, object[] args)
        {
            return Interop.InvokeNative(handle, args);
        }

        public bool NativeExists(string name)
        {
            return Interop.NativeExists(name);
        }

        public bool RegisterExtension(object extension)
        {
            return Interop.RegisterExtension(extension);
        }

        public int SetTimer(int interval, bool repeat, object args)
        {
            return Interop.SetTimer(interval, repeat, args);
        }

        public bool KillTimer(int timerid)
        {
            return Interop.KillTimer(timerid);
        }

        public void Print(string msg)
        {
            Interop.Print(msg);
        }

        public void SetCodepage(string codepage)
        {
            Interop.SetCodepage(codepage);
        }
    }
}