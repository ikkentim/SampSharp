using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace SampSharp.SourceGenerator.SyntaxFactories;

/// <summary>
/// Creates type syntax nodes.
/// </summary>
public static class TypeSyntaxFactory
{
    /// <summary>
    /// <c>nint</c>
    /// </summary>
    public static TypeSyntax IntPtrType { get; } = ParseTypeName("nint");

    /// <summary>
    /// <c>int</c>
    /// </summary>
    public static TypeSyntax IntType { get; } = PredefinedType(Token(SyntaxKind.IntKeyword));

    /// <summary>
    /// <c>object</c>
    /// </summary>
    public static TypeSyntax ObjectType { get; } = PredefinedType(Token(SyntaxKind.ObjectKeyword));

    /// <summary>
    /// <c>global::System.Span&lt;byte&gt;</c>
    /// </summary>
    public static TypeSyntax SpanOfBytes { get; } = QualifiedName(
        AliasQualifiedName(
            IdentifierName(
                Token(SyntaxKind.GlobalKeyword)),
            IdentifierName("System")),
        GenericName(
                Identifier("Span"))
            .WithTypeArgumentList(
                TypeArgumentList(SingletonSeparatedList<TypeSyntax>(
                    PredefinedType(
                        Token(SyntaxKind.ByteKeyword))))));

    public static IdentifierNameSyntax IdentifierNameGlobal(string typeFQN)
    {
        return IdentifierName(ToGlobalTypeString(typeFQN));
    }

    public static SyntaxToken IdentifierGlobal(string typeFQN)
    {
        return Identifier(ToGlobalTypeString(typeFQN));
    }

    public static TypeSyntax TypeNameGlobal(string typeFQN)
    {
        return ParseTypeName(ToGlobalTypeString(typeFQN));
    }

    public static TypeSyntax TypeNameGlobal(ITypeSymbol symbol)
    {
        if (symbol.TypeKind == TypeKind.TypeParameter)
        {
            return ParseTypeName(symbol.Name);
        }

        if (symbol is INamedTypeSymbol { IsTupleType: true } namedTypeSymbol)
        {
            var elements = new List<TupleElementSyntax>();
            foreach (var element in namedTypeSymbol.TupleElements)
            {
                var el = TupleElement(TypeNameGlobal(element.Type));
                if (element.IsExplicitlyNamedTupleElement)
                {
                    el = el.WithIdentifier(Identifier(element.Name));
                }

                elements.Add(el);
            }

            return TupleType(SeparatedList(elements));
        }

        return ParseTypeName(
            symbol.SpecialType == SpecialType.None
                ? ToGlobalTypeString(symbol)
                : symbol.ToDisplayString());
    }

    public static TypeSyntax TypeNameGlobal(IMethodSymbol returnTypeOfMethod, bool includeNullable = false)
    {
        var result = TypeNameGlobal(returnTypeOfMethod.ReturnType);
        
        if (includeNullable && returnTypeOfMethod.ReturnNullableAnnotation == NullableAnnotation.Annotated)
        {
            result = NullableType(result);
        }

        if (returnTypeOfMethod.ReturnsByRef || returnTypeOfMethod.ReturnsByRefReadonly)
        {
            result = returnTypeOfMethod.ReturnsByRefReadonly
                ? RefType(result).WithReadOnlyKeyword(Token(SyntaxKind.ReadOnlyKeyword))
                : RefType(result);
        }

        return result;
    }
    
    public static TypeSyntax GenericType(string typeFQN, TypeSyntax typeArgument)
    {
        return GenericName(
                IdentifierGlobal(typeFQN))
            .WithTypeArgumentList(
                TypeArgumentList(
                    SingletonSeparatedList(typeArgument)));
    }

    public static string ToGlobalTypeString(string typeFQN)
    {
        return $"global::{typeFQN}";
    }

    public static string ToGlobalTypeString(ITypeSymbol symbol)
    {
        return symbol.SpecialType == SpecialType.None && symbol.TypeKind != TypeKind.Pointer
            ? ToGlobalTypeString(symbol.ToDisplayString()) 
            : symbol.ToDisplayString();
    }
}