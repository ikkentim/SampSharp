using System.Runtime.CompilerServices;

namespace SampSharp.SourceGenerator.Tests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        VerifySourceGenerators.Initialize();
        Verifier.UseProjectRelativeDirectory("Snapshots");
    }
}
