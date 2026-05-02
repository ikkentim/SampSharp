using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static SampSharp.SourceGenerator.SyntaxFactories.ExpressionFactory;
using static SampSharp.SourceGenerator.SyntaxFactories.StatementFactory;

namespace SampSharp.SourceGenerator.Marshalling.ShapeGenerators;

public class StatefulManagedToUnmanaged(IMarshalShapeGenerator innerGenerator) : IMarshalShapeGenerator
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
            MarshalPhase.Marshal => Marshal(context),
            MarshalPhase.PinnedMarshal => PinnedMarshal(context),
            _ => innerGenerator.Generate(phase, context)
        };
    }

    private static IEnumerable<StatementSyntax> PinnedMarshal(IdentifierStubContext context)
    {
        // native = marshaller.ToUnmanaged();
        yield return Assign(
            context.GetNativeId(),
            InvocationExpression(
                context.GetMarshallerId(), 
                ShapeConstants.MethodToUnmanaged)
            );
    }

    private static IEnumerable<StatementSyntax> Marshal(IdentifierStubContext context)
    {
        // marshaller.FromManaged(managed);
        yield return Invoke(
            context.GetMarshallerId(), 
            ShapeConstants.MethodFromManaged,
            Argument(
                IdentifierName(
                    context.GetManagedId())));
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
}