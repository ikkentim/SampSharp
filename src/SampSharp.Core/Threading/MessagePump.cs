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
using System.Threading;

namespace SampSharp.Core.Threading
{
    public class MessagePump : IDisposable
    {
        private readonly MessageQueue _queue;

        private readonly ManualResetEvent _stopEvent = new ManualResetEvent(false);

        public MessagePump(MessageQueue queue)
        {
            _queue = queue ?? throw new ArgumentNullException(nameof(queue));
        }

        #region IDisposable

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _stopEvent.Set();
            _queue.ReleaseReader();
        }

        #endregion

        public void Pump()
        {
            while (true)
            {
                var stop = _stopEvent.WaitOne(0);
                if (stop)
                    break;

                var workItem = _queue.Dequeue();
                workItem?.Execute();
            }
        }
    }
}