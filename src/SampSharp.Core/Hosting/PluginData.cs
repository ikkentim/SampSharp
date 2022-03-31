using System;
using System.Runtime.InteropServices;

namespace SampSharp.Core.Hosting;

[StructLayout(LayoutKind.Sequential)]
public readonly unsafe struct PluginData
{
    /// <summary>
    /// void (*logprintf)(char* format, ...)
    /// c# doesn't support varargs. Use this function with arguments ("%s", "your text here")
    /// </summary>
    public readonly delegate* unmanaged[Stdcall] <char *, char *, void> Logprintf;
            
    private readonly IntPtr Placeholder1;
    private readonly IntPtr Placeholder2;
    private readonly IntPtr Placeholder3;
    private readonly IntPtr Placeholder4;
    private readonly IntPtr Placeholder5;
    private readonly IntPtr Placeholder6;
    private readonly IntPtr Placeholder7;
    private readonly IntPtr Placeholder8;
    private readonly IntPtr Placeholder9;
    private readonly IntPtr PlaceholderA;
    private readonly IntPtr PlaceholderB;
    private readonly IntPtr PlaceholderC;
    private readonly IntPtr PlaceholderD;
    private readonly IntPtr PlaceholderE;
    private readonly IntPtr PlaceholderF;

    /// <summary>
    /// void* AmxFunctionTable[]
    /// </summary>
    public readonly AmxExport* AmxExports;
    /// <summary>
    /// int (*AmxCallPublicFilterScript)(char *szFunctionName)
    /// </summary>
    public readonly IntPtr CallPublicFs;
    /// <summary>
    /// int (*AmxCallPublicGameMode)(char *szFunctionName)
    /// </summary>
    public readonly IntPtr CallPublicGm;
}