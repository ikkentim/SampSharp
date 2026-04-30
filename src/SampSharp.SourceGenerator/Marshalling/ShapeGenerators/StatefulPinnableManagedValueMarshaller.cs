using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace SampSharp.SourceGenerator.Marshalling.ShapeGenerators;

public class StatefulPinnableManagedValueMarshaller(IMarshalShapeGenerator innerGenerator) : IMarshalShapeGenerator
{
    public bool UsesNativeIdentifier => innerGenerator.UsesNativeIdentifier;

    public TypeSyntax GetNativeType(IdentifierStubContext context)
    {
        return innerGenerator.GetNativeType(context);
    }

    public IEnumerable<StatementSyntax> Generate(MarshalPhase phase, IdentifierStubContext context)
    {
        return phase switch
        {
            MarshalPhase.Pin => GeneratePin(context),
            _ => innerGenerator.Generate(phase, context)
        };
    }

    private static IEnumerable<StatementSyntax> GeneratePin(IdentifierStubContext context)
    {
        // fixed(void* __managed_native__unused = managed) {}
        yield return FixedStatement(VariableDeclaration(
                PointerType(
                    PredefinedType(
                        Token(SyntaxKind.VoidKeyword))))
            .WithVariables(
                SingletonSeparatedList(
                    VariableDeclarator(
                            Identifier(context.GetNativeExtraId("unused")))
                        .WithInitializer(
                            EqualsValueClause(
                                IdentifierName(context.GetMarshallerId()))))), Block());
    }
}