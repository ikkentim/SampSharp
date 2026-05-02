using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace SampSharp.SourceGenerator.Marshalling.ShapeGenerators;

public class StaticPinnableManagedValueMarshaller(IMarshalShapeGenerator innerGenerator) : IMarshalShapeGenerator
{
    public bool UsesNativeIdentifier => false;

    public TypeSyntax GetNativeType(IdentifierStubContext context)
    {
        return PointerType(PredefinedType(Token(SyntaxKind.VoidKeyword)));
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
        // fixed(void* __managed_native = managed.GetPinnableReference()) {}
        yield return FixedStatement(VariableDeclaration(
                PointerType(
                    PredefinedType(
                        Token(SyntaxKind.VoidKeyword))))
            .WithVariables(
                SingletonSeparatedList(
                    VariableDeclarator(
                            Identifier(context.GetNativeId()))
                        .WithInitializer(
                            EqualsValueClause(
                                PrefixUnaryExpression(
                                    SyntaxKind.AddressOfExpression,
                                    InvocationExpression(
                                            MemberAccessExpression(
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                context.MarshallerType!.TypeName,
                                                IdentifierName(ShapeConstants.MethodGetPinnableReference)))
                                        .WithArgumentList(
                                            ArgumentList(
                                                SingletonSeparatedList(
                                                    Argument(
                                                        IdentifierName(context.GetManagedId())))))))))), Block());
    }
}