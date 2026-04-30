using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using SampSharp.Analyzer.Helpers;

namespace SampSharp.Analyzer.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class Sash0007ApiStructMustNotContainFields : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => [AnalyzerIds.Sash0007ApiStructMustNotContainFields];

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

        if (!context.SemanticModel.HasAttribute(structDeclaration, apiAttributeType))
        {
            return;
        }

        var fieldMembers = structDeclaration.Members.OfType<FieldDeclarationSyntax>().ToList();

        var propertyMembersWithBackingFields = structDeclaration.Members
            .OfType<PropertyDeclarationSyntax>()
            .Where(property => property.AccessorList?.Accessors.Any(accessor =>
                accessor.Body?.Statements.OfType<ExpressionStatementSyntax>()
                    .Any(s => s.Expression is AssignmentExpressionSyntax { Left: IdentifierNameSyntax { Identifier.Text: "field" } }) == true ||
                accessor.Body == null && accessor.ExpressionBody == null
            ) == true)
            .ToList();

        foreach (var fieldMember in fieldMembers)
        {
            var diagnostic = Diagnostic.Create(
                AnalyzerIds.Sash0007ApiStructMustNotContainFields,
                fieldMember.GetLocation(),
                structDeclaration.Identifier.ToString());
            
            context.ReportDiagnostic(diagnostic);
        }
        
        foreach (var propertyMember in propertyMembersWithBackingFields)
        {
            var diagnostic = Diagnostic.Create(
                AnalyzerIds.Sash0007ApiStructMustNotContainFields,
                propertyMember.GetLocation(),
                structDeclaration.Identifier.ToString());
            
            context.ReportDiagnostic(diagnostic);
        }
    }
}