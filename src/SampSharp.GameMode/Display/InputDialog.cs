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
using SampSharp.GameMode.Definitions;

namespace SampSharp.GameMode.Display
{
    /// <summary>
    ///     Represents an input dialog.
    /// </summary>
    public class InputDialog : Dialog
    {
        /// <summary>
        ///     Initializes a new instance of the Dialog class.
        /// </summary>
        /// <param name="caption">
        ///     The title at the top of the dialog. The length of the caption can not exceed more than 64
        ///     characters before it starts to cut off.
        /// </param>
        /// <param name="message">The text to display in the main dialog. Use \n to start a new line and \t to tabulate.</param>
        /// <param name="isPassword">if set to <c>true</c> the input will be hidden on the user's screen.</param>
        /// <param name="button1">The text on the left button.</param>
        /// <param name="button2">The text on the right button. Leave it blank to hide it.</param>
        public InputDialog(string caption, string message, bool isPassword, string button1, string button2 = null)
        {
            if (caption == null) throw new ArgumentNullException(nameof(caption));
            if (message == null) throw new ArgumentNullException(nameof(message));
            if (button1 == null) throw new ArgumentNullException(nameof(button1));
            Caption = caption;
            Message = message;
            Button1 = button1;
            Button2 = button2;
            Style = isPassword ? DialogStyle.Password : DialogStyle.Input;
        }

        #region Overrides of Dialog

        /// <summary>
        ///     Gets the Info displayed.
        /// </summary>
        protected override string Info => Message;

        #endregion

        /// <summary>
        ///     Gets or sets the message in the dialog.
        /// </summary>
        public string Message { get; set; }
    }
}