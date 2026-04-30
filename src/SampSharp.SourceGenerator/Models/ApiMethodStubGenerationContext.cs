using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SampSharp.SourceGenerator.Marshalling;

namespace SampSharp.SourceGenerator.Models;

public record ApiMethodStubGenerationContext(
    MethodDeclarationSyntax Declaration,
    IMethodSymbol Symbol,
    IdentifierStubContext[] Parameters,
    IdentifierStubContext ReturnValue,
    string Library,
    string NativeTypeName) : MarshallingStubGenerationContext(Symbol, Parameters, ReturnValue);