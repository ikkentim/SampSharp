using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SampSharp.SourceGenerator.SyntaxFactories;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace SampSharp.SourceGenerator;

public class FullyQualifiedTypeRewriter : CSharpSyntaxRewriter
{
    private readonly SemanticModel _semanticModel;

    public FullyQualifiedTypeRewriter(SemanticModel semanticModel)
    {
        _semanticModel = semanticModel;
    }

    public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
    {
        return ResolveMemberAccessSyntaxTree(node);
    }

    private MemberAccessExpressionSyntax ResolveMemberAccessSyntaxTree(MemberAccessExpressionSyntax node)
    {
        switch (node.Expression)
        {
            case MemberAccessExpressionSyntax access:
                return node.WithExpression(ResolveMemberAccessSyntaxTree(access));
            case IdentifierNameSyntax name:
                {
                    var symbol = _semanticModel.GetSymbolInfo(name);
                    if (symbol.Symbol is INamedTypeSymbol type)
                    {
                        return node.WithExpression(name.WithIdentifier(Identifier(TypeSyntaxFactory.ToGlobalTypeString(type))));
                    }

                    break;
                }
        }

        return node;
    }

    public override SyntaxNode VisitAttribute(AttributeSyntax node)
    {
        var symbol = _semanticModel.GetSymbolInfo(node.Name).Symbol;
    
        if (symbol != null && !node.Name.ToString().StartsWith("global::"))
        {
            node = node
                .WithName(ParseName($"global::{symbol.ContainingNamespace}.{node.Name}"))
                .WithArgumentList((AttributeArgumentListSyntax?)Visit(node.ArgumentList));

        }
    
        return node;
    }

    public override SyntaxNode? VisitIdentifierName(IdentifierNameSyntax node)
    {
        var symbolInfo = _semanticModel.GetSymbolInfo(node);
        var symbol = symbolInfo.Symbol;

        if (symbol is ITypeSymbol { SpecialType: SpecialType.None } typeSymbol)
        {
            if (typeSymbol.TypeKind != TypeKind.TypeParameter)
            {
                return IdentifierName(TypeSyntaxFactory.ToGlobalTypeString(typeSymbol));
            }
        }

        return base.VisitIdentifierName(node);
    }

    public override SyntaxNode? VisitQualifiedName(QualifiedNameSyntax node)
    {
        var symbolInfo = _semanticModel.GetSymbolInfo(node);
        var symbol = symbolInfo.Symbol;

        if (symbol is ITypeSymbol typeSymbol)
        {
            return IdentifierName(TypeSyntaxFactory.ToGlobalTypeString(typeSymbol));
        }

        return base.VisitQualifiedName(node);
    }

    public override SyntaxNode? VisitGenericName(GenericNameSyntax node)
    {
        var symbolInfo = _semanticModel.GetSymbolInfo(node);
        var symbol = symbolInfo.Symbol;

        if (symbol is ITypeSymbol typeSymbol)
        {
            var fullyQualifiedName = TypeSyntaxFactory.ToGlobalTypeString(typeSymbol);
            var identifier = fullyQualifiedName.Split('<')[0]; // Get the identifier part
            var typeArguments = node.TypeArgumentList.Arguments.Select(arg => (TypeSyntax)Visit(arg)).ToArray();

            return GenericName(identifier)
                .WithTypeArgumentList(
                    TypeArgumentList(
                        SeparatedList(typeArguments)));
        }

        return base.VisitGenericName(node);
    }
    public override SyntaxNode? VisitInvocationExpression(InvocationExpressionSyntax node)
    {
        if (node.Expression is MemberAccessExpressionSyntax { Name: GenericNameSyntax memberGenericName } memberAccess)
        {
            var typeArguments = memberGenericName.TypeArgumentList.Arguments.Select(arg =>
                {
                    var symbolInfo = _semanticModel.GetSymbolInfo(arg);
                    var symbol = symbolInfo.Symbol;

                    if (symbol is ITypeSymbol typeSymbol)
                    {
                        return IdentifierName(TypeSyntaxFactory.ToGlobalTypeString(typeSymbol));
                    }

                    return arg;
                })
                .ToArray();

            var newExpression = memberAccess
                .WithName(memberGenericName
                    .WithTypeArgumentList(
                        TypeArgumentList(
                            SeparatedList(typeArguments))))
                .WithExpression(
                    (ExpressionSyntax)Visit(memberAccess.Expression));

            return node
                .WithExpression(newExpression)
                .WithArgumentList(
                    (ArgumentListSyntax)Visit(node.ArgumentList));
        }

        return base.VisitInvocationExpression(node);
    }
}
