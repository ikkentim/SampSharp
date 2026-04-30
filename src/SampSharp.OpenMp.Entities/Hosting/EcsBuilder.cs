namespace SampSharp.Entities;

internal class EcsBuilder(IServiceProvider services) : IEcsBuilder
{
    public IServiceProvider Services { get; } = services;
}