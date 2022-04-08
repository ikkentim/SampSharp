using System;
using System.Runtime.InteropServices;

namespace SampSharp.Core.Hosting;

[StructLayout(LayoutKind.Sequential)]
public readonly unsafe struct PluginData
{
    /// <summary>
    /// void (*logprintf)(char* format, ...)
    /// C# doesn't support interop with varargs. Use this function with arguments ("%s", "your text here")
    /// </summary>
    public readonly delegate* unmanaged <byte*, byte*, void> Logprintf;

#pragma warning disable S1144 // Unused private types or members should be removed
    private readonly IntPtr _placeholder1;
    private readonly IntPtr _placeholder2;
    private readonly IntPtr _placeholder3;
    private readonly IntPtr _placeholder4;
    private readonly IntPtr _placeholder5;
    private readonly IntPtr _placeholder6;
    private readonly IntPtr _placeholder7;
    private readonly IntPtr _placeholder8;
    private readonly IntPtr _placeholder9;
    private readonly IntPtr _placeholderA;
    private readonly IntPtr _placeholderB;
    private readonly IntPtr _placeholderC;
    private readonly IntPtr _placeholderD;
    private readonly IntPtr _placeholderE;
    private readonly IntPtr _placeholderF;
#pragma warning restore S1144 // Unused private types or members should be removed

    /// <summary>
    /// void* AmxFunctionTable[]
    /// </summary>
    public readonly AmxExport* AmxExports;

    /// <summary>
    /// int (*AmxCallPublicFilterScript)(char *szFunctionName)
    /// </summary>
    public readonly delegate* unmanaged <byte*, int> CallPublicFs;

    /// <summary>
    /// int (*AmxCallPublicGameMode)(char *szFunctionName)
    /// </summary>
    public readonly delegate* unmanaged <byte*, int> CallPublicGm;
}