// SampSharp
// Copyright 2018 Tim Potze
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

using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using SampSharp.Core.Communication;
using SampSharp.Core.Logging;

namespace SampSharp.Core.Threading
{
    internal class CommandWaitQueue
    {
        private readonly ConcurrentQueue<ServerCommandData> _asyncQueue = new ConcurrentQueue<ServerCommandData>();
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(0, int.MaxValue);

        public async Task<ServerCommandData> WaitAsync()
        {
            for (;;)
            {
                if (_asyncQueue.TryDequeue(out var value))
                    return value;

                await _semaphore.WaitAsync();

                if (_asyncQueue.TryDequeue(out var result))
                    return result;
            }
        }

        public ServerCommandData Wait()
        {
            for (;;)
            {
                // TODO: This is rather much of a workaround. It seems with certain timing the semaphore might stay in the waiting state if it has been released too soon. In case that happens, the concurrent queue will have the member added already.
                if (_asyncQueue.TryDequeue(out var value))
                    return value;

                _semaphore.Wait();

                if (_asyncQueue.TryDequeue(out var result))
                    return result;
            }
        }

        public void Release(ServerCommandData command)
        {
            _asyncQueue.Enqueue(command);
            _semaphore.Release();
        }
    }
}