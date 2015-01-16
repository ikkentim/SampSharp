// SampSharp
// Copyright (C) 2015 Tim Potze
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
using System.Threading.Tasks;
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.Pools;

namespace SampSharp.GameMode.Tools
{
    /// <summary>
    ///     Contains methods to run an action on the main thread from a different thread.
    /// </summary>
    public static class Sync
    {
        /// <summary>
        ///     Gets whether is it required to sync before calling natives.
        /// </summary>
        public static bool IsRequired
        {
            get { return SyncController.MainThread != Thread.CurrentThread; }
        }

        /// <summary>
        ///     Run a function on the main  thread.
        /// </summary>
        /// <param name="action">The action the run</param>
        public static async Task Run(Action action)
        {
            if (!IsRequired)
            {
                action();
                return;
            }

            var task = new SyncTask {Action = action};
            SyncController.Start();

            while (!task.Done)
            {
                await Task.Delay(1);
            }
        }

        /// <summary>
        ///     Run a function on the main VM thread.
        /// </summary>
        /// <typeparam name="TResult">The type of the return value of the method that the action encapsulates.</typeparam>
        /// <param name="action">The action to run.</param>
        /// <returns>The return value of the method that the action encapsulates.</returns>
        public static async Task<TResult> Run<TResult>(Func<TResult> action)
        {
            if (!IsRequired)
            {
                return action();
            }

            TResult result = default(TResult);

            await Run(() => { result = action(); });

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