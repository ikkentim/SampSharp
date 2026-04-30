using Microsoft.CodeAnalysis;

namespace SampSharp.SourceGenerator.Marshalling;

public class IdentifierStubContextFactory
{    
    //
    // stages:
    // during context building:
    // 1. Decide which marshaller implementation to use based on entry point of the specified custom marshaller ( CustomMarshallerTypeFinder )
    // 2. Deduce shape based on the implementation ( ShapeTool )
    // 3. Activate ShapeGenerator based on shape (CustomMarshalGeneratorFactory.Create)
    // during generation:
    // 4. Generate marshaling code and combine with invocation code
    //

    private readonly CustomMarshallerTypeDetector _customMarshallerTypeDetector;

    public IdentifierStubContextFactory(WellKnownMarshallerTypes wellKnownMarshallerTypes)
    {
        _customMarshallerTypeDetector = new CustomMarshallerTypeDetector(wellKnownMarshallerTypes);
    }

    public IdentifierStubContext Create(IParameterSymbol parameter, MarshalDirection marshalDirection)
    {
        return Create(parameter, parameter.Type, marshalDirection);
    }

    public IdentifierStubContext Create(IParameterSymbol parameter, ITypeSymbol type, MarshalDirection marshalDirection)
    {
        var customMarshaller = _customMarshallerTypeDetector.GetCustomMarshaller(parameter, type, marshalDirection);
        var (shape, nativeType) = customMarshaller == null 
            ? (MarshallerShape.None, new ManagedType(type))
            : ShapeDetector.GetShapeOfMarshaller(customMarshaller);

        var generator = CustomMarshalGeneratorFactory.Create(shape, customMarshaller?.IsStateful);

        return new IdentifierStubContext(marshalDirection, new ManagedType(type), customMarshaller?.MarshallerType, nativeType, parameter.NullableAnnotation, shape, generator, parameter.RefKind, parameter.Name);
    }

    public IdentifierStubContext Create(IMethodSymbol method, MarshalDirection marshalDirection)
    {
        return Create(method, method.ReturnType, marshalDirection);
    }

    public IdentifierStubContext Create(IMethodSymbol method, ITypeSymbol type, MarshalDirection marshalDirection)
    {
        var customMarshaller = _customMarshallerTypeDetector.GetCustomMarshaller(method, type, marshalDirection);
        var (shape, nativeType) = customMarshaller == null 
            ? (MarshallerShape.None, new ManagedType(type)) 
            : ShapeDetector.GetShapeOfMarshaller(customMarshaller);

        var generator = CustomMarshalGeneratorFactory.Create(shape, customMarshaller?.IsStateful);

        // will have been annotated by the MarshallingGeneratorBase
        var annotation = method.ReturnType.IsReferenceType 
            ? NullableAnnotation.Annotated
            : NullableAnnotation.NotAnnotated;

        return new IdentifierStubContext(marshalDirection, new ManagedType(type), customMarshaller?.MarshallerType, nativeType, annotation, shape, generator, RefKind.Out, MarshallerConstants.LocalReturnValue);
    }
}