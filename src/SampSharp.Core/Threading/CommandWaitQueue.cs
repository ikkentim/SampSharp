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

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SampSharp.Core.Communication;
using SampSharp.Core.Logging;

namespace SampSharp.Core.Threading
{
    internal class CommandWaitQueue
    {
        private readonly List<ServerCommandData> _backlog = new List<ServerCommandData>();
        private readonly ConcurrentQueue<ServerCommandData> _dismissed = new ConcurrentQueue<ServerCommandData>();
        private readonly ConcurrentQueue<ServerCommandData> _asyncQueue = new ConcurrentQueue<ServerCommandData>();
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(0, int.MaxValue);

        public async Task<ServerCommandData> WaitAsync()
        {
            ServerCommandData value;

            lock (_backlog)
            {
                if (_backlog.Count > 0)
                {
                    value = _backlog[0];
                    _backlog.RemoveAt(0);// TODO: Improve O(n) performance
                    return value;
                }
            }

            for (;;)
            {
                if (_asyncQueue.TryDequeue(out value))
                    return value;

                await _semaphore.WaitAsync();

                if (_asyncQueue.TryDequeue(out value))
                    return value;
            }
        }

        public ServerCommandData Wait(Func<ServerCommandData, bool> accept = null)
        {
            ServerCommandData value;
            
            lock (_backlog)
            {
                if (accept == null)
                {
                    if (_backlog.Count > 0)
                    {
                        value = _backlog[0];
                        _backlog.RemoveAt(0);// TODO: Improve O(n) performance
                        return value;
                    }
                }
                else
                {
                    for (var i = 0; i < _backlog.Count; i++)
                    {
                        if (accept(_backlog[i]))
                        {
                            value = _backlog[i];
                            _backlog.RemoveAt(i);
                            return value;
                        }
                    }
                }
            }

            for (;;)
            {
                // TODO: This is rather much of a workaround. It seems with certain timing the semaphore might stay in
                // TODO: the waiting state if it has been released too soon. In case that happens, the concurrent queue
                // TODO: will have the member added already.
                if (_asyncQueue.TryDequeue(out value))
                {
                    var ok = accept?.Invoke(value) ?? true;

                    if (ok)
                    {
                        return value;
                    }

                    CoreLog.LogDebug("command added to backlog");
                    lock (_backlog)
                    {
                        _backlog.Add(value);
                        continue;
                    }
                }


                _semaphore.Wait();

                if (_asyncQueue.TryDequeue(out value))
                {
                    var ok = accept?.Invoke(value) ?? true;
                    
                    if (ok)
                    {
                        return value;
                    }
                    
                    CoreLog.LogDebug("command added to backlog");
                    lock (_backlog)
                    {
                        _backlog.Add(value);
                    }
                }
            }
        }

        public void Release(ServerCommandData command)
        {
            _asyncQueue.Enqueue(command);
            _semaphore.Release();
        }
    }
}