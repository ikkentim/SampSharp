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

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SampSharp.Core.Threading
{
    internal class DataReceiveStack<T>
    {
        private readonly Stack<TaskCompletionSource<T>> _receivers = new Stack<TaskCompletionSource<T>>();
        private readonly Queue<T> _remainers = new Queue<T>();

        public void Push(T value)
        {
            lock (this)
            {
                if (_receivers.Count > 0)
                    _receivers.Pop().SetResult(value);
                else
                    _remainers.Enqueue(value);
            }
        }

        public T Pop()
        {
            TaskCompletionSource<T> source;
            lock (this)
            {
                if (_remainers.Count > 0)
                    return _remainers.Dequeue();

                _receivers.Push(source = new TaskCompletionSource<T>());
            }

            return source.Task.Result;
        }

        public Task<T> PopAsync()
        {
            TaskCompletionSource<T> source;
            lock (this)
            {
                if (_remainers.Count > 0)
                    return Task.FromResult(_remainers.Dequeue());

                _receivers.Push(source = new TaskCompletionSource<T>());
            }

            return source.Task;
        }
    }
}