using System.Linq;
using Microsoft.CodeAnalysis;
using SampSharp.SourceGenerator.Marshalling;

namespace SampSharp.SourceGenerator.Models;

public record MarshallingStubGenerationContext(
    IMethodSymbol Symbol,
    IdentifierStubContext[] Parameters,
    IdentifierStubContext ReturnValue)
{
    public bool ReturnsByRef => Symbol.ReturnsByRef || Symbol.ReturnsByRefReadonly;
    public bool RequiresMarshalling { get; } = ReturnValue.Shape != MarshallerShape.None || Parameters.Any(p => p.Shape != MarshallerShape.None);
}