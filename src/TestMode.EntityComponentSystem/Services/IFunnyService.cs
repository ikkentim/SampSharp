using SampSharp.EntityComponentSystem.Entities;

namespace TestMode.EntityComponentSystem.Services
{
    public interface IFunnyService
    {
        string MakePlayerNameFunny(Entity player);
    }
}