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
using GameMode.Events;

namespace GameMode.World
{
    /// <summary>
    ///     Represents a SA:MP timer.
    /// </summary>
    public class Timer : IDisposable
    {
        private bool _hit;
        private static BaseMode _gameMode;

        /// <summary>
        ///     Initializes a new instance of the Timer class.
        /// </summary>
        /// <param name="interval">The interval in miliseconds.</param>
        /// <param name="repeat">Whether to repeat the timer (True); or stop after the first Tick(False).</param>
        public Timer(int interval, bool repeat)
        {
            _gameMode.Exited += GameModeExited;

            Id = Native.SetTimer(interval, repeat, this);
            Interval = interval;
            Repeat = repeat;
        }

        /// <summary>
        ///     Gets the ID of this Timer.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        ///     Gets the interval of this Timer.
        /// </summary>
        public int Interval { get; private set; }

        /// <summary>
        ///     Gets whether this Timer is a repeating timer.
        /// </summary>
        public bool Repeat { get; private set; }

        /// <summary>
        ///     Gets or sets whether this Timer is running.
        /// </summary>
        public bool Running
        {
            get
            {
                //Single-run and not hit yet, or a repeating timer
                return !_hit || Repeat;
            }
            set
            {
                if (value && Running)
                {
                    _hit = false;
                    Id = Native.SetTimer(Interval, Repeat, this);

                    _gameMode.Exited += GameModeExited;
                }
                else if (!value && Running)
                {
                    Native.KillTimer(Id);

                    _gameMode.Exited -= GameModeExited;
                }
            }
        }

        /// <summary>
        ///     Gets or sets a tag containing about this Timer.
        /// </summary>
        public object Tag { get; set; }

        public void Dispose()
        {
            Running = false;
        }

        /// <summary>
        ///     Occurs when the interval has elapsed.
        /// </summary>
        public event EventHandler Tick;

        /// <summary>
        ///     Registers all events the Timer class listens to.
        /// </summary>
        /// <param name="gameMode">An instance of BaseMode to which to listen.</param>
        public static void RegisterEvents(BaseMode gameMode)
        {
            _gameMode = gameMode;
            gameMode.TimerTick += (sender, args) =>
            {
                var timer = sender as Timer;
                if(timer != null)
                    timer.OnTick(args);
            };
        }

        /// <summary>
        ///     Raises the <see cref="Tick" /> event.
        /// </summary>
        /// <param name="e">A <see cref="System.EventArgs" /> that contains the event data.</param>
        public virtual void OnTick(EventArgs e)
        {
            if (Tick != null)
                Tick(this, e);

            _hit = true;

            if (!Running)
                _gameMode.Exited -= GameModeExited;
        }

        private void GameModeExited(object sender, GameModeEventArgs e)
        {
            Dispose();
        }
    }
}