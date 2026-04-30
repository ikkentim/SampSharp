using System;
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

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(Sash0001ExtensionAttributeCodeFixProvider)), Shared]
public class Sash0001ExtensionAttributeCodeFixProvider : CodeFixProvider
{
    private const string Title = "Add 'ExtensionAttribute'";

    public sealed override ImmutableArray<string> FixableDiagnosticIds => [AnalyzerIds.Sash0001MissingExtensionAttribute.Id];

    public sealed override FixAllProvider GetFixAllProvider()
    {
        return WellKnownFixAllProviders.BatchFixer;
    }

    public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
        var diagnostic = context.Diagnostics[0];
        var diagnosticSpan = diagnostic.Location.SourceSpan;

        var classDeclaration = root
            ?.FindToken(diagnosticSpan.Start).Parent
            ?.AncestorsAndSelf()
            .OfType<ClassDeclarationSyntax>()
            .First();

        if (classDeclaration == null)
        {
            return;
        }

        context.RegisterCodeFix(
            CodeAction.Create(
                title: Title,
                createChangedDocument: c => Fix(context.Document, classDeclaration, c),
                equivalenceKey: Title),
            diagnostic);
    }

    private static async Task<Document> Fix(Document document, ClassDeclarationSyntax classDeclaration, CancellationToken cancellationToken)
    {
        var root = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);

        if (root == null)
        {
            return document;
        }

#pragma warning disable RS1035 // Do not use APIs banned for analyzers
        var random = new Random();

        var bytes = new byte[8];
        random.NextBytes(bytes);
#pragma warning restore RS1035 // Do not use APIs banned for analyzers
        var id = BitConverter.ToUInt64(bytes, 0);

        var newClassDeclaration = classDeclaration.AddAttributeLists(
            AttributeList(
                    SingletonSeparatedList(
                        Attribute(
                                ParseName("Extension"))
                            .WithArgumentList(
                                AttributeArgumentList(
                                    SingletonSeparatedList(
                                        AttributeArgument(
                                            LiteralExpression(
                                                SyntaxKind.NumericLiteralExpression,
                                                Literal(
                                                    $"0x{id:x16}",
                                                    id)))))))));

        var newRoot = root.ReplaceNode(classDeclaration, newClassDeclaration);
        
        if (root is CompilationUnitSyntax compilationUnit && compilationUnit.Usings.All(u => u.Name?.ToString() != "SampSharp.OpenMp.Core"))
        {
            newRoot = compilationUnit.AddUsings(UsingDirective(ParseName("SampSharp.OpenMp.Core")));
        }

        return document.WithSyntaxRoot(newRoot);
    }
}
