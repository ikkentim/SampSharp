using GameMode.World;

namespace GameMode.Controllers
{
    /// <summary>
    /// A controller processing all timer actions.
    /// </summary>
    public class TimerController : IController
    {
        /// <summary>
        /// Registers the events this TimerController wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        public void RegisterEvents(BaseMode gameMode)
        {
            gameMode.TimerTick += (sender, args) =>
            {
                var timer = sender as Timer;

                if(timer != null)
                    timer.OnTick(args);
            };
        }
    }
}
