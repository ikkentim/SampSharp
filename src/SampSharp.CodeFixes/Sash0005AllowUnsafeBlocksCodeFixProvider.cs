using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis;
using SampSharp.Analyzer;
using System.Collections.Immutable;
using System.Composition;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;

namespace SampSharp.CodeFixes;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(Sash0005AllowUnsafeBlocksCodeFixProvider)), Shared]
public class Sash0005AllowUnsafeBlocksCodeFixProvider : CodeFixProvider
{
    private const string Title = "Set 'AllowUnsafeBlocks' to true";

    public sealed override ImmutableArray<string> FixableDiagnosticIds => [AnalyzerIds.Sash0005ApiStructRequiresAllowUnsafeBlocks.Id];

    public sealed override FixAllProvider GetFixAllProvider()
    {
        return WellKnownFixAllProviders.BatchFixer;
    }

    public sealed override Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var diagnostic = context.Diagnostics[0];

        context.RegisterCodeFix(
            CodeAction.Create(
                title: Title,
                createChangedSolution: _ => Fix(context.Document.Project.Solution, context.Document.Project),
                equivalenceKey: Title),
            diagnostic);

        return Task.CompletedTask;
    }

    private static Task<Solution> Fix(Solution solution, Project project)
    {
        var compilationOptions = project.CompilationOptions as CSharpCompilationOptions;

        if (compilationOptions != null && !compilationOptions.AllowUnsafe)
        {
            var newCompilationOptions = compilationOptions.WithAllowUnsafe(true);
            solution = solution.WithProjectCompilationOptions(project.Id, newCompilationOptions);
        }

        return Task.FromResult(solution);
    }
}