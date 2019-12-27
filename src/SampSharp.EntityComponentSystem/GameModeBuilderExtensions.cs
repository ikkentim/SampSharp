using SampSharp.Core;

namespace SampSharp.EntityComponentSystem
{
    public static class GameModeBuilderExtensions
    {
        public static GameModeBuilder UseEcs<TStartup>(this GameModeBuilder gameModeBuilder) where TStartup : class, IStartup, new()
        {
            return gameModeBuilder.Use(new EcsManager(new TStartup()));
        }
    }
}