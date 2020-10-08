using System;
using System.Diagnostics;
using SampSharp.Entities;
using SampSharp.Entities.SAMP;

namespace TestMode.Entities.Systems.IssueTests
{
    public class Issue326Timers : ISystem
    {
        private readonly Stopwatch _stopwatch1 = new Stopwatch();
        private readonly Stopwatch _stopwatch2 = new Stopwatch();
        private int _readonlyTicks;

        [Timer(60000)]
        public void Every60Sec(IServerService serverService)
        {
            Console.WriteLine($"Every 60 seconds timer! {_stopwatch1.Elapsed}, tick rate: {serverService.TickRate}");
            _stopwatch1.Restart();
        }

        [Event]
        public void OnGameModeInit(ITimerService timerService)
        {
            _stopwatch1.Start();
            _stopwatch2.Start();

            TimerReference timer = null;
            timer = timerService.Start(_ =>
            {
                if (++_readonlyTicks == 3)
                {
                    Console.WriteLine("Stop timer");
                    timerService.Stop(timer);
                }
                Console.WriteLine($"Manual timer {_stopwatch2.Elapsed}");
                _stopwatch2.Restart();
            }, TimeSpan.FromSeconds(0.1));
        }
    }
}