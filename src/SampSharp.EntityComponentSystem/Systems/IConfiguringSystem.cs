namespace SampSharp.EntityComponentSystem.Systems
{
    public interface IConfiguringSystem : ISystem
    {
        void Configure(IEcsBuilder builder);
    }
}