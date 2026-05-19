namespace SampSharp.Entities;

internal interface ISafeComponentHandle
{
    nint Handle { get; }
    void Free();
}