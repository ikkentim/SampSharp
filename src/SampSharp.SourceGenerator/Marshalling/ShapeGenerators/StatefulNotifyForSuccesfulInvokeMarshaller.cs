using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static SampSharp.SourceGenerator.SyntaxFactories.StatementFactory;

namespace SampSharp.SourceGenerator.Marshalling.ShapeGenerators;

public class StatefulNotifyForSuccesfulInvokeMarshaller(IMarshalShapeGenerator innerGenerator) : IMarshalShapeGenerator
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
            MarshalPhase.NotifyForSuccessfulInvoke => GenerateNotifyForSuccessfulInvoke(context),
            _ => innerGenerator.Generate(phase, context)
        };
    }

    private static IEnumerable<StatementSyntax> GenerateNotifyForSuccessfulInvoke(IdentifierStubContext context)
    {
        // marshaller.OnInvoked();
        yield return Invoke(
            context.GetMarshallerId(), 
            ShapeConstants.MethodOnInvoked);
    }
}