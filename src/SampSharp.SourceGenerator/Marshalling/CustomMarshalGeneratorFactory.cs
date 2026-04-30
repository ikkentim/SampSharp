using SampSharp.SourceGenerator.Marshalling.ShapeGenerators;

namespace SampSharp.SourceGenerator.Marshalling;

public static class CustomMarshalGeneratorFactory
{
    public static IMarshalShapeGenerator Create(MarshallerShape shape, bool? stateful)
    {
        return !stateful.HasValue || shape == MarshallerShape.None // fast path
            ? EmptyMarshalShapeGenerator.Instance
            : GetFactory(stateful.Value).Create(shape);
    }

    private static ICustomMarshalGeneratorFactoryStrategy GetFactory(bool stateful)
    {
        return stateful
            ? StatefulCustomMarshalGeneratorFactoryStrategy.Instance
            : StatelessCustomMarshalGeneratorFactoryStrategy.Instance;
    }
}