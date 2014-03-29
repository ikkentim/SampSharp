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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using GameMode.Definitions;
using GameMode.Events;

namespace GameMode.World
{
    /// <summary>
    ///     Represents a SA:MP dialog.
    /// </summary>
    public class Dialog
    {
        #region Fields

        /// <summary>
        ///     Gets an ID this system will use.
        /// </summary>
        protected const int DialogId = 10000;

        /// <summary>
        ///     Gets an ID this system will use to hide Dialogs.
        /// </summary>
        protected const int DialogHideId = -1;

        /// <summary>
        ///     Contains all instances of Dialogs that are being shown to Players.
        /// </summary>
        protected static Dictionary<int, Dialog> OpenDialogs = new Dictionary<int, Dialog>();

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the Dialog class.
        /// </summary>
        /// <param name="style">The style of the dialog.</param>
        /// <param name="caption">
        ///     The title at the top of the dialog. The length of the caption can not exceed more than 64
        ///     characters before it starts to cut off.
        /// </param>
        /// <param name="message">The text to display in the main dialog. Use \n to start a new line and \t to tabulate.</param>
        /// <param name="button">The text on the left button.</param>
        public Dialog(DialogStyle style, string caption, string message, string button)
        {
            Style = style;
            Caption = caption;
            Message = message;
            Button1 = button;
        }

        /// <summary>
        ///     Initializes a new instance of the Dialog class.
        /// </summary>
        /// <param name="style">The style of the dialog.</param>
        /// <param name="caption">
        ///     The title at the top of the dialog. The length of the caption can not exceed more than 64
        ///     characters before it starts to cut off.
        /// </param>
        /// <param name="message">The text to display in the main dialog. Use \n to start a new line and \t to tabulate.</param>
        /// <param name="button1">The text on the left button.</param>
        /// <param name="button2">The text on the right button. Leave it blank to hide it.</param>
        public Dialog(DialogStyle style, string caption, string message, string button1, string button2)
        {
            Style = style;
            Caption = caption;
            Message = message;
            Button1 = button1;
            Button2 = button2;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     The style of the dialog.
        /// </summary>
        public DialogStyle Style { get; set; }

        /// <summary>
        ///     The title at the top of the dialog. The length of the caption can not exceed more than 64 characters before it
        ///     starts to cut off.
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        ///     The text to display in the main dialog. Use \n to start a new line and \t to tabulate.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     The text on the left button.
        /// </summary>
        public string Button1 { get; set; }

        /// <summary>
        ///     The text on the right button. Leave it blank to hide it.
        /// </summary>
        public string Button2 { get; set; }

        #endregion

        #region Events

        /// <summary>
        ///     Occurs when the <see cref="BaseMode.OnDialogResponse" /> is being called.
        ///     This callback is called when a player responds to a dialog by either clicking a button, pressing ENTER/ESC or
        ///     double-clicking a list item (if using a <see cref="DialogStyle.List" />).
        ///     This callback is called when a player connects to the server.
        /// </summary>
        public event DialogResponseHandler Response;

        #endregion

        #region Methods
        
        /// <summary>
        ///     Shows the dialog box to a Player.
        /// </summary>
        /// <param name="player">The Player to show the dialog to.</param>
        public virtual void Show(Player player)
        {
            OpenDialogs.Add(player.PlayerId, this);

            Native.ShowPlayerDialog(player.PlayerId, DialogId, (int) Style, Caption, Message, Button1,
                Button2 ?? string.Empty);
        }

        /// <summary>
        ///     Hides all dialogs for a Player.
        /// </summary>
        /// <param name="player">The Player to hide all dialogs from.</param>
        public static void Hide(Player player)
        {
            OpenDialogs.Remove(player.PlayerId);

            Native.ShowPlayerDialog(player.PlayerId, DialogHideId, (int) DialogStyle.MessageBox, string.Empty,
                string.Empty, string.Empty, string.Empty);
        }

        /// <summary>
        /// Gets the dialog currently being shown to a Player.
        /// </summary>
        /// <param name="player">The Player whose Dialog you want.</param>
        /// <returns>The Dialog currently being shown to the Player.</returns>
        public static Dialog GetOpenDialog(Player player)
        {
            return OpenDialogs[player.PlayerId];
        }

        /// <summary>
        ///     Raises the <see cref="Response" /> event.
        /// </summary>
        /// <param name="e">An <see cref="DialogResponseEventArgs" /> that contains the event data. </param>
        public virtual void OnResponse(DialogResponseEventArgs e)
        {
            OpenDialogs.Remove(e.PlayerId);

            if (Response != null)
                Response(this, e);
        }

        #endregion
    }
}