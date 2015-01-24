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
        public event EventHandler<CancelableEventArgs> HighPriority;

        /// <summary>
        /// Occurs as second handler, if the no <see cref="HighPriority" /> have canceled the event.
        /// </summary>
        public event EventHandler<CancelableEventArgs> NormalPriority;
        /// <summary>
        /// Occurs as third handler, if the no <see cref="HighPriority" /> or <see cref="NormalPriority" /> have canceled the event.
        /// </summary>
        public event EventHandler<CancelableEventArgs> LowPriority;

        /// <summary>
        ///     Handles a change in PlayerKeyState.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Object containing information about the event.</param>
        public void Handle(object sender, KeyStateChangedEventArgs e)
        {
            var args = new CancelableEventArgs();

            OnHighPriority(sender, args);

            if (!args.IsCanceled)
                OnNormalPriority(sender, args);

            if (!args.IsCanceled)
                OnLowPriority(sender, args);
        }

        private void OnHighPriority(object sender, CancelableEventArgs e)
        {
            if (HighPriority != null)
                HighPriority(sender, e);
        }

        private void OnNormalPriority(object sender, CancelableEventArgs e)
        {
            if (NormalPriority != null)
                NormalPriority(sender, e);
        }

        private void OnLowPriority(object sender, CancelableEventArgs e)
        {
            if (LowPriority != null)
                LowPriority(sender, e);
        }
    }
}