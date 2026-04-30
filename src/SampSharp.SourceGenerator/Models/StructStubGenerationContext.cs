using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace SampSharp.SourceGenerator.Models;

public record StructStubGenerationContext(
    ISymbol Symbol,
    StructDeclarationSyntax Syntax,
    ApiMethodStubGenerationContext[] Methods,
    ImplementingType[] ImplementingTypes,
    bool IsPartial,
    string Library,
    List<MemberDeclarationSyntax> PublicMembers)
{
    public TypeSyntax Type { get; } = GetSelfType(Symbol, Syntax);
    
    private static TypeSyntax GetSelfType(ISymbol symbol, StructDeclarationSyntax syntax)
    {
        if (syntax.TypeParameterList == null)
        {
            return ParseTypeName(symbol.Name);
        }

        return GenericName(
                Identifier(symbol.Name))
            .WithTypeArgumentList(
                TypeArgumentList(
                    SeparatedList<TypeSyntax>(
                        syntax.TypeParameterList!.Parameters.Select(x => IdentifierName(x.Identifier)))));
    }
}

public readonly record struct ImplementingType(DefiniteType Type, DefiniteType[] CastPath);

public readonly record struct DefiniteType(ITypeSymbol Symbol, TypeSyntax Syntax);