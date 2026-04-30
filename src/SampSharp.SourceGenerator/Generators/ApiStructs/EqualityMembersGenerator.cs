using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SampSharp.SourceGenerator.Models;
using SampSharp.SourceGenerator.SyntaxFactories;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace SampSharp.SourceGenerator.Generators.ApiStructs;

public static class EqualityMembersGenerator
{
    public static IEnumerable<MemberDeclarationSyntax> GenerateEqualityMembers(StructStubGenerationContext ctx)
    {
        var self = ParseTypeName(ctx.Symbol.Name);

        if (ctx.Syntax.TypeParameterList != null)
        {
            self = GenericName(
                    Identifier(ctx.Symbol.Name))
                .WithTypeArgumentList(
                    TypeArgumentList(
                        SeparatedList<TypeSyntax>(
                            ctx.Syntax.TypeParameterList.Parameters.Select(x => IdentifierName(x.Identifier)))));
        }

        // public readonly bool HasValue => _handle != default;
        yield return PropertyDeclaration(
                PredefinedType(
                    Token(SyntaxKind.BoolKeyword)),
                Identifier("HasValue"))
            .WithModifiers(
                TokenList(
                    Token(SyntaxKind.PublicKeyword),
                    Token(SyntaxKind.ReadOnlyKeyword)))
            .WithLeadingTrivia(
                TriviaFactory.InheritDoc())
            .WithExpressionBody(
                ArrowExpressionClause(
                    BinaryExpression(
                        SyntaxKind.NotEqualsExpression,
                        IdentifierName("_handle"),
                        LiteralExpression(
                            SyntaxKind.DefaultLiteralExpression,
                            Token(SyntaxKind.DefaultKeyword)))))
            .WithSemicolonToken(
                Token(SyntaxKind.SemicolonToken));

        // public override int GetHashCode()
        yield return MethodDeclaration(
                PredefinedType(
                    Token(SyntaxKind.IntKeyword)),
                Identifier("GetHashCode"))
            .WithModifiers(
                TokenList(
                    Token(SyntaxKind.PublicKeyword),
                    Token(SyntaxKind.OverrideKeyword)
                ))
            .WithLeadingTrivia(
                TriviaFactory.InheritDoc())
            .WithBody(
                Block(
                    SingletonList<StatementSyntax>(
                        ReturnStatement(
                            InvocationExpression(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("_handle"),
                                    IdentifierName("GetHashCode")))))));

        // public bool Equals(MyType other)
        yield return MethodDeclaration(
            PredefinedType(
                Token(SyntaxKind.BoolKeyword)),
            Identifier("Equals"))
            .WithModifiers(
                TokenList(
                    Token(SyntaxKind.PublicKeyword)))
            .WithParameterList(
                    ParameterList(
                    SingletonSeparatedList(
                        Parameter(
                            Identifier("other"))
                        .WithType(ctx.Type))))
            .WithLeadingTrivia(
                TriviaFactory.InheritDoc())
            .WithBody(
                Block(
                    SingletonList<StatementSyntax>(
                        ReturnStatement(
                            BinaryExpression(
                                SyntaxKind.EqualsExpression,
                                IdentifierName("_handle"),
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("other"),
                                    IdentifierName("_handle")))))));

        // public override bool Equals(object? other)
        yield return MethodDeclaration(
                    PredefinedType(
                        Token(SyntaxKind.BoolKeyword)),
                    Identifier("Equals"))
                .WithModifiers(
                    TokenList(
                        Token(SyntaxKind.PublicKeyword), 
                        Token(SyntaxKind.OverrideKeyword)))
                .WithLeadingTrivia(
                    TriviaFactory.InheritDoc())
                .WithParameterList(
                    ParameterList(
                        SingletonSeparatedList(
                            Parameter(
                                Identifier("other"))
                            .WithType(
                                NullableType(
                                    PredefinedType(
                                        Token(SyntaxKind.ObjectKeyword)))))))
                .WithBody(
                    Block(
                        IfStatement(
                            BinaryExpression(
                                SyntaxKind.EqualsExpression,
                                IdentifierName("_handle"),
                                LiteralExpression(
                                    SyntaxKind.DefaultLiteralExpression,
                                    Token(SyntaxKind.DefaultKeyword))),
                            ReturnStatement(
                                BinaryExpression(
                                    SyntaxKind.EqualsExpression,
                                    IdentifierName("other"),
                                    LiteralExpression(
                                        SyntaxKind.NullLiteralExpression)))),
                        IfStatement(
                            BinaryExpression(
                                SyntaxKind.EqualsExpression,
                                IdentifierName("other"),
                                LiteralExpression(
                                    SyntaxKind.NullLiteralExpression)),
                            ReturnStatement(
                                LiteralExpression(
                                    SyntaxKind.FalseLiteralExpression))),
                        IfStatement(
                            IsPatternExpression(
                                IdentifierName("other"),
                                DeclarationPattern(
                                    self,
                                    SingleVariableDesignation(
                                        Identifier("otherValue")))),
                            Block(
                                SingletonList<StatementSyntax>(
                                    ReturnStatement(
                                        InvocationExpression(
                                            IdentifierName("Equals"))
                                        .WithArgumentList(
                                            ArgumentList(
                                                SingletonSeparatedList(
                                                    Argument(
                                                        IdentifierName("otherValue"))))))))),
                        ReturnStatement(
                            LiteralExpression(
                                SyntaxKind.FalseLiteralExpression))));

        // public static bool operator ==(MyType lhs, object? rhs)
        yield return OperatorDeclaration(
                        PredefinedType(
                            Token(SyntaxKind.BoolKeyword)),
                        Token(SyntaxKind.EqualsEqualsToken))
                    .WithModifiers(
                        TokenList(
                            Token(SyntaxKind.PublicKeyword), 
                            Token(SyntaxKind.StaticKeyword)))
                    .WithParameterList(
                        ParameterList(
                            SeparatedList([
                                Parameter(
                                    Identifier("lhs"))
                                .WithType(self),
                                Parameter(
                                    Identifier("rhs"))
                                .WithType(
                                    NullableType(
                                        PredefinedType(
                                            Token(SyntaxKind.ObjectKeyword))))])))
                    .WithLeadingTrivia(
                        TriviaFactory.DocsOpEqual())
                    .WithBody(
                        Block(
                            IfStatement(
                                IsPatternExpression(
                                    IdentifierName("rhs"),
                                    ConstantPattern(
                                        LiteralExpression(
                                            SyntaxKind.NullLiteralExpression))),
                                ReturnStatement(
                                    PrefixUnaryExpression(
                                        SyntaxKind.LogicalNotExpression,
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            IdentifierName("lhs"),
                                            IdentifierName("HasValue"))))),
                            IfStatement(
                                IsPatternExpression(
                                    IdentifierName("rhs"),
                                    DeclarationPattern(
                                        self,
                                        SingleVariableDesignation(
                                            Identifier("other")))),
                                ReturnStatement(
                                    InvocationExpression(
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            IdentifierName("lhs"),
                                            IdentifierName("Equals")))
                                    .WithArgumentList(
                                        ArgumentList(
                                            SingletonSeparatedList(
                                                Argument(
                                                    IdentifierName("other"))))))),
                            ReturnStatement(
                                LiteralExpression(
                                    SyntaxKind.FalseLiteralExpression))));
        
        // public static bool operator !=(MyType lhs, object? rhs)
        yield return OperatorDeclaration(
                        PredefinedType(
                            Token(SyntaxKind.BoolKeyword)),
                        Token(SyntaxKind.ExclamationEqualsToken))
                    .WithModifiers(
                        TokenList(
                            Token(SyntaxKind.PublicKeyword), 
                            Token(SyntaxKind.StaticKeyword)))
                    .WithParameterList(
                        ParameterList(
                            SeparatedList([
                                Parameter(
                                    Identifier("lhs"))
                                .WithType(self),
                                Parameter(
                                    Identifier("rhs"))
                                .WithType(
                                    NullableType(
                                        PredefinedType(
                                            Token(SyntaxKind.ObjectKeyword))))])))
                    .WithLeadingTrivia(
                        TriviaFactory.DocsOpNotEqual())
                    .WithExpressionBody(
                        ArrowExpressionClause(
                            PrefixUnaryExpression(
                                SyntaxKind.LogicalNotExpression,
                                ParenthesizedExpression(
                                    BinaryExpression(
                                        SyntaxKind.EqualsExpression,
                                        IdentifierName("lhs"),
                                        IdentifierName("rhs"))))))
                    .WithSemicolonToken(
                        Token(SyntaxKind.SemicolonToken));
    }
}