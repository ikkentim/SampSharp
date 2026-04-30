using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SampSharp.SourceGenerator.Marshalling;

public interface IMarshalShapeGenerator
{
    bool UsesNativeIdentifier { get; }

    TypeSyntax GetNativeType(IdentifierStubContext context);

    public IEnumerable<StatementSyntax> Generate(MarshalPhase phase, IdentifierStubContext context);
}