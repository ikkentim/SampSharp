using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using SampSharp.Analyzer.Helpers;

namespace SampSharp.Analyzer.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class Sash0005ApiStructRequiresAllowUnsafeBlocks : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [AnalyzerIds.Sash0005ApiStructRequiresAllowUnsafeBlocks];

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeSyntaxNode, SyntaxKind.Attribute);
    }

    private static void AnalyzeSyntaxNode(SyntaxNodeAnalysisContext context)
    {
        var apiAttributeType = context.Compilation.GetTypeByMetadataName(Constants.ApiAttributeFQN);

        if (apiAttributeType == null)
        {
            return;
        }

        var attribute = (AttributeSyntax)context.Node;

        if (context.SemanticModel.IsAttribute(attribute, apiAttributeType))
        {
            var compilationOptions = context.Compilation.Options as CSharpCompilationOptions;
            if (compilationOptions == null || !compilationOptions.AllowUnsafe)
            {
                var diagnostic = Diagnostic.Create(
                    AnalyzerIds.Sash0005ApiStructRequiresAllowUnsafeBlocks,
                    attribute.GetLocation());

                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}