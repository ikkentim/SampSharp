using System;
using Microsoft.CodeAnalysis;

namespace SampSharp.SourceGenerator.Marshalling;

public record WellKnownMarshallerTypes(params (Func<ITypeSymbol, bool> matcher, INamedTypeSymbol? marshaller)[] Marshallers)
{
    public static WellKnownMarshallerTypes Create(Compilation compilation)
    {
        var stringViewMarshaller = compilation.GetTypeByMetadataName(Constants.StringViewMarshallerFQN);
        var booleanMarshaller = compilation.GetTypeByMetadataName(Constants.BooleanMarshallerFQN);

        var wellKnownMarshallerTypes = new WellKnownMarshallerTypes(
            (x => x.SpecialType == SpecialType.System_String, stringViewMarshaller),
            (x => x.SpecialType == SpecialType.System_Boolean, booleanMarshaller)
        );
        return wellKnownMarshallerTypes;
    }
}