namespace SampSharp.Core
{
    /// <summary>
    ///     Contains possible behaviours for the <see cref="GameModeBuilder" /> once a OnGameModeExit call has been received.
    /// </summary>
    public enum GameModeExitBehaviour
    {
        /// <summary>
        ///     Restart the game mode.
        /// </summary>
        Restart,

        /// <summary>
        ///     Return from the <see cref="GameModeBuilder.Run" /> routine.
        /// </summary>
        ShutDown
    }
}