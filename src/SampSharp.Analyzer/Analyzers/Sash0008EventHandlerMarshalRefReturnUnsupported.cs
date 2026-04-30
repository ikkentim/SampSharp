using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using SampSharp.Analyzer.Helpers;

namespace SampSharp.Analyzer.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class Sash0008EventHandlerMarshalRefReturnUnsupported : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [AnalyzerIds.Sash0008EventHandlerMarshalRefReturnUnsupported];

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeSyntaxNode, SyntaxKind.InterfaceDeclaration);
    }

    private static void AnalyzeSyntaxNode(SyntaxNodeAnalysisContext context)
    {
        var eventHandlerAttributeType = context.Compilation.GetTypeByMetadataName(Constants.EventHandlerAttributeFQN);
        var marshalUsingAttribute = context.Compilation.GetTypeByMetadataName(Constants.MarshalUsingAttributeFQN);
        var nativeMarshallingAttribute = context.Compilation.GetTypeByMetadataName(Constants.NativeMarshallingAttributeFQN);

        if (eventHandlerAttributeType == null || marshalUsingAttribute == null || nativeMarshallingAttribute == null)
        {
            return;
        }

        var structDeclaration = (InterfaceDeclarationSyntax)context.Node;

        if (!context.SemanticModel.HasAttribute(structDeclaration, eventHandlerAttributeType))
        {
            return;
        }

        foreach (var method in structDeclaration.Members.OfType<MethodDeclarationSyntax>())
        {
            if (method.ReturnType is RefTypeSyntax refType)
            {
                if (method.AttributeLists.Any(attribList => attribList.Target?.Identifier.IsKind(SyntaxKind.ReturnKeyword) == true &&
                                                            context.SemanticModel.HasAttribute(attribList, marshalUsingAttribute)) ||
                    context.SemanticModel.HasAttribute(refType.Type, nativeMarshallingAttribute))
                {
                    var diagnostic = Diagnostic.Create(
                        AnalyzerIds.Sash0008EventHandlerMarshalRefReturnUnsupported,
                        method.Identifier.GetLocation(),
                        method.Identifier.ToString());

                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }
}