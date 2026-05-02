using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SampSharp.SourceGenerator.SyntaxFactories;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static SampSharp.SourceGenerator.SyntaxFactories.StatementFactory;

namespace SampSharp.SourceGenerator.Marshalling.ShapeGenerators;

public class CallerAllocatedBuffer(IMarshalShapeGenerator innerGenerator) : IMarshalShapeGenerator
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
            MarshalPhase.Marshal => GenerateMarshal(context),
            _ => innerGenerator.Generate(phase, context)
        };
    }

    private IEnumerable<StatementSyntax> GenerateMarshal(IdentifierStubContext context)
    {
        var bufferVar = context.GetNativeExtraId("buffer");

        // global::System.Span<byte> __varName_native__buffer = stackalloc byte[MarshallerType.BufferSize];
        yield return DeclareLocal(
            TypeSyntaxFactory.SpanOfBytes,
            bufferVar,
            StackAllocArrayCreationExpression(
                ArrayType(
                        PredefinedType(
                            Token(SyntaxKind.ByteKeyword)))
                    .WithRankSpecifiers(
                        SingletonList(
                            ArrayRankSpecifier(SingletonSeparatedList<ExpressionSyntax>(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    context.MarshallerType!.TypeName,
                                    IdentifierName(ShapeConstants.PropertyBufferSize))))))));

        // append argument to the method call
        var rewriter = new InvocationRewriter(bufferVar);
        foreach (var el in innerGenerator.Generate(MarshalPhase.Marshal, context))
        {
            yield return (StatementSyntax)rewriter.Visit(el);
        }
    }

    private class InvocationRewriter(string bufferName) : CSharpSyntaxRewriter
    {
        public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            return node.WithArgumentList(
                node.ArgumentList.AddArguments(
                    Argument(
                        IdentifierName(bufferName))));
        }
    }
}