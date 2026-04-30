using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SampSharp.SourceGenerator.Marshalling;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static SampSharp.SourceGenerator.SyntaxFactories.TypeSyntaxFactory;

namespace SampSharp.SourceGenerator.SyntaxFactories;

/// <summary>
/// Creates SampSharp specific syntax nodes.
/// </summary>
public static class HelperSyntaxFactory
{
    public static LocalFunctionStatementSyntax GenerateExternFunction(
        string library,
        string externName,
        TypeSyntax externReturnType, 
        IEnumerable<ParamForwardInfo> parameters, 
        params ParameterSyntax[] parametersPrefix)
    {
        var externParameters = ToParameterListSyntax(parametersPrefix, parameters, true);

        return LocalFunctionStatement(externReturnType, "__PInvoke")
            .WithModifiers(TokenList(          
                Token(SyntaxKind.StaticKeyword),
                Token(SyntaxKind.UnsafeKeyword),
                Token(SyntaxKind.ExternKeyword)
            ))
            .WithParameterList(externParameters)
            .WithAttributeLists(
                SingletonList(
                    AttributeFactory.DllImport(library, externName)))
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
            .WithLeadingTrivia(Comment(MarshallingCodeGenDocumentation.COMMENT_P_INVOKE));
    }
    
    public static ParameterListSyntax ToParameterListSyntax(ImmutableArray<IParameterSymbol> parameters)
    {
        return ToParameterListSyntax([], parameters.Select(x => new ParamForwardInfo(x.Name, TypeNameGlobal(x.Type), x.RefKind)));
    }

    public static ParameterListSyntax ToParameterListSyntax(ParameterSyntax[] prefix, IEnumerable<ParamForwardInfo> parameters, bool removeIn = false)
    {
        return ParameterList(
            SeparatedList(prefix)
                .AddRange(
                    parameters
                        .Select(parameter => Parameter(Identifier(parameter.Name))
                            .WithType(parameter.Type)
                            .WithModifiers(GetRefTokens(parameter.RefKind, removeIn)))));
    }

    public static ParamForwardInfo ToForwardInfo(IdentifierStubContext context, bool toExtern = false)
    {
        var paramType = context.Generator.GetNativeType(context);
        var refKind = context.RefKind;

        if (toExtern && refKind is RefKind.In or RefKind.RefReadOnlyParameter)
        {
            paramType = PointerType(paramType);
            refKind = RefKind.None;
        }

        var name = context.Generator.UsesNativeIdentifier && 
                   context.Direction == MarshalDirection.UnmanagedToManaged 
            ? context.GetNativeId() 
            : context.GetManagedId();

        return new ParamForwardInfo(name, paramType, refKind);
    }

    private static SyntaxTokenList GetRefTokens(RefKind refKind, bool removeIn)
    {
        return refKind switch
        {
            RefKind.Ref => TokenList(Token(SyntaxKind.RefKeyword)),
            RefKind.RefReadOnlyParameter => TokenList(Token(SyntaxKind.RefKeyword), Token(SyntaxKind.ReadOnlyKeyword)),
            RefKind.Out => TokenList(Token(SyntaxKind.OutKeyword)),
            RefKind.In => removeIn ? default : TokenList(Token(SyntaxKind.InKeyword)),
            _ => default
        };
    }

    public static ArgumentSyntax WithPInvokeParameterRefToken(ArgumentSyntax argument, RefKind refKind)
    {
        switch (refKind)
        {
            case RefKind.Ref:
                return argument.WithRefKindKeyword(Token(SyntaxKind.RefKeyword));
            case RefKind.Out:
                return argument.WithRefKindKeyword(Token(SyntaxKind.OutKeyword));
            default:
                return argument;
        }
    }

    public record struct ParamForwardInfo(string Name, TypeSyntax Type, RefKind RefKind);
}