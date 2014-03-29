namespace GameMode.Controllers
{
    /// <summary>
    /// Provides the functionality for a controller to act on events.
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// Registers the events this IController wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        void RegisterEvents(BaseMode gameMode);
    }
}
