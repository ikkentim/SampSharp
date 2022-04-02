using System;
using System.Text;

namespace SampSharp.Core.Hosting;

internal class NewCallbackParameterString : INewCallbackParameter
{
    public static readonly NewCallbackParameterString Instance = new();

    public unsafe object GetValue(IntPtr amx, IntPtr parameter)
    {
        void* physAddr;
        Interop.Api->PluginData->AmxExports->GetAddr(amx.ToPointer(), *(int *)parameter, &physAddr);

        if ((IntPtr)physAddr == IntPtr.Zero)
        {
            return null;
        }

        var len = 0;
        Interop.Api->PluginData->AmxExports->StrLen(physAddr, &len);
        len++;

        var buf = len < 100 ? stackalloc byte[len] : new byte[len];
            
        fixed (byte* p = &buf.GetPinnableReference())
        {
            Interop.Api->PluginData->AmxExports->GetString(p, physAddr, 0, len);
        }

        while (buf.Length > 0 && buf[^1] == 0)
        {
            buf = buf[..^1];
        }

        return Encoding.ASCII.GetString(buf);
    }
}