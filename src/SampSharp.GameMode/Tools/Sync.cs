using System;
using System.Threading;
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.Pools;

namespace SampSharp.GameMode.Tools
{
    public static class Sync
    {
        public static void Run(Action action)
        {
            var task = new SyncTask { Action = action };

            SyncController.Start();

            while (!task.Done)
            {
                Thread.Sleep(1);
            }
        }
        public static TResult Run<TResult>(Func<TResult> action)
        {
            TResult result = default(TResult);

            Run(() =>
            {
                result = action();
            });
            
            return result;
        }

        public class SyncTask : Pool<SyncTask>
        {
            public Action Action { get; set; }
            public bool Done { get; private set; }
            public void Run()
            {
                Action();
                Done = true;
            }
        }
    }
}
