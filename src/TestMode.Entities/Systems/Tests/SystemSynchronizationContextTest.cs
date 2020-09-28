using System.Threading;
using System.Threading.Tasks;
using SampSharp.Entities;

namespace TestMode.Entities.Systems.Tests
{
    public class SystemSynchronizationContextTest : ISystem
    {
        [Event]
        public void OnGameModeInit()
        {
            var ctx = SynchronizationContext.Current;
            Task.Run(() =>
            {
                // ... Run things on a worker thread.

                ctx.Send(_ =>
                {
                    // ... Run things on the main thead.
                }, null);
            });
        }
    }
}