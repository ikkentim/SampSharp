using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using SampSharp.Analyzer.Helpers;

namespace SampSharp.Analyzer.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class Sash0004ApiStructMethodMarshalRefReturnNotSupportedAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [AnalyzerIds.Sash0004ApiStructMarshalRefReturnUnsupported];

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeSyntaxNode, SyntaxKind.StructDeclaration);
    }

    private static void AnalyzeSyntaxNode(SyntaxNodeAnalysisContext context)
    {
        var apiAttributeType = context.Compilation.GetTypeByMetadataName(Constants.ApiAttributeFQN);
        var marshalUsingAttribute = context.Compilation.GetTypeByMetadataName(Constants.MarshalUsingAttributeFQN);
        var nativeMarshallingAttribute = context.Compilation.GetTypeByMetadataName(Constants.NativeMarshallingAttributeFQN);

        if (apiAttributeType == null || marshalUsingAttribute == null || nativeMarshallingAttribute == null)
        {
            return;
        }

        var structDeclaration = (StructDeclarationSyntax)context.Node;

        if (!context.SemanticModel.HasAttribute(structDeclaration, apiAttributeType))
        {
            return;
        }

        foreach (var method in structDeclaration.Members.OfType<MethodDeclarationSyntax>())
        {
            if (!method.Modifiers.Any(SyntaxKind.PartialKeyword))
            {
                return;
            }

            if (method.ReturnType is RefTypeSyntax refType)
            {
                if (method.AttributeLists.Any(attribList => attribList.Target?.Identifier.IsKind(SyntaxKind.ReturnKeyword) == true &&
                                                            context.SemanticModel.HasAttribute(attribList, marshalUsingAttribute)) ||
                    context.SemanticModel.HasAttribute(refType.Type, nativeMarshallingAttribute))
                {
                    var diagnostic = Diagnostic.Create(
                        AnalyzerIds.Sash0004ApiStructMarshalRefReturnUnsupported,
                        method.Identifier.GetLocation(),
                        method.Identifier.ToString());

                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }
}