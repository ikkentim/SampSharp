namespace GameMode.Definitions
{
    /// <summary>
    /// Contains the reasons a player could disconnect.
    /// </summary>
    public enum PlayerDisconnectReason
    {
        /// <summary>
        ///     The player timed out.
        /// </summary>
        TimedOut = 0,
        /// <summary>
        ///     The player left.
        /// </summary>
        Left = 1,
        /// <summary>
        ///     The player was kicked.
        /// </summary>
        Kicked = 2
    }
}
