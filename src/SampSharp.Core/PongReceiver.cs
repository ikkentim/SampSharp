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
using System.Threading.Tasks;

namespace SampSharp.Core
{
    /// <summary>
    ///     Represents an measurement device for the duration of pings to the SampSharp server.
    /// </summary>
    internal class PongReceiver
    {
        private readonly TaskCompletionSource<int> _ponger = new TaskCompletionSource<int>();

        private TimeSpan? _result;
        private DateTime _start;

        /// <summary>
        ///     Gets the task which contains the result after the ping has been completed.
        /// </summary>
        public Task<TimeSpan> Task => GetTask();

        private async Task<TimeSpan> GetTask()
        {
            if (_result == null)
                await _ponger.Task;

            return _result ?? default(TimeSpan);
        }

        /// <summary>
        ///     Ends the ping measurement.
        /// </summary>
        public void Pong()
        {
            _result = DateTime.Now - _start;
            _ponger.SetResult(0);
        }

        /// <summary>
        ///     Starts the ping measurement.
        /// </summary>
        public void Ping()
        {
            _result = null;
            _start = DateTime.Now;
        }
    }
}