using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using SampSharp.Analyzer.Helpers;

namespace SampSharp.Analyzer.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class Sash0001ExtensionAttributeAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [AnalyzerIds.Sash0001MissingExtensionAttribute];

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeSyntaxNode, SyntaxKind.ClassDeclaration);
    }

    private static void AnalyzeSyntaxNode(SyntaxNodeAnalysisContext context)
    {
        var extensionType = context.Compilation.GetTypeByMetadataName(Constants.ExtensionFQN);
        var extensionAttributeType = context.Compilation.GetTypeByMetadataName(Constants.ExtensionAttributeFQN);

        if (extensionType == null || extensionAttributeType == null)
        {
            return;
        }

        var classDeclaration = (ClassDeclarationSyntax)context.Node;

        if (!context.SemanticModel.IsBaseType(classDeclaration, extensionType))
        {
            return;
        }

        if (!context.SemanticModel.HasAttribute(classDeclaration, extensionAttributeType))
        {
            var diagnostic = Diagnostic.Create(
                AnalyzerIds.Sash0001MissingExtensionAttribute,
                classDeclaration.Identifier.GetLocation(),
                classDeclaration.Identifier.ToString());

            context.ReportDiagnostic(diagnostic);
        }
    }
}