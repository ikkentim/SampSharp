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
using SampSharp.GameMode.SAMP;

namespace SampSharp.GameMode.Controllers
{
    /// <summary>
    ///     A controller processing all timer actions.
    /// </summary>
    [Controller]
    public class TimerController : IEventListener
    {
        internal static TimerController Instance;
        private readonly List<Timer> _activeTimers = new List<Timer>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="TimerController"/> class.
        /// </summary>
        public TimerController()
        {
            Instance = this;
        }

        internal void AddTimer(Timer timer)
        {
            if (timer == null) throw new ArgumentNullException(nameof(timer));
            _activeTimers.Add(timer);
        }

        internal void RemoveTimer(Timer timer)
        {
            if (timer == null) throw new ArgumentNullException(nameof(timer));
            _activeTimers.Remove(timer);
        }

        /// <summary>
        ///     Registers the events this TimerController wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        public virtual void RegisterEvents(BaseMode gameMode)
        {
            gameMode.Tick += OnTick;
        }

        private void OnTick(object sender, EventArgs eventArgs)
        {
            if (_activeTimers.Count == 0)
                return;

            var now = DateTime.UtcNow;
            for(var i = _activeTimers.Count - 1; i >= 0;i--)
            {
                if (i >= _activeTimers.Count) continue;

                var timer = _activeTimers[i];

                if (!timer.IsRunning)
                {
                    _activeTimers.Remove(timer);
                }
                else if (timer.NextTick <= now)
                {
                    timer.PerformTick();
                }
            }
        }
    }
}