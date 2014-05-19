// SampSharp
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

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