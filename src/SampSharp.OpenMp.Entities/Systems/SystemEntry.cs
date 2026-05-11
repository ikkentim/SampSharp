namespace SampSharp.Entities;

internal sealed class SystemEntry
{
    public SystemEntry(Type type)
    {
        Type = type;
    }

    public Type Type { get; }
}