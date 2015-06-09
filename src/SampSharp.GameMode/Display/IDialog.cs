// SampSharp
// Copyright 2015 Tim Potze
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
using System.Threading.Tasks;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Display
{
    /// <summary>
    ///     Provides the functionality of a SA:MP dialog.
    /// </summary>
    public interface IDialog
    {
        /// <summary>
        ///     Gets the style.
        /// </summary>
        DialogStyle Style { get; }

        /// <summary>
        ///     Gets the caption.
        /// </summary>
        /// <remarks>
        ///     The length of the caption can not exceed more than 64 characters before it
        ///     starts to cut off.
        /// </remarks>
        string Caption { get; }

        /// <summary>
        ///     Gets the message displayed.
        /// </summary>
        string Message { get; }

        /// <summary>
        ///     Gets the text on the left button.
        /// </summary>
        string Button1 { get; }

        /// <summary>
        ///     Gets the text on the right button.
        /// </summary>
        /// <remarks>Leave it blank to hide it.</remarks>
        string Button2 { get; }

        /// <summary>
        ///     Occurs when a player responds to a dialog by either clicking a button, pressing ENTER/ESC or double-clicking a list
        ///     item .
        /// </summary>
        event EventHandler<DialogResponseEventArgs> Response;

        /// <summary>
        ///     Shows the dialog box to a Player.
        /// </summary>
        /// <param name="player">The Player to show the dialog to.</param>
        void Show(GtaPlayer player);

        /// <summary>
        ///     Shows the dialog box to a Player asynchronously.
        /// </summary>
        /// <param name="player">The Player to show the dialog to.</param>
        Task<DialogResponseEventArgs> ShowAsync(GtaPlayer player);

        /// <summary>
        ///     Raises the <see cref="Response" /> event.
        /// </summary>
        /// <param name="e">An <see cref="DialogResponseEventArgs" /> that contains the event data. </param>
        void OnResponse(DialogResponseEventArgs e);
    }
}