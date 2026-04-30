using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static SampSharp.SourceGenerator.SyntaxFactories.ExpressionFactory;
using static SampSharp.SourceGenerator.SyntaxFactories.StatementFactory;

namespace SampSharp.SourceGenerator.Marshalling.ShapeGenerators;

public class StatefulUnmanagedToManaged(IMarshalShapeGenerator innerGenerator) : IMarshalShapeGenerator
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
            MarshalPhase.Setup => Setup(context),
            MarshalPhase.UnmarshalCapture => UnmarshalCapture(context),
            MarshalPhase.Unmarshal => Unmarshal(context),
            MarshalPhase.CleanupCalleeAllocated => CleanupCalleeAllocated(context),
            _ => innerGenerator.Generate(phase, context)
        };
    }

    private static IEnumerable<StatementSyntax> Setup(IdentifierStubContext context)
    {
        // scoped type marshaller = new();
        var local = DeclareLocal(
            context.MarshallerType!.TypeName,
            context.GetMarshallerId(),
            ImplicitObjectCreationExpression());

        if (context.MarshallerType.Symbol.IsRefLikeType)
        {
            local = local.WithModifiers(
                TokenList(
                    Token(
                        SyntaxKind.ScopedKeyword)));

        }

        yield return local;
    }

    private static IEnumerable<StatementSyntax> CleanupCalleeAllocated(IdentifierStubContext context)
    {
        // marshaller.Free();
        yield return Invoke(
            context.GetMarshallerId(),
            ShapeConstants.MethodFree);
    }

    private static IEnumerable<StatementSyntax> Unmarshal(IdentifierStubContext context)
    {
        // managed = marshaller.ToManaged();
        yield return Assign(
            context.GetManagedId(),
            context.PostfixManagedNullableSuppression(
                InvocationExpression(
                    context.GetMarshallerId(), 
                    ShapeConstants.MethodToManaged)));
    }

    private static IEnumerable<StatementSyntax> UnmarshalCapture(IdentifierStubContext context)
    {
        // marshaller.FromUnmanaged(unmanaged);
        yield return Invoke(
            context.GetMarshallerId(),
            ShapeConstants.MethodFromUnmanaged,
            Argument(
                IdentifierName(context.GetNativeId())));
    }
}