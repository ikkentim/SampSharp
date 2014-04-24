namespace SampSharp.GameMode.Controllers
{
    /// <summary>
    ///     Provides the functionality for an <see cref="IController"/> to act on events.
    /// </summary>
    public interface IEventListener : IController
    {
        /// <summary>
        ///     Registers the events this <see cref="IEventListener"/> wants to listen to.
        /// </summary>
        /// <param name="gameMode">An instance of the <see cref="BaseMode"/> currently running.</param>
        void RegisterEvents(BaseMode gameMode);
    }
}
