using System;

namespace GameMode.World
{
    /// <summary>
    /// Represents a SA:MP timer.
    /// </summary>
    public class Timer
    {
        private bool _hit;

        /// <summary>
        /// Initializes a new instance of the Timer class.
        /// </summary>
        /// <param name="interval">The interval in miliseconds.</param>
        /// <param name="repeat">Whether to repeat the timer (True); or stop after the first Tick(False).</param>
        public Timer(int interval, bool repeat)
        {
            Console.WriteLine("Timer intialized");
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
                if (value)
                {
                    //Single-run and not hit yet and repeating timers run already
                    if (Repeat || !_hit) return;

                    //Reset hit value and restart timer.
                    _hit = false;
                    Console.WriteLine("Start a Running timer");
                    Id = Native.SetTimer(Interval, Repeat, this);
                }
                else
                {
                    if (!Repeat && _hit) return;

                    //Kill the timer. If killed already, no worried; Ids are unique.
                    Native.KillTimer(Id);
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
        }
    }
}
