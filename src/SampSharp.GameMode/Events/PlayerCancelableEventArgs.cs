using SampSharp.GameMode.SAMP;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for events in <see cref="KeyHandlerSet" />.
    /// </summary>
    public class PlayerCancelableEventArgs : PlayerEventArgs
    {
        public PlayerCancelableEventArgs(int playerid)
            : base(playerid)
        {
        }

        public bool Canceled { get; private set; }
    }
}