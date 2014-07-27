// SampSharp
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

using System;
using System.Threading;
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.Pools;

namespace SampSharp.GameMode.Tools
{
    /// <summary>
    ///     Contains methods to run an action on the main VM thread from a different thread.
    /// </summary>
    public static class Sync
    {
        /// <summary>
        ///     Gets whether is it required to sync before calling natives.
        /// </summary>
        public static bool IsRequired
        {
            get { return SyncController.IsMainThread; }
        }

        /// <summary>
        ///     Run a function on the main VM thread.
        /// </summary>
        /// <param name="action">The action the run</param>
        public static void Run(Action action)
        {
            if (SyncController.IsMainThread)
            {
                action();
                return;
            }

            var task = new SyncTask {Action = action};
            SyncController.Start();

            while (!task.Done)
            {
                Thread.Sleep(1);
            }
        }

        /// <summary>
        ///     Run a function on the main VM thread.
        /// </summary>
        /// <typeparam name="TResult">The type of the return value of the method that the action encapsulates.</typeparam>
        /// <param name="action">The action to run.</param>
        /// <returns>The return value of the method that the action encapsulates.</returns>
        public static TResult Run<TResult>(Func<TResult> action)
        {
            if (SyncController.IsMainThread)
            {
                return action();
            }

            TResult result = default(TResult);

            Run(() => { result = action(); });

            return result;
        }

        internal sealed class SyncTask : Pool<SyncTask>
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