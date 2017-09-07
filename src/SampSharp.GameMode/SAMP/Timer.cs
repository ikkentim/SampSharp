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
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.Tools;

namespace SampSharp.GameMode.SAMP
{
    /// <summary>
    ///     Represents a SA:MP timer.
    /// </summary>
    public sealed class Timer : Disposable
    {
        private TimeSpan _interval;
        private bool _isRunning;
        private DateTime _lastTick;

        /// <summary>
        ///     Initializes a new instance of the Timer class and starts the timer.
        /// </summary>
        /// <param name="interval">The interval in miliseconds.</param>
        /// <param name="isRepeating">Whether to IsRepeating the timer (True); or stop after the first Tick(False).</param>
        public Timer(double interval, bool isRepeating)
            : this(interval, isRepeating, true)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the Timer class.
        /// </summary>
        /// <param name="interval">The interval in miliseconds.</param>
        /// <param name="isRepeating">Whether to IsRepeating the timer (True); or stop after the first Tick(False).</param>
        /// <param name="running">Whether the timer is running.</param>
        public Timer(double interval, bool isRepeating, bool running)
            : this(TimeSpan.FromMilliseconds(interval), isRepeating, running)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the Timer class and starts the timer.
        /// </summary>
        /// <param name="interval">The interval in miliseconds.</param>
        /// <param name="isRepeating">Whether to IsRepeating the timer (True); or stop after the first Tick(False).</param>
        public Timer(int interval, bool isRepeating) : this(interval, isRepeating, true)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the Timer class.
        /// </summary>
        /// <param name="interval">The interval in miliseconds.</param>
        /// <param name="isRepeating">Whether to IsRepeating the timer (True); or stop after the first Tick(False).</param>
        /// <param name="running">Whether the timer is running.</param>
        public Timer(int interval, bool isRepeating, bool running)
            : this(TimeSpan.FromMilliseconds(interval), isRepeating, running)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the Timer class and starts the timer.
        /// </summary>
        /// <param name="interval">The interval.</param>
        /// <param name="isRepeating">Whether to IsRepeating the timer (True); or stop after the first Tick(False).</param>
        public Timer(TimeSpan interval, bool isRepeating) : this(interval, isRepeating, true)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the Timer class.
        /// </summary>
        /// <param name="interval">The interval.</param>
        /// <param name="isRepeating">Whether to IsRepeating the timer (True); or stop after the first Tick(False).</param>
        /// <param name="running">Whether the timer is running.</param>
        public Timer(TimeSpan interval, bool isRepeating, bool running)
        {
            Interval = interval;
            IsRepeating = isRepeating;
            IsRunning = running;
        }

        /// <summary>
        ///     Gets or sets the interval of this <see cref="Timer" />.
        /// </summary>
        public TimeSpan Interval
        {
            get => _interval;
            set
            {
                _interval = value;

                if (IsRunning)
                    NextTick = _lastTick + value;
            }
        }

        /// <summary>
        ///     Gets or sets whether this <see cref="Timer"/> is repeating.
        /// </summary>
        public bool IsRepeating { get; set; }

        /// <summary>
        ///     Gets or sets whether this <see cref="Timer"/> is running.
        /// </summary>
        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                if (_isRunning == value) return;
                _isRunning = value;

                if (value)
                {
                    _lastTick = DateTime.UtcNow;
                    NextTick = _lastTick + _interval;
                    TimerController.Instance.AddTimer(this);
                }
                else
                    TimerController.Instance.RemoveTimer(this);
            }
        }

        /// <summary>
        ///     Gets or sets a tag containing about this Timer.
        /// </summary>
        public object Tag { get; set; }

        internal DateTime NextTick { get; private set; }

        /// <summary>
        ///     Runs the specified action repeatedly with the specified interval.
        /// </summary>
        /// <param name="interval">The interval.</param>
        /// <param name="action">The action.</param>
        /// <returns>The creatd timer.</returns>
        public static Timer Run(TimeSpan interval, Action action)
        {
            var t = new Timer(interval, true, true);
            t.Tick += (sender, args) => action();
            return t;
        }

        /// <summary>
        ///     Runs the specified action once after the specified interval.
        /// </summary>
        /// <param name="interval">The interval.</param>
        /// <param name="action">The action.</param>
        /// <returns>The created timer.</returns>
        public static Timer RunOnce(TimeSpan interval, Action action)
        {
            var t = new Timer(interval, false, true);
            t.Tick += (sender, args) => action();
            return t;
        }

        /// <summary>
        ///     Removes this instance from the pool.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            IsRunning = false;
        }

        /// <summary>
        ///     Occurs when the interval has elapsed.
        /// </summary>
        public event EventHandler Tick;

        /// <summary>
        ///     Raises the <see cref="Tick" /> event.
        /// </summary>
        /// <param name="e">A <see cref="System.EventArgs" /> that contains the event data.</param>
        public void OnTick(EventArgs e)
        {
            Tick?.Invoke(this, e);
        }

        /// <summary>
        ///     Restarts this Timer.
        /// </summary>
        public void Restart()
        {
            _lastTick = DateTime.UtcNow;
            IsRunning = true;
        }

        internal void PerformTick()
        {
            OnTick(EventArgs.Empty);

            if (!IsRepeating)
                IsRunning = false;
            else
            {
                _lastTick = DateTime.UtcNow;
                NextTick = NextTick + _interval;
            }
        }
    }
}