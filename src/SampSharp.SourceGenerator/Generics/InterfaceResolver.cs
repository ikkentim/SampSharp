using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SampSharp.SourceGenerator.Generics;

public class InterfaceResolver
{
    public static List<ResolvedInterface> GetInterfaces(SemanticModel semanticModel, InterfaceDeclarationSyntax first)
    {
        var result = new List<ResolvedInterface>();
        GetInterfaces(semanticModel, first, result, null);
        return result;
    }

    private static void GetInterfaces(SemanticModel semanticModel, InterfaceDeclarationSyntax current, List<ResolvedInterface> result, Dictionary<string, ITypeSymbol>? typeSymbols)
    {
        result.Add(new ResolvedInterface(current, typeSymbols));

        if (current.BaseList == null)
        {
            return;
        }

        foreach (var baseType in current.BaseList.Types)
        {
            var interfaceTypeSyntax = baseType.Type;

            var typeSymbol = semanticModel.GetSymbolInfo(interfaceTypeSyntax).Symbol;
            if (typeSymbol is INamedTypeSymbol { TypeKind: TypeKind.Interface } namedTypeSymbol)
            {
                foreach (var reference in namedTypeSymbol.DeclaringSyntaxReferences)
                {
                    if (reference.GetSyntax() is InterfaceDeclarationSyntax iface)
                    {
                        Dictionary<string, ITypeSymbol>? dict = null;

                        if (namedTypeSymbol.TypeParameters.Length > 0)
                        {
                            dict = new Dictionary<string, ITypeSymbol>();
                            var index = 0;
                            foreach (var sym in namedTypeSymbol.TypeParameters)
                            {
                                dict[sym.Name] = namedTypeSymbol.TypeArguments[index++];
                            }

                        }

                        GetInterfaces(semanticModel, iface, result, dict);
                    }
                }
            }
        }
    }
}