using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SampSharp.SourceGenerator.Models;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static SampSharp.SourceGenerator.SyntaxFactories.TriviaFactory;

namespace SampSharp.SourceGenerator.Generators.ApiStructs;

public static class CreationMembersGenerator
{
    /// <summary>
    /// Returns members for the creation of values. This includes the handle field and property, the constructor, and
    /// implicit/explicit conversion operators.
    /// </summary>
    public static IEnumerable<MemberDeclarationSyntax> GenerateCreationMembers(StructStubGenerationContext ctx)
    {
        // private readonly field _handle;
        yield return GenerateHandleField();

        // .ctor(nint handle)
        yield return GenerateConstructor(ctx);

        // public nint Handle => _handle;
        yield return GenerateHandleProperty();
    }

    private static PropertyDeclarationSyntax GenerateHandleProperty()
    {
        return PropertyDeclaration(
                ParseTypeName("nint"),
                "Handle")
            .WithModifiers(
                TokenList(
                    Token(SyntaxKind.PublicKeyword)))
            .WithExpressionBody(
                ArrowExpressionClause(
                    IdentifierName("_handle")))
            .WithSemicolonToken(
                Token(SyntaxKind.SemicolonToken))
            .WithLeadingTrivia(
                InheritDoc());
    }

    private static FieldDeclarationSyntax GenerateHandleField()
    {
        return FieldDeclaration(
                VariableDeclaration(ParseTypeName("nint"),
                    SingletonSeparatedList(
                        VariableDeclarator("_handle"))))
            .WithModifiers(
                TokenList(
                    Token(SyntaxKind.PrivateKeyword), 
                    Token(SyntaxKind.ReadOnlyKeyword)));
    }

    private static ConstructorDeclarationSyntax GenerateConstructor(StructStubGenerationContext ctx)
    {
        return ConstructorDeclaration(Identifier(ctx.Symbol.Name))
            .WithParameterList(ParameterList(
                SingletonSeparatedList(
                    Parameter(Identifier("handle")).WithType(ParseTypeName("nint"))
                )))
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .WithLeadingTrivia(
                DocsStructConstructor(
                    ctx.Type, 
                    new ParameterDoc(
                        "handle",
                        "A pointer to the unmanaged interface.")))
            .WithBody(Block(
                SingletonList(
                    ExpressionStatement(
                        AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression, 
                            IdentifierName("_handle"),
                            IdentifierName("handle"))))));
    }
}
