using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using SampSharp.Analyzer.Helpers;

namespace SampSharp.Analyzer.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class Sash0003ApiStructMustBeReadonlyPartialAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [AnalyzerIds.Sash0003ApiStructMustBeReadonlyPartial];

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeSyntaxNode, SyntaxKind.StructDeclaration);
    }

    private static void AnalyzeSyntaxNode(SyntaxNodeAnalysisContext context)
    {
        var apiAttributeType = context.Compilation.GetTypeByMetadataName(Constants.ApiAttributeFQN);

        if (apiAttributeType == null)
        {
            return;
        }

        var structDeclaration = (StructDeclarationSyntax)context.Node;

        if (structDeclaration.Modifiers.Any(SyntaxKind.PartialKeyword) && structDeclaration.Modifiers.Any(SyntaxKind.ReadOnlyKeyword))
        {
            return;
        }

        if (context.SemanticModel.HasAttribute(structDeclaration, apiAttributeType))
        {
            var diagnostic = Diagnostic.Create(
                AnalyzerIds.Sash0003ApiStructMustBeReadonlyPartial,
                structDeclaration.Identifier.GetLocation(),
                structDeclaration.Identifier.ToString());

            context.ReportDiagnostic(diagnostic);
        }
    }
}