using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SampSharp.SourceGenerator.Marshalling;

public record IdentifierStubContext(
    MarshalDirection Direction,
    ManagedType ManagedType,
    ManagedType? MarshallerType, 
    ManagedType? NativeType,
    NullableAnnotation ManagedTypeNullableAnnotation,
    MarshallerShape Shape,
    IMarshalShapeGenerator Generator,
    RefKind RefKind,
    string ManagedIdentifier)
{
    public string GetManagedId()
    {
        return ManagedIdentifier;
    }

    public string GetMarshallerId()
    {
        return GetNativeExtraId("marshaller");
    }

    public string GetNativeId()
    {
        return ManagedIdentifier == MarshallerConstants.LocalReturnValue 
            ? $"{MarshallerConstants.LocalReturnValue}_native" 
            : $"__{ManagedIdentifier}_native";
    }

    public string GetNativeExtraId(string extra)
    {
        return $"{GetNativeId()}__{extra}";
    }

    public ExpressionSyntax PostfixManagedNullableSuppression(ExpressionSyntax expr)
    {
        if (ManagedTypeNullableAnnotation == NullableAnnotation.NotAnnotated)
        {
            expr = SyntaxFactory.PostfixUnaryExpression(SyntaxKind.SuppressNullableWarningExpression, expr);
        }

        return expr;
    }
}