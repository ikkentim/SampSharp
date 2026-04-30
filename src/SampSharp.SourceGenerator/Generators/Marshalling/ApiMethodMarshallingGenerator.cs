using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SampSharp.SourceGenerator.Helpers;
using SampSharp.SourceGenerator.Marshalling;
using SampSharp.SourceGenerator.Models;
using SampSharp.SourceGenerator.SyntaxFactories;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace SampSharp.SourceGenerator.Generators.Marshalling;

public class ApiMethodMarshallingGenerator() : MarshallingGeneratorBase(MarshalDirection.ManagedToUnmanaged)
{
    private const string MethodPInvoke = "__PInvoke";
    private const string FieldHandle = "_handle";

    public MemberDeclarationSyntax GenerateNativeMethod(ApiMethodStubGenerationContext ctx)
    {
        return MethodDeclaration(TypeSyntaxFactory.TypeNameGlobal(ctx.Symbol), ctx.Declaration.Identifier)
            .WithModifiers(ctx.Declaration.Modifiers)
            .WithParameterList(HelperSyntaxFactory.ToParameterListSyntax(ctx.Symbol.Parameters))
            .WithBody(GetMarshallingBlock(ctx));
    }

    protected override BlockSyntax GetMarshallingBlock(MarshallingStubGenerationContext ctx)
    {
        var block = base.GetMarshallingBlock(ctx);

        block = block.WithStatements(
            block.Statements.Add(
                GenerateExternFunction((ApiMethodStubGenerationContext)ctx)));

        return block;
    }

    protected override ExpressionSyntax GetInvocation(MarshallingStubGenerationContext ctx)
    {
        return InvocationExpression(IdentifierName(MethodPInvoke))
            .WithArgumentList(
                ArgumentList(
                    SingletonSeparatedList(Argument(IdentifierName(FieldHandle)))
                        .AddRange(
                            GetInvocationArguments(ctx))
                )
            );
    }

    private static LocalFunctionStatementSyntax GenerateExternFunction(ApiMethodStubGenerationContext ctx)
    {
        // Extern P/Invoke

        var externReturnType = ctx.ReturnValue.Generator.GetNativeType(ctx.ReturnValue);

        if(ctx.ReturnsByRef)
        {
            externReturnType = ctx.RequiresMarshalling
                ? PointerType(externReturnType)
                : RefType(externReturnType);
        }

        
        var handleParam = Parameter(Identifier("handle_")).WithType(TypeSyntaxFactory.IntPtrType);

        return HelperSyntaxFactory.GenerateExternFunction(
            library: ctx.Library, 
            externName: ToExternName(ctx),
            externReturnType: externReturnType, 
            parameters: ctx.Parameters.Select(x => HelperSyntaxFactory.ToForwardInfo(x, true)), 
            parametersPrefix: handleParam);
    }

    private static string ToExternName(ApiMethodStubGenerationContext ctx)
    {
        var overload = ctx.Symbol.GetAttribute(Constants.OverloadAttributeFQN)?.ConstructorArguments[0].Value as string;

        return ctx.Symbol.GetAttribute(Constants.FunctionAttributeFQN)?.ConstructorArguments[0].Value is string functionName 
            ? $"{ctx.NativeTypeName}_{functionName}" 
            : $"{ctx.NativeTypeName}_{StringUtil.FirstCharToLower(ctx.Symbol.Name)}{overload}";
    }
}