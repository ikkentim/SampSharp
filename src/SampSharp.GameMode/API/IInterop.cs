namespace SampSharp.GameMode.API
{
    public interface IInterop
    {
        int LoadNative(string name, string format, int[] sizes);

        int InvokeNative(int handle, object[] args);

        bool NativeExists(string name);

        bool RegisterExtension(object extension);

        int SetTimer(int interval, bool repeat, object args);

        bool KillTimer(int timerid);

        void Print(string msg);

        void SetCodepage(string codepage);
    }
}