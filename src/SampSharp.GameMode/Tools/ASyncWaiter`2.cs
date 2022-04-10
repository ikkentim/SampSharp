// SampSharp
// Copyright 2017 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampSharp.GameMode.Tools
{
    /// <summary>
    ///     Contains methods for awaiting calls.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TArguments"></typeparam>
    public class ASyncWaiter<TKey, TArguments>
    {
        private readonly Dictionary<TKey, TaskCompletionSource<TArguments>> _completionSources =
            new Dictionary<TKey, TaskCompletionSource<TArguments>>();

        /// <summary>
        ///     Waits for the <see cref="Fire" /> to be called with the specified <paramref name="key" />.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The arguments passed to the <see cref="Fire" /> method.</returns>
        public virtual Task<TArguments> Result(TKey key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            var taskCompletionSource = new TaskCompletionSource<TArguments>();
            _completionSources[key] = taskCompletionSource;

            if (key is Disposable disposable)
                disposable.Disposed += OnDisposableDisposed;

            return taskCompletionSource.Task;
        }

        private void OnDisposableDisposed(object sender, EventArgs e)
        {
            if (sender is TKey key)
            {
                Cancel(key);
            }
        }

        /// <summary>
        ///     Fires the task with the given <paramref name="key" />.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="arguments">The arguments.</param>
        public virtual void Fire(TKey key, TArguments arguments)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (!_completionSources.ContainsKey(key)) return;

            var task = _completionSources[key];
            Remove(key);
            task.SetResult(arguments);
        }

        /// <summary>
        ///     Cancels the task for the specified <paramref name="key" />.
        /// </summary>
        /// <param name="key">The key of the task to cancel.</param>
        public virtual void Cancel(TKey key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (!_completionSources.ContainsKey(key)) return;

            var task = _completionSources[key];
            Remove(key);
            task.TrySetCanceled();
        }

        private void Remove(TKey key)
        {
            _completionSources.Remove(key);

            if (key is Disposable disposable)
                disposable.Disposed -= OnDisposableDisposed;

            _completionSources.Remove(key);
        }
    }
}
