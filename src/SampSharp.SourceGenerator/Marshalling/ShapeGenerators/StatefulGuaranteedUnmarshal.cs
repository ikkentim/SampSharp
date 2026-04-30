using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static SampSharp.SourceGenerator.SyntaxFactories.ExpressionFactory;
using static SampSharp.SourceGenerator.SyntaxFactories.StatementFactory;

namespace SampSharp.SourceGenerator.Marshalling.ShapeGenerators;

public class StatefulGuaranteedUnmarshal(IMarshalShapeGenerator innerGenerator) : IMarshalShapeGenerator
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
            MarshalPhase.Unmarshal => [],
            MarshalPhase.GuaranteedUnmarshal => GuaranteedUnmarshal(context),
            _ => innerGenerator.Generate(phase, context)
        };
    }

    private static IEnumerable<StatementSyntax> GuaranteedUnmarshal(IdentifierStubContext context)
    {
        // managed = marshaller.ToManagedFinally();
        yield return Assign(
            context.GetManagedId(),
            context.PostfixManagedNullableSuppression(
                InvocationExpression(
                    context.GetMarshallerId(),
                    ShapeConstants.MethodToManagedFinally)));
    }
}