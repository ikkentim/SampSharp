using System.Collections.Immutable;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Host.Mef;
using SampSharp.OpenMp.Core;

namespace SampSharp.CodeFixes.Tests;

/// <summary>
/// Drives an analyzer + code fix against a snippet, applies the first code action,
/// and returns the resulting document text.
/// </summary>
internal static class CodeFixTestHelper
{
    private static readonly Lazy<IReadOnlyList<MetadataReference>> _references = new(BuildReferences);

    public static async Task<string> ApplyFixAsync(
        DiagnosticAnalyzer analyzer,
        CodeFixProvider codeFix,
        string source,
        bool allowUnsafe = true)
    {
        var workspace = new AdhocWorkspace(MefHostServices.DefaultHost);
        var projectId = ProjectId.CreateNewId();
        var documentId = DocumentId.CreateNewId(projectId);

        var compilationOptions = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, allowUnsafe: allowUnsafe);

        var solution = workspace.CurrentSolution
            .AddProject(projectId, "TestProject", "TestProject", LanguageNames.CSharp)
            .WithProjectCompilationOptions(projectId, compilationOptions)
            .AddMetadataReferences(projectId, _references.Value)
            .AddDocument(documentId, "Test.cs", source);

        var document = solution.GetDocument(documentId)!;
        var compilation = (await document.Project.GetCompilationAsync().ConfigureAwait(false))!;
        var compilationWithAnalyzers = compilation.WithAnalyzers(ImmutableArray.Create(analyzer));
        var diagnostics = await compilationWithAnalyzers.GetAnalyzerDiagnosticsAsync().ConfigureAwait(false);

        var fixable = diagnostics.FirstOrDefault(d => codeFix.FixableDiagnosticIds.Contains(d.Id));
        if (fixable == null)
        {
            return source;
        }

        var actions = new List<CodeAction>();
        var context = new CodeFixContext(document, fixable, (a, _) => actions.Add(a), CancellationToken.None);
        await codeFix.RegisterCodeFixesAsync(context).ConfigureAwait(false);

        if (actions.Count == 0)
        {
            return source;
        }

        var operations = await actions[0].GetOperationsAsync(CancellationToken.None).ConfigureAwait(false);
        var solutionAfter = operations.OfType<ApplyChangesOperation>().Single().ChangedSolution;
        var docAfter = solutionAfter.GetDocument(documentId)!;
        var text = await docAfter.GetTextAsync().ConfigureAwait(false);

        return text.ToString();
    }

    public static async Task<CSharpCompilationOptions?> ApplyFixAndGetCompilationOptionsAsync(
        DiagnosticAnalyzer analyzer,
        CodeFixProvider codeFix,
        string source,
        bool allowUnsafe = false)
    {
        var workspace = new AdhocWorkspace(MefHostServices.DefaultHost);
        var projectId = ProjectId.CreateNewId();
        var documentId = DocumentId.CreateNewId(projectId);

        var compilationOptions = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, allowUnsafe: allowUnsafe);

        var solution = workspace.CurrentSolution
            .AddProject(projectId, "TestProject", "TestProject", LanguageNames.CSharp)
            .WithProjectCompilationOptions(projectId, compilationOptions)
            .AddMetadataReferences(projectId, _references.Value)
            .AddDocument(documentId, "Test.cs", source);

        var document = solution.GetDocument(documentId)!;
        var compilation = (await document.Project.GetCompilationAsync().ConfigureAwait(false))!;
        var compilationWithAnalyzers = compilation.WithAnalyzers(ImmutableArray.Create(analyzer));
        var diagnostics = await compilationWithAnalyzers.GetAnalyzerDiagnosticsAsync().ConfigureAwait(false);

        var fixable = diagnostics.FirstOrDefault(d => codeFix.FixableDiagnosticIds.Contains(d.Id));
        if (fixable == null)
        {
            return null;
        }

        var actions = new List<CodeAction>();
        var context = new CodeFixContext(document, fixable, (a, _) => actions.Add(a), CancellationToken.None);
        await codeFix.RegisterCodeFixesAsync(context).ConfigureAwait(false);

        if (actions.Count == 0)
        {
            return null;
        }

        var operations = await actions[0].GetOperationsAsync(CancellationToken.None).ConfigureAwait(false);
        var solutionAfter = operations.OfType<ApplyChangesOperation>().Single().ChangedSolution;
        var projectAfter = solutionAfter.GetProject(projectId)!;

        return projectAfter.CompilationOptions as CSharpCompilationOptions;
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
