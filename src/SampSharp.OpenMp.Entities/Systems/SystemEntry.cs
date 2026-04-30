namespace SampSharp.Entities;

internal class SystemEntry
{
    public SystemEntry(Type type)
    {
        Type = type;
    }

    public Type Type { get; }
}