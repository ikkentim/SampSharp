using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SampSharp.SourceGenerator.Helpers;
using SampSharp.SourceGenerator.Models;
using SampSharp.SourceGenerator.SyntaxFactories;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace SampSharp.SourceGenerator.Generators.ApiStructs;

public static class InterfaceMemberGenerator
{
    public const string InterfaceName = "IManagedInterface";

    public static IEnumerable<MemberDeclarationSyntax> GenerateNativeMethods(StructStubGenerationContext ctx)
    {
        var publicMembers = ctx.PublicMembers
            .Where(x => !x.HasModifier(SyntaxKind.StaticKeyword))
            .Where(x => x is MethodDeclarationSyntax)
            .Select(MemberDeclarationSyntax (member) =>
            {
                if (member is MethodDeclarationSyntax method)
                {
                    method = method.WithBody(null).WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
                    member = method.WithParameterList(ParameterList(SeparatedList(method.ParameterList.Parameters.Select(x => x.WithAttributeLists([])))));
                }

                var trivia = member.GetLeadingTrivia();

                var modifiers = member.Modifiers;
                var partialIdx = modifiers.IndexOf(SyntaxKind.PartialKeyword);

                if (partialIdx >= 0)
                {
                    modifiers = modifiers.RemoveAt(partialIdx);
                }

                member = member
                    .WithAttributeLists([])
                    .WithModifiers(modifiers)
                    .WithLeadingTrivia(trivia);

                return member;
            })
            .ToList();


        yield return InterfaceDeclaration(InterfaceName)
            .WithModifiers(TokenList(
                Token(SyntaxKind.PublicKeyword),
                Token(SyntaxKind.PartialKeyword)
            ))
            .WithMembers(List(publicMembers))
            .WithBaseList(BaseList(
                SingletonSeparatedList<BaseTypeSyntax>(
                    SimpleBaseType(
                        TypeSyntaxFactory.TypeNameGlobal(Constants.UnmanagedInterfaceFQN)))))
            .WithLeadingTrivia(
                TriviaFactory.Docs("Represents the managed interface implemented by its unmanaged counterpart.", []));
    }
}

