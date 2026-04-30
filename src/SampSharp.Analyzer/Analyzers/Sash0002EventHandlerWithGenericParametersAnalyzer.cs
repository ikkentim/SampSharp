using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using SampSharp.Analyzer.Helpers;

namespace SampSharp.Analyzer.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class Sash0002EventHandlerWithGenericParametersAnalyzer : DiagnosticAnalyzer
{
    public const string EventHandlerAttributeTypeFQN = "SampSharp.OpenMp.Core.OpenMpEventHandlerAttribute";

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [AnalyzerIds.Sash0002GenericEventHandlerUnsupported];

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeSyntaxNode, SyntaxKind.InterfaceDeclaration);
    }

    private static void AnalyzeSyntaxNode(SyntaxNodeAnalysisContext context)
    {
        var eventHandlerAttributeType = context.Compilation.GetTypeByMetadataName(EventHandlerAttributeTypeFQN);

        if (eventHandlerAttributeType == null)
        {
            return;
        }

        var ifaceDeclaration = (InterfaceDeclarationSyntax)context.Node;

        if (ifaceDeclaration.TypeParameterList == null || ifaceDeclaration.TypeParameterList.Parameters.Count == 0)
        {
            return;
        }

        if (context.SemanticModel.HasAttribute(ifaceDeclaration, eventHandlerAttributeType))
        {
            var diagnostic = Diagnostic.Create(
                AnalyzerIds.Sash0002GenericEventHandlerUnsupported,
                ifaceDeclaration.Identifier.GetLocation(),
                ifaceDeclaration.Identifier.ToString());

            context.ReportDiagnostic(diagnostic);
        }
    }
}