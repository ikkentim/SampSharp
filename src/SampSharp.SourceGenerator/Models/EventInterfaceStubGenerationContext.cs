using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace SampSharp.SourceGenerator.Models;

public record EventInterfaceStubGenerationContext(
    INamedTypeSymbol Symbol,
    InterfaceDeclarationSyntax Syntax,
    MarshallingStubGenerationContext[] Methods,
    string Library,
    string NativeTypeName)
{
    public TypeSyntax Type { get; } = GetSelfType(Symbol, Syntax);
    
    private static TypeSyntax GetSelfType(ISymbol symbol, InterfaceDeclarationSyntax syntax)
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