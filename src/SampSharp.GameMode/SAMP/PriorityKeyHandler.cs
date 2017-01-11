// SampSharp
// Copyright 2017 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
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
        ///     Occurs as first handler.
        /// </summary>
        public event EventHandler<CancelableEventArgs> HighPriority;

        /// <summary>
        ///     Occurs as second handler, if the no <see cref="HighPriority" /> have canceled the event.
        /// </summary>
        public event EventHandler<CancelableEventArgs> NormalPriority;

        /// <summary>
        ///     Occurs as third handler, if the no <see cref="HighPriority" /> or <see cref="NormalPriority" /> have canceled the
        ///     event.
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
            HighPriority?.Invoke(sender, e);
        }

        private void OnNormalPriority(object sender, CancelableEventArgs e)
        {
            NormalPriority?.Invoke(sender, e);
        }

        private void OnLowPriority(object sender, CancelableEventArgs e)
        {
            LowPriority?.Invoke(sender, e);
        }
    }
}