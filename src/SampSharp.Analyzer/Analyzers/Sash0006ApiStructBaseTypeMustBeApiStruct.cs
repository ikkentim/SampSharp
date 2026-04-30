using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using SampSharp.Analyzer.Helpers;

namespace SampSharp.Analyzer.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class Sash0006ApiStructBaseTypeMustBeApiStruct : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [AnalyzerIds.Sash0006ApiStructBaseTypeMustBeApiStruct];

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

        if (!context.SemanticModel.IsAttribute(attribute, apiAttributeType) || attribute.ArgumentList == null)
        {
            return;
        }

        var structDeclaration = attribute.FirstAncestorOrSelf<StructDeclarationSyntax>();
        if (structDeclaration == null)
        {
            return;
        }

        var structName = structDeclaration.Identifier.Text;

        foreach (var arg in attribute.ArgumentList.Arguments)
        {
            if (arg.Expression is TypeOfExpressionSyntax typeOfExpression)
            {
                if (!context.SemanticModel.HasAttribute(typeOfExpression.Type, apiAttributeType))
                {
                    var diagnostic = Diagnostic.Create(
                        AnalyzerIds.Sash0006ApiStructBaseTypeMustBeApiStruct,
                        typeOfExpression.GetLocation(), typeOfExpression.Type.ToString(), structName);

                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }
}