using System;
using SampSharp.GameMode.Events;

namespace SampSharp.GameMode.SAMP
{
    /// <summary>
    ///     Contains a set priority events.
    /// </summary>
    public sealed class PriorityKeyHandler
    {
        /// <summary>
        /// Occurs as first handler.
        /// </summary>
        public event EventHandler<PlayerCancelableEventArgs> HighPriority;

        /// <summary>
        /// Occurs as second handler, if the no <see cref="HighPriority" /> have canceled the event.
        /// </summary>
        public event EventHandler<PlayerCancelableEventArgs> NormalPriority;
        /// <summary>
        /// Occurs as third handler, if the no <see cref="HighPriority" /> or <see cref="NormalPriority" /> have canceled the event.
        /// </summary>
        public event EventHandler<PlayerCancelableEventArgs> LowPriority;

        /// <summary>
        ///     Handles a change in PlayerKeyState.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Object containing information about the event.</param>
        public void Handle(object sender, PlayerKeyStateChangedEventArgs e)
        {
            var args = new PlayerCancelableEventArgs(e.PlayerId);

            OnHighPriority(args);

            if (!args.Canceled)
                OnNormalPriority(args);

            if (!args.Canceled)
                OnLowPriority(args);
        }

        private void OnHighPriority(PlayerCancelableEventArgs e)
        {
            if (HighPriority != null)
                HighPriority(this, e);
        }

        private void OnNormalPriority(PlayerCancelableEventArgs e)
        {
            if (NormalPriority != null)
                NormalPriority(this, e);
        }

        private void OnLowPriority(PlayerCancelableEventArgs e)
        {
            if (LowPriority != null)
                LowPriority(this, e);
        }
    }
}