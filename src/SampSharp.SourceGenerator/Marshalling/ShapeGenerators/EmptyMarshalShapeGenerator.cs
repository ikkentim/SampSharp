using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SampSharp.SourceGenerator.Marshalling.ShapeGenerators;

public class EmptyMarshalShapeGenerator : IMarshalShapeGenerator
{
    private EmptyMarshalShapeGenerator()
    {
    }

    public static IMarshalShapeGenerator Instance { get; } = new EmptyMarshalShapeGenerator();

    public bool UsesNativeIdentifier => false;

    public TypeSyntax GetNativeType(IdentifierStubContext context)
    {
        return context.ManagedType.TypeName;
    }

    public IEnumerable<StatementSyntax> Generate(MarshalPhase phase, IdentifierStubContext context)
    {
        return [];
    }
}