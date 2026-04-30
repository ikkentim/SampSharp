using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SampSharp.SourceGenerator.Helpers;

public static class SymbolExtensions
{
    private static readonly SymbolDisplayFormat _fullyQualifiedFormatWithoutGlobal =
        SymbolDisplayFormat.FullyQualifiedFormat.WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.OmittedAsContaining);

    public static bool IsPartial(this MemberDeclarationSyntax syntax)
    {
        return HasModifier(syntax, SyntaxKind.PartialKeyword);
    }

    public static bool HasModifier(this MemberDeclarationSyntax syntax, SyntaxKind modifier)
    {
        return syntax.Modifiers.Any(m => m.IsKind(modifier));
    }

    public static IEnumerable<AttributeData> GetAttributes(this ISymbol symbol, string attributeName)
    {
        return symbol.GetAttributes().GetAttributes(attributeName);
    }

    public static AttributeData? GetAttribute(this ISymbol symbol, string attributeName)
    {
        return symbol.GetAttributes(attributeName)
            .FirstOrDefault();
    }

    public static AttributeData? GetReturnTypeAttribute(this IMethodSymbol symbol, string attributeName)
    {
        return symbol.GetReturnTypeAttributes()
            .GetAttributes(attributeName)
            .FirstOrDefault();
    }

    public static bool IsSame(this ITypeSymbol symbol, string typeFQN)
    {
        return string.Equals(symbol.ToDisplayString(_fullyQualifiedFormatWithoutGlobal), typeFQN, StringComparison.Ordinal);
    }

    public static bool IsSame(this ISymbol symbol, ISymbol other)
    {
        return SymbolEqualityComparer.Default.Equals(symbol, other);
    }

    private static IEnumerable<AttributeData> GetAttributes(this ImmutableArray<AttributeData> attribute, string attributeName)
    {
        return attribute
            .Where(x =>
                string.Equals(
                    x.AttributeClass?.ToDisplayString(_fullyQualifiedFormatWithoutGlobal),
                    attributeName,
                    StringComparison.Ordinal
                )
            );
    }
}