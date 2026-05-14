namespace SampSharp.Entities;

internal sealed class SystemEntry(Type type)
{
    public Type Type { get; } = type;
}