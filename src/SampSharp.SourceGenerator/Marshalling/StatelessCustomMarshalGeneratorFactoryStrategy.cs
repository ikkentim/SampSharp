using SampSharp.SourceGenerator.Marshalling.ShapeGenerators;

namespace SampSharp.SourceGenerator.Marshalling;

public class StatelessCustomMarshalGeneratorFactoryStrategy : ICustomMarshalGeneratorFactoryStrategy
{
    public static ICustomMarshalGeneratorFactoryStrategy Instance { get; } = new StatelessCustomMarshalGeneratorFactoryStrategy();

    public IMarshalShapeGenerator Create(MarshallerShape shape)
    {
        var generator = EmptyMarshalShapeGenerator.Instance;

        if (shape.HasFlag(MarshallerShape.StatelessPinnableReference))
        {
            return new StaticPinnableManagedValueMarshaller(generator);
        }

        if (shape.HasFlag(MarshallerShape.GuaranteedUnmarshal))
        {
            generator = new StatelessUnmanagedToManagedGuaranteed(generator);
        }
        else if (shape.HasFlag(MarshallerShape.ToUnmanaged))
        {
            generator = new StatelessManagedToUnmanaged(generator);
        }

        if (shape.HasFlag(MarshallerShape.ToManaged))
        {
            generator = new StatelessUnmanagedToManaged(generator);
        }

        if (shape.HasFlag(MarshallerShape.CallerAllocatedBuffer))
        {
            generator = new CallerAllocatedBuffer(generator);
        }

        if (shape.HasFlag(MarshallerShape.Free))
        {
            generator = new StatelessFree(generator);
        }

        return generator;
    }
}