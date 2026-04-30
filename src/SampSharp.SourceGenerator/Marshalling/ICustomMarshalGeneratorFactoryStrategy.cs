namespace SampSharp.SourceGenerator.Marshalling;

public interface ICustomMarshalGeneratorFactoryStrategy
{
    IMarshalShapeGenerator Create(MarshallerShape shape);
}