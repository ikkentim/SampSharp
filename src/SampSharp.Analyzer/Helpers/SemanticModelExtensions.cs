using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SampSharp.Analyzer.Helpers;

public static class SemanticModelExtensions
{
    public static bool HasAttribute(this SemanticModel semanticModel, BaseTypeDeclarationSyntax declaration, INamedTypeSymbol attributeType)
    {
        foreach (var attributeList in declaration.AttributeLists)
        {
            foreach (var attribute in attributeList.Attributes)
            {

                var symbol = semanticModel.GetTypeInfo(attribute).Type;
                if (symbol == null)
                {
                    continue;
                }

                if (SymbolEqualityComparer.Default.Equals(symbol, attributeType))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static bool IsAttribute(this SemanticModel semanticModel, AttributeSyntax attribute, INamedTypeSymbol attributeType)
    {
        var symbol = semanticModel.GetTypeInfo(attribute).Type;
        return symbol != null && SymbolEqualityComparer.Default.Equals(symbol, attributeType);
    }

    public static bool HasAttribute(this SemanticModel semanticModel, TypeSyntax type, INamedTypeSymbol attributeType)
    {
        return semanticModel.GetSymbolInfo(type).Symbol is INamedTypeSymbol sym &&
               sym.GetAttributes().Any(x => SymbolEqualityComparer.Default.Equals(x.AttributeClass, attributeType));
    }

    public static bool HasAttribute(this SemanticModel semanticModel, AttributeListSyntax attributeList, INamedTypeSymbol attributeType)
    {
        return attributeList.Attributes.Any(x => SymbolEqualityComparer.Default.Equals(semanticModel.GetTypeInfo(x).Type, attributeType));
    }

    public static bool IsBaseType(this SemanticModel semanticModel, ClassDeclarationSyntax classDeclaration, INamedTypeSymbol baseType)
    {
        return classDeclaration.BaseList?.Types
            .Select(baseTypeSyntax => semanticModel.GetTypeInfo(baseTypeSyntax.Type).Type)
            .Any(baseTypeSymbol => SymbolEqualityComparer.Default.Equals(baseTypeSymbol, baseType)) ?? false;
    }
}