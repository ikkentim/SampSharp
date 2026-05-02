using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static SampSharp.SourceGenerator.SyntaxFactories.ExpressionFactory;
using static SampSharp.SourceGenerator.SyntaxFactories.StatementFactory;

namespace SampSharp.SourceGenerator.Marshalling.ShapeGenerators;

public class StatelessManagedToUnmanaged(IMarshalShapeGenerator innerGenerator) : IMarshalShapeGenerator
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
            MarshalPhase.Marshal => Marshal(context),
            _ => innerGenerator.Generate(phase, context)
        };
    }

    private static IEnumerable<StatementSyntax> Marshal(IdentifierStubContext context)
    {
        // native = Marshaller.ConvertToUnmanaged(managed);
        yield return Assign(
            context.GetNativeId(),
            InvocationExpression(
                context.MarshallerType!.TypeName,
                ShapeConstants.MethodConvertToUnmanaged, 
                Argument(IdentifierName(context.GetManagedId()))));
    }
}