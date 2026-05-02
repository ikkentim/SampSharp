using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SampSharp.SourceGenerator.Helpers;
using SampSharp.SourceGenerator.Models;
using SampSharp.SourceGenerator.SyntaxFactories;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static SampSharp.SourceGenerator.SyntaxFactories.TypeSyntaxFactory;

namespace SampSharp.SourceGenerator.Generators.ApiStructs;

public static class CastMembersGenerator
{
    public static IEnumerable<MemberDeclarationSyntax> GenerateCastMembers(StructStubGenerationContext ctx)
    {
        var isFirst = true;
        var generatedFromComponentHandle = false;

        foreach (var impl in ctx.ImplementingTypes)
        {
            var block = CreateCastBlock(ctx, isFirst, impl);

            yield return ConversionOperatorDeclaration(
                    Token(SyntaxKind.ExplicitKeyword),
                    impl.Type.Syntax)
                .WithModifiers(
                    TokenList(
                        Token(SyntaxKind.PublicKeyword), 
                        Token(SyntaxKind.StaticKeyword)))
                .WithParameterList(
                    ParameterList(
                        SingletonSeparatedList(
                            Parameter(
                                    Identifier("value"))
                                .WithType(
                                    ctx.Type))))
                .WithLeadingTrivia(
                    TriviaFactory.DocsOpCast(ctx.Type, impl.Type.Syntax))
                .WithBody(block);

            // Generate reverse cast (impl.Type -> ctx.Type)
            var reverseBlock = CreateReverseCastBlock(ctx, isFirst, impl);
            yield return ConversionOperatorDeclaration(
                    Token(SyntaxKind.ExplicitKeyword),
                    ctx.Type)
                .WithModifiers(
                    TokenList(
                        Token(SyntaxKind.PublicKeyword), 
                        Token(SyntaxKind.StaticKeyword)))
                .WithParameterList(
                    ParameterList(
                        SingletonSeparatedList(
                            Parameter(
                                    Identifier("value"))
                                .WithType(
                                    impl.Type.Syntax))))
                .WithLeadingTrivia(
                    TriviaFactory.DocsOpCast(impl.Type.Syntax, ctx.Type))
                .WithBody(reverseBlock);

            isFirst = false;

            if (!generatedFromComponentHandle && impl.Type.Symbol.IsSame(Constants.IComponentFQN))
            {
                yield return GenerateFromComponentHandle(ctx);
                generatedFromComponentHandle = true;
            }
        }
    }

    private static BlockSyntax CreateCastBlock(StructStubGenerationContext ctx, bool isFirst, ImplementingType impl)
    {
        BlockSyntax block;

        if (isFirst)
        {
            block = Block(SingletonList<StatementSyntax>(
                ReturnStatement(
                    ObjectCreationExpression(impl.Type.Syntax)
                        .WithArgumentList(
                            ArgumentList(
                                SingletonSeparatedList(
                                    Argument(
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            IdentifierName("value"),
                                            IdentifierName("Handle")))))))));
        }
        else
        {
            if (impl.CastPath.Length == 1)
            {
                var func = GenerateExternFunctionCast(ctx, impl.Type.Symbol);

                var invoke = InvocationExpression(
                        IdentifierName("__PInvoke"))
                    .WithArgumentList(
                        ArgumentList(
                            SingletonSeparatedList(
                                Argument(
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName("value"),
                                        IdentifierName("Handle"))))));

                var ret = ReturnStatement(
                    ObjectCreationExpression(impl.Type.Syntax)
                        .WithArgumentList(
                            ArgumentList(
                                SingletonSeparatedList(
                                    Argument(invoke)))));

                block = Block(
                    List<StatementSyntax>([
                        ret,
                        func
                    ]));
            }
            else
            {
                var cast = impl.CastPath.Aggregate((ExpressionSyntax)IdentifierName("value"), (current, c) => CastExpression(c.Syntax, current));

                block = Block(SingletonList<StatementSyntax>(
                    ReturnStatement(cast)));
            }
        }

        return block;
    }

    private static BlockSyntax CreateReverseCastBlock(StructStubGenerationContext ctx, bool isFirst, ImplementingType impl)
    {
        BlockSyntax block;

        if (isFirst)
        {
            // First implementing type: direct handle access
            block = Block(SingletonList<StatementSyntax>(
                ReturnStatement(
                    ObjectCreationExpression(ctx.Type)
                        .WithArgumentList(
                            ArgumentList(
                                SingletonSeparatedList(
                                    Argument(
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            IdentifierName("value"),
                                            IdentifierName("Handle")))))))));
        }
        else
        {
            if (impl.CastPath.Length == 1)
            {
                // Use extern function for the reverse cast
                var func = GenerateExternFunctionCastReverse(ctx, impl.Type.Symbol);

                var invoke = InvocationExpression(
                        IdentifierName("__PInvoke"))
                    .WithArgumentList(
                        ArgumentList(
                            SingletonSeparatedList(
                                Argument(
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName("value"),
                                        IdentifierName("Handle"))))));

                var ret = ReturnStatement(
                    ObjectCreationExpression(ctx.Type)
                        .WithArgumentList(
                            ArgumentList(
                                SingletonSeparatedList(
                                    Argument(invoke)))));

                block = Block(
                    List<StatementSyntax>([
                        ret,
                        func
                    ]));
            }
            else
            {
                // For multi-step cast paths, apply the reverse cast chain directly
                var cast = impl.CastPath.Reverse().Aggregate(
                    (ExpressionSyntax)IdentifierName("value"), 
                    (current, c) => CastExpression(c.Syntax, current));

                // Wrap the result in a cast to ctx.Type
                var finalCast = CastExpression(ctx.Type, cast);

                block = Block(SingletonList<StatementSyntax>(
                    ReturnStatement(finalCast)));
            }
        }

        return block;
    }

    private static MethodDeclarationSyntax GenerateFromComponentHandle(StructStubGenerationContext ctx)
    {
        return MethodDeclaration(
                ParseName("nint"), 
                "FromComponentHandle")
            .WithModifiers(
                TokenList(
                    Token(SyntaxKind.PublicKeyword),
                    Token(SyntaxKind.StaticKeyword)))
            .WithParameterList(
                ParameterList(
                    SingletonSeparatedList(
                        Parameter(
                                Identifier("handle"))
                            .WithType(
                                ParseName("nint")))))
            .WithLeadingTrivia(
                TriviaFactory.InheritDoc())
            .WithBody(
                Block(
                    SingletonList<StatementSyntax>(
                        ReturnStatement(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                ParenthesizedExpression(
                                    CastExpression(
                                        ctx.Type,
                                        ObjectCreationExpression(
                                                TypeNameGlobal(Constants.IComponentFQN))
                                            .WithArgumentList(
                                                ArgumentList(
                                                    SingletonSeparatedList(
                                                        Argument(
                                                            ParseName("handle"))))))), 
                                IdentifierName("Handle"))))));
    }


    private static LocalFunctionStatementSyntax GenerateExternFunctionCast(StructStubGenerationContext ctx, ITypeSymbol target)
    {
        return HelperSyntaxFactory.GenerateExternFunction(
            library: ctx.Library, 
            externName: $"cast_{ctx.Symbol.Name}_to_{target.Name}",
            externReturnType: IntPtrType, 
            parameters: [new HelperSyntaxFactory.ParamForwardInfo("ptr", IntPtrType, RefKind.None)]);
    }

    private static LocalFunctionStatementSyntax GenerateExternFunctionCastReverse(StructStubGenerationContext ctx, ITypeSymbol source)
    {
        return HelperSyntaxFactory.GenerateExternFunction(
            library: ctx.Library, 
            externName: $"cast_{source.Name}_to_{ctx.Symbol.Name}",
            externReturnType: IntPtrType, 
            parameters: [new HelperSyntaxFactory.ParamForwardInfo("ptr", IntPtrType, RefKind.None)]);
    }
}