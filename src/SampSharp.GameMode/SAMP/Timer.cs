// SampSharp
// Copyright 2015 Tim Potze
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
using SampSharp.GameMode.API;
using SampSharp.GameMode.Tools;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP
{
    /// <summary>
    ///     Represents a SA:MP timer.
    /// </summary>
    public sealed class Timer : Disposable, IIdentifiable
    {
        private const int InvalidId = -1;
        private bool _hit;
        private TimeSpan _interval;
        private bool _isRepeating;

        /// <summary>
        ///     Initializes a new instance of the Timer class.
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
        ///     Initializes a new instance of the Timer class.
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
            Id = running ? Interop.SetTimer((int) interval.TotalMilliseconds, isRepeating, this) : InvalidId;
            Interval = interval;
            IsRepeating = isRepeating;
        }

        /// <summary>
        ///     Gets or sets the interval of this <see cref="Timer" />.
        /// </summary>
        public TimeSpan Interval
        {
            get { return _interval; }
            set
            {
                var wasRunning = IsRunning;
                IsRunning = false;
                _interval = value;
                IsRunning = wasRunning;
            }
        }

        /// <summary>
        ///     Gets or sets whether this Timer is a repeating  <see cref="Timer" />.
        /// </summary>
        public bool IsRepeating
        {
            get { return _isRepeating; }
            set
            {
                if (_isRepeating == value) return;

                var wasRunning = IsRunning;
                IsRunning = false;
                _isRepeating = value;
                IsRunning = wasRunning;
            }
        }

        /// <summary>
        ///     Gets or sets whether this Timer is running.
        /// </summary>
        public bool IsRunning
        {
            get { return (!IsRepeating && !_hit && Id != InvalidId) || (IsRepeating && Id != InvalidId); }
            set
            {
                if (value && !IsRunning)
                {
                    _hit = false;
                    Id = Interop.SetTimer((int) Interval.TotalMilliseconds, IsRepeating, this);
                }
                else if (!value && IsRunning)
                {
                    Interop.KillTimer(Id);
                    Id = InvalidId;
                }
            }
        }

        /// <summary>
        ///     Gets or sets a tag containing about this Timer.
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        ///     Gets the ID of this Timer.
        /// </summary>
        public int Id { get; private set; }

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
            _hit = true;
            if (!IsRepeating)
            {
                Id = InvalidId;
            }

            if (Tick != null)
                Tick(this, e);
        }

        /// <summary>
        ///     Restarts this Timer.
        /// </summary>
        public void Restart()
        {
            IsRunning = false;
            IsRunning = true;
        }
    }
}