using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SampSharp.SourceGenerator.Generics;

public readonly record struct ResolvedInterfaceMethod(MethodDeclarationSyntax Method, ResolvedInterface Interface);