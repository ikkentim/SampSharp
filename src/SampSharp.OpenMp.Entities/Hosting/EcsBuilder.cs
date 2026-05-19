namespace SampSharp.Entities;

internal sealed class EcsBuilder(IServiceProvider services) : IEcsBuilder
{
    public IServiceProvider Services { get; } = services;
}