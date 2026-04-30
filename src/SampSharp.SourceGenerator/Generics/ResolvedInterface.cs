using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SampSharp.SourceGenerator.Helpers;

namespace SampSharp.SourceGenerator.Generics;

public readonly record struct ResolvedInterface(InterfaceDeclarationSyntax Syntax, Dictionary<string, ITypeSymbol>? TypeArguments)
{
    public ITypeSymbol Resolve(ITypeSymbol type)
    {
        if (type.TypeKind == TypeKind.TypeParameter && TypeArguments != null)
        {
            if (TypeArguments.TryGetValue(type.Name, out var result))
            {
                return result;
            }
        }

        return type;
    }

    public IEnumerable<ResolvedInterfaceMethod> GetInstanceMethods()
    {
        var iface = this;
        return Syntax.Members.OfType<MethodDeclarationSyntax>()
            .Select(m => new ResolvedInterfaceMethod(m, iface))
            .Where(x => !x.Method.HasModifier(SyntaxKind.StaticKeyword));
    }
}