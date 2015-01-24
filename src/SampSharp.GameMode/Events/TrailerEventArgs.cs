using System;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.TrailerUpdate" /> event.
    /// </summary>
    public class TrailerEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TrailerEventArgs" /> class.
        /// </summary>
        /// <param name="player">The player sending the update.</param>
        public TrailerEventArgs(GtaPlayer player)
        {
            Player = player;
        }

        /// <summary>
        ///     Gets the player sending the update.
        /// </summary>
        public GtaPlayer Player { get; private set; }

        /// <summary>
        ///     Gets or sets whether to stop the vehicle syncing its position to other players.
        /// </summary>
        public bool PreventPropagation { get; set; }
    }
}