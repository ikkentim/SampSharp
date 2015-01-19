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

using SampSharp.GameMode.Definitions;

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.DialogResponse" /> event.
    /// </summary>
    public class DialogResponseEventArgs : PlayerEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the DialogResponseEventArgs class.
        /// </summary>
        /// <param name="playerid">Id of the player.</param>
        /// <param name="dialogid">Id of the dialog.</param>
        /// <param name="response">Response of the dialogresponse.</param>
        /// <param name="listitem">Listitem of the dialogresponse.</param>
        /// <param name="inputtext">Inputtext of the dialogresponse.</param>
        public DialogResponseEventArgs(int playerid, int dialogid, int response, int listitem, string inputtext)
            : base(playerid)
        {
            DialogId = dialogid;
            DialogButton = (DialogButton) response;
            ListItem = listitem;
            InputText = inputtext;
        }

        /// <summary>
        ///     Gets the id of dialog of this response.
        /// </summary>
        public int DialogId { get; private set; }

        /// <summary>
        ///     Gets the button clicked for this response.
        /// </summary>
        public DialogButton DialogButton { get; private set; }

        /// <summary>
        ///     Gets the index of the listitem clicked for this response.
        /// </summary>
        public int ListItem { get; private set; }

        /// <summary>
        ///     Gets the inputtext of this response.
        /// </summary>
        public string InputText { get; private set; }
    }
}