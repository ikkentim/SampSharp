using System;
using SampSharp.Core.Hosting;

namespace SampSharp.Core.UnitTests.TestHelpers;

public unsafe class ApiScope : IDisposable
{
    private SampSharpApi* _api;
        
    public ApiScope(SampSharpApi* api)
    {
        _api = api;

        Interop.Initialize(InitializeMock);
    }

    public ApiScope(InteropStructs.SampSharpApiRw* api)
    {
        _api = (SampSharpApi*)api;

        Interop.Initialize(InitializeMock);
    }
        
    SampSharpApi* InitializeMock(void* pub, void* tick)
    {
        return _api;
    }

    SampSharpApi* UninitializeMock(void* pub, void* tick)
    {
        return (SampSharpApi*)IntPtr.Zero;
    }

    public void Dispose()
    {
        Interop.Initialize(UninitializeMock);
    }
}