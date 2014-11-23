// SampSharp
// Copyright (C) 2014 Tim Potze
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
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP
{
    /// <summary>
    ///     Represents a SA:MP timer.
    /// </summary>
    public sealed class Timer : IdentifiedPool<Timer>, IIdentifiable
    {
        private const int InvalidId = -1;

        private bool _hit;
        private int _interval;
        private bool _repeat;

        /// <summary>
        ///     Initializes a new instance of the Timer class.
        /// </summary>
        /// <param name="interval">The interval in miliseconds.</param>
        /// <param name="repeat">Whether to repeat the timer (True); or stop after the first Tick(False).</param>
        public Timer(int interval, bool repeat)
        {
            Id = Native.SetTimer(interval, repeat, this);
            Interval = interval;
            Repeat = repeat;
        }

        /// <summary>
        ///     Initializes a new instance of the Timer class.
        /// </summary>
        /// <param name="interval">The interval in miliseconds.</param>
        /// <param name="repeat">Whether to repeat the timer (True); or stop after the first Tick(False).</param>
        /// <param name="running">Whether the timer is running</param>
        public Timer(int interval, bool repeat, bool running)
        {
            Id = running ? Native.SetTimer(interval, repeat, this) : InvalidId;
            Interval = interval;
            Repeat = repeat;
        }

        /// <summary>
        ///     Gets or sets the interval of this Timer.
        /// </summary>
        public int Interval
        {
            get { return _interval; }
            set
            {
                bool wasRunning = IsRunning;
                IsRunning = false;
                _interval = value;
                IsRunning = wasRunning;
            }
        }

        /// <summary>
        ///     Gets or sets whether this Timer is a repeating timer.
        /// </summary>
        public bool Repeat
        {
            get { return _repeat; }
            set
            {
                if (_repeat == value) return;

                bool wasRunning = IsRunning;
                IsRunning = false;
                _repeat = value;
                IsRunning = wasRunning;
            }
        }

        /// <summary>
        ///     Gets or sets whether this Timer is running.
        /// </summary>
        public bool IsRunning
        {
            get { return (!Repeat && !_hit && Id != InvalidId) || (Repeat && Id != InvalidId); }
            set
            {
                if (value && !IsRunning)
                {
                    _hit = false;
                    Id = Native.SetTimer(Interval, Repeat, this);
                }
                else if (!value && IsRunning)
                {
                    Native.KillTimer(Id);
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
            base.Dispose(disposing);

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
            if (!Repeat) Id = InvalidId;

            if (Tick != null)
                Tick(this, e);
        }
    }
}