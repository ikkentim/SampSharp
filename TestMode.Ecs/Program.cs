using SampSharp.Core;
using SampSharp.Core.Logging;
using SampSharp.EntityComponentSystem;

namespace TestMode.Ecs
{
	class Program
	{
		static void Main(string[] args)
		{
            new GameModeBuilder()
                .UseLogLevel(CoreLogLevel.Verbose)
                .UseStartBehaviour(GameModeStartBehaviour.FakeGmx)
                .UseEcs<TestStartup>()
                .Run();
		}
	}
}
