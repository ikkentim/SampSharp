using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SampSharp.SourceGenerator.SyntaxFactories;

namespace SampSharp.SourceGenerator.Marshalling;

public record ManagedType(ITypeSymbol Symbol, string Name, TypeSyntax TypeName)
{
    public ManagedType(ITypeSymbol symbol) : this(symbol, TypeSyntaxFactory.ToGlobalTypeString(symbol), TypeSyntaxFactory.TypeNameGlobal(symbol)) { }
}