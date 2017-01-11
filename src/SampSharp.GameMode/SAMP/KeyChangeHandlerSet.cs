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
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Tools;

namespace SampSharp.GameMode.SAMP
{
    /// <summary>
    ///     Contains a set of KeyHandlers for different keystates.
    /// </summary>
    public class KeyChangeHandlerSet
    {
        /// <summary>
        ///     Initializes a new instance of the KeyChangeHandlerSet class.
        /// </summary>
        public KeyChangeHandlerSet()
        {
            Pressed = new KeyHandlerSet(KeyUtils.HasPressed);
            Released = new KeyHandlerSet(KeyUtils.HasReleased);
        }

        /// <summary>
        ///     Gets a set of KeyHandlers which are triggered once a key has been pressed.
        /// </summary>
        public KeyHandlerSet Pressed { get; }

        /// <summary>
        ///     Gets a set of KeyHandlers which are triggered once a key has been released.
        /// </summary>
        public KeyHandlerSet Released { get; }

        /// <summary>
        ///     Handles a change in PlayerKeyState.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Object containing information about the event.</param>
        public void Handle(object sender, KeyStateChangedEventArgs e)
        {
            Pressed.Handle(sender, e);
            Released.Handle(sender, e);
        }
    }
}