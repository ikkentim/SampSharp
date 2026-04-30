namespace SampSharp.SourceGenerator.Marshalling;

/// <summary>
/// Information about a marshaller mode as provided by the CustomMarshallerAttribute on the marshaller entry point.
/// </summary>
/// <param name="ManagedType">The managed type to marshal.</param>
/// <param name="MarshalMode">The marshalling mode this applies to.</param>
/// <param name="MarshallerType">The type used for marshalling.</param>
public record CustomMarshallerInfo(ManagedType ManagedType, MarshalMode MarshalMode, ManagedType MarshallerType)
{
    public bool IsStateful => MarshallerType.Symbol is { IsStatic: false, IsValueType: true };
    public bool IsStateless => MarshallerType.Symbol.IsStatic;

    public bool IsValid => IsStateful || IsStateless;
}