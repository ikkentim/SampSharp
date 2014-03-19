using System;

namespace GameMode.World
{
    /// <summary>
    /// Represents a SA:MP timer.
    /// </summary>
    public class Timer : IDisposable
    {
        private bool _hit;

        /// <summary>
        /// Initializes a new instance of the Timer class.
        /// </summary>
        /// <param name="interval">The interval in miliseconds.</param>
        /// <param name="repeat">Whether to repeat the timer (True); or stop after the first Tick(False).</param>
        public Timer(int interval, bool repeat)
        {
            BaseMode.Instance.Exited += BaseMode_Exited;

            Id = Native.SetTimer(interval, repeat, this);
            Interval = interval;
            Repeat = repeat;
        }

        /// <summary>
        /// Occurs when the interval has elapsed.
        /// </summary>
        public event EventHandler Tick;

        /// <summary>
        /// Gets the ID of this Timer.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the interval of this Timer.
        /// </summary>
        public int Interval { get; private set; }

        /// <summary>
        /// Gets whether this Timer is a repeating timer.
        /// </summary>
        public bool Repeat { get; private set; }

        /// <summary>
        /// Gets or sets whether this Timer is running.
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

                    BaseMode.Instance.Exited += BaseMode_Exited;
                }
                else if(!value && Running)
                {
                    Native.KillTimer(Id);

                    BaseMode.Instance.Exited -= BaseMode_Exited;
                }
            }
        }
        /// <summary>
        /// Gets or sets a tag containing about this Timer.
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Raises the <see cref="Tick"/> event.
        /// </summary>
        /// <param name="e">A <see cref="System.EventArgs"/> that contains the event data.</param>
        public virtual void OnTick(EventArgs e)
        {
            if (Tick != null)
                Tick(this, e);

            _hit = true;

            if(!Running)
                BaseMode.Instance.Exited -= BaseMode_Exited;
        }

        public void Dispose()
        {
            Running = false;
        }

        private void BaseMode_Exited(object sender, Events.GameModeEventArgs e)
        {
            Dispose();
        }
    }
}
