using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using SampSharp.OpenMp.Core;

namespace SampSharp.SourceGenerator.Tests;

/// <summary>
/// Helper for creating Roslyn <see cref="CSharpCompilation" /> instances used in source generator snapshot tests.
/// </summary>
public static class CompilationHelper
{
    private static readonly Lazy<IReadOnlyList<MetadataReference>> _defaultReferences = new(BuildDefaultReferences);

    /// <summary>
    /// Creates a <see cref="CSharpCompilation" /> with the given source text and all default metadata references
    /// (BCL + SampSharp.OpenMp.Core).
    /// </summary>
    public static CSharpCompilation CreateCompilation(string source, string assemblyName = "TestCompilation")
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(source);

        return CSharpCompilation.Create(
            assemblyName,
            syntaxTrees: [syntaxTree],
            references: _defaultReferences.Value,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, allowUnsafe: true));
    }

    private static IReadOnlyList<MetadataReference> BuildDefaultReferences()
    {
        var runtimeDir = System.IO.Path.GetDirectoryName(typeof(object).Assembly.Location)!;

        var dlls = new[]
        {
            typeof(object).Assembly.Location,                  // System.Private.CoreLib
            typeof(Attribute).Assembly.Location,               // may differ per runtime
            typeof(Console).Assembly.Location,
            typeof(Marshal).Assembly.Location,                 // System.Runtime.InteropServices
            typeof(CustomMarshallerAttribute).Assembly.Location, // System.Runtime.InteropServices.Marshalling
        };

        var extraPaths = new[]
        {
            "System.Runtime.dll",
            "System.Collections.dll",
            "System.Numerics.Vectors.dll",
        };

        var refs = new List<MetadataReference>();

        foreach (var dll in dlls)
        {
            if (!string.IsNullOrEmpty(dll) && System.IO.File.Exists(dll))
            {
                refs.Add(MetadataReference.CreateFromFile(dll));
            }
        }

        foreach (var name in extraPaths)
        {
            var path = System.IO.Path.Combine(runtimeDir, name);
            if (System.IO.File.Exists(path))
            {
                refs.Add(MetadataReference.CreateFromFile(path));
            }
        }

        // SampSharp.OpenMp.Core — supplies all attribute types and well-known marshaller types
        refs.Add(MetadataReference.CreateFromFile(typeof(OpenMpApiAttribute).Assembly.Location));

        return refs;
    }
}
