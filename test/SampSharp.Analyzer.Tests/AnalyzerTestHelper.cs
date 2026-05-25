using System.Collections.Immutable;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using SampSharp.OpenMp.Core;

namespace SampSharp.Analyzer.Tests;

/// <summary>
/// Lightweight helper that compiles a C# source string with the SampSharp.OpenMp.Core
/// references available and runs an analyzer against it.
/// </summary>
internal static class AnalyzerTestHelper
{
    private static readonly Lazy<IReadOnlyList<MetadataReference>> _references = new(BuildReferences);

    public static async Task<ImmutableArray<Diagnostic>> GetDiagnosticsAsync(
        DiagnosticAnalyzer analyzer,
        string source,
        bool allowUnsafe = true)
    {
        var compilation = CreateCompilation(source, allowUnsafe);
        var withAnalyzers = compilation.WithAnalyzers(ImmutableArray.Create(analyzer));
        var diagnostics = await withAnalyzers.GetAnalyzerDiagnosticsAsync().ConfigureAwait(false);
        return diagnostics;
    }

    public static CSharpCompilation CreateCompilation(string source, bool allowUnsafe = true, string assemblyName = "TestCompilation")
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(source);

        return CSharpCompilation.Create(
            assemblyName,
            syntaxTrees: [syntaxTree],
            references: _references.Value,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, allowUnsafe: allowUnsafe));
    }

    private static IReadOnlyList<MetadataReference> BuildReferences()
    {
        var runtimeDir = Path.GetDirectoryName(typeof(object).Assembly.Location)!;
        var refs = new List<MetadataReference>();

        void Add(Assembly assembly)
        {
            if (!string.IsNullOrEmpty(assembly.Location) && File.Exists(assembly.Location))
            {
                refs.Add(MetadataReference.CreateFromFile(assembly.Location));
            }
        }

        Add(typeof(object).Assembly);
        Add(typeof(Attribute).Assembly);
        Add(typeof(Console).Assembly);
        Add(typeof(Marshal).Assembly);
        Add(typeof(CustomMarshallerAttribute).Assembly);

        foreach (var name in new[] { "System.Runtime.dll", "System.Collections.dll", "netstandard.dll" })
        {
            var path = Path.Combine(runtimeDir, name);
            if (File.Exists(path))
            {
                refs.Add(MetadataReference.CreateFromFile(path));
            }
        }

        refs.Add(MetadataReference.CreateFromFile(typeof(OpenMpApiAttribute).Assembly.Location));

        return refs;
    }
}
