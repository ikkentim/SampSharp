using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static SampSharp.SourceGenerator.SyntaxFactories.ExpressionFactory;
using static SampSharp.SourceGenerator.SyntaxFactories.StatementFactory;

namespace SampSharp.SourceGenerator.Marshalling.ShapeGenerators;

public class StatelessUnmanagedToManaged(IMarshalShapeGenerator innerGenerator) : IMarshalShapeGenerator
{
    public bool UsesNativeIdentifier => true;

    public TypeSyntax GetNativeType(IdentifierStubContext context)
    {
        return context.NativeType!.TypeName;
    }

    public IEnumerable<StatementSyntax> Generate(MarshalPhase phase, IdentifierStubContext context)
    {
        return phase switch
        {
            MarshalPhase.Unmarshal => Unmarshal(context),
            _ => innerGenerator.Generate(phase, context)
        };
    }

    private static IEnumerable<StatementSyntax> Unmarshal(IdentifierStubContext context)
    {
        // managed = Marshaller.ConvertToManaged(unmanaged);
        yield return Assign(
            context.GetManagedId(),
            context.PostfixManagedNullableSuppression(
                InvocationExpression(
                    context.MarshallerType!.TypeName,
                    ShapeConstants.MethodConvertToManaged, 
                    Argument(IdentifierName(context.GetNativeId())))));
    }
}