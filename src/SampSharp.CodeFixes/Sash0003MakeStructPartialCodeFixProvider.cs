using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using SampSharp.Analyzer;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace SampSharp.CodeFixes;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(Sash0003MakeStructPartialCodeFixProvider)), Shared]
public class Sash0003MakeStructPartialCodeFixProvider : CodeFixProvider
{
    private const string Title = "Make struct partial";

    public sealed override ImmutableArray<string> FixableDiagnosticIds => [AnalyzerIds.Sash0003ApiStructMustBeReadonlyPartial.Id];

    public sealed override FixAllProvider GetFixAllProvider()
    {
        return WellKnownFixAllProviders.BatchFixer;
    }

    public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
        var diagnostic = context.Diagnostics[0];
        var diagnosticSpan = diagnostic.Location.SourceSpan;

        var structDeclaration = root
            ?.FindToken(diagnosticSpan.Start).Parent
            ?.AncestorsAndSelf()
            .OfType<StructDeclarationSyntax>()
            .First();

        if (structDeclaration == null)
        {
            return;
        }

        context.RegisterCodeFix(
            CodeAction.Create(
                title: Title,
                createChangedDocument: c => Fix(context.Document, structDeclaration, c),
                equivalenceKey: Title),
            diagnostic);
    }

    private static async Task<Document> Fix(Document document, StructDeclarationSyntax structDeclaration, CancellationToken cancellationToken)
    {
        var root = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);

        if (root == null)
        {
            return document;
        }

        var newStructDeclaration = structDeclaration;
        
        if (!newStructDeclaration.Modifiers.Any(SyntaxKind.PartialKeyword))
        {
            newStructDeclaration = newStructDeclaration
                .WithModifiers(
                    structDeclaration.Modifiers.Add(
                        Token(SyntaxKind.PartialKeyword)));
        }

        if (!newStructDeclaration.Modifiers.Any(SyntaxKind.ReadOnlyKeyword))
        {
            newStructDeclaration = newStructDeclaration
                .WithModifiers(
                    structDeclaration.Modifiers.Insert(0,
                        Token(SyntaxKind.ReadOnlyKeyword)));
        }

        var newRoot = root.ReplaceNode(structDeclaration, newStructDeclaration);

        return document.WithSyntaxRoot(newRoot);
    }
}
