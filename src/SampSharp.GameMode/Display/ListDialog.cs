using System.Collections.Generic;
using SampSharp.GameMode.Definitions;

namespace SampSharp.GameMode.Display
{
    public class ListDialog : Dialog
    {
        private readonly List<string> _items = new List<string>();

        /// <summary>
        ///     Initializes a new instance of the Dialog class.
        /// </summary>
        /// <param name="caption">
        ///     The title at the top of the dialog. The length of the caption can not exceed more than 64
        ///     characters before it starts to cut off.
        /// </param>
        /// <param name="button1">The text on the left button.</param>
        /// <param name="button2">The text on the right button. Leave it blank to hide it.</param>
        public ListDialog(string caption, string button1, string button2 = null)
            : base(DialogStyle.List, caption, button1, button2)
        {
        }

        public IList<string> Items
        {
            get { return _items; }
        }

        #region Overrides of Dialog

        /// <summary>
        ///     Gets the message displayed.
        /// </summary>
        public override string Message
        {
            get { return string.Join("\n", Items); }
        }

        #endregion
    }
}