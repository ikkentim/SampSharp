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
using System.Collections.Generic;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.Tools;

namespace SampSharp.GameMode.Display
{
    /// <summary>
    ///     Represents a list dialog.
    /// </summary>
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
        {
            Button1 = button1 ?? throw new ArgumentNullException(nameof(button1));
            Button2 = button2;
            Caption = caption ?? throw new ArgumentNullException(nameof(caption));
            Style = DialogStyle.List;
        }

        /// <summary>
        ///     Gets the list items.
        /// </summary>
        public IList<string> Items => _items;

        /// <summary>
        /// Adds the specified item to the list items.
        /// </summary>
        /// <param name="item">The item.</param>
        public void AddItem(string item)
        {
            _items.Add(item);
        }

        /// <summary>
        /// Adds a collection of items to the list items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="items"/> is null.</exception>
        public void AddItems(IEnumerable<string> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));

            _items.AddRange(items);
        }

        #region Overrides of Dialog

        /// <summary>
        ///     Gets the info displayed in the box.
        /// </summary>
        protected override string Info => string.Join("\n", Items);

        #endregion
    }

    /// <summary>
    ///     Represents a list dialog.
    /// </summary>
    public class ListDialog<T> : ListDialog
    {
        private readonly List<T> _items = new List<T>();

        /// <summary>
        ///     Initializes a new instance of the Dialog class.
        /// </summary>
        /// <param name="caption">
        ///     The title at the top of the dialog. The length of the caption can not exceed more than 64
        ///     characters before it starts to cut off.
        /// </param>
        /// <param name="button1">The text on the left button.</param>
        /// <param name="button2">The text on the right button. Leave it blank to hide it.</param>
        public ListDialog(string caption, string button1, string button2 = null) : base(caption, button1, button2)
        {

        }

        /// <summary>
        ///     Gets the list items.
        /// </summary>
        public new IList<T> Items => _items;

        /// <summary>
        /// Adds the specified item to the list items.
        /// </summary>
        /// <param name="item">The item.</param>
        public void AddItem(T item)
        {
            _items.Add(item);
        }

        /// <summary>
        /// Adds a collection of items to the list items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="items"/> is null.</exception>
        public void AddItems(IEnumerable<T> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));

            _items.AddRange(items);
        }

        #region Overrides of Dialog

        /// <summary>
        ///     Gets the info displayed in the box.
        /// </summary>
        protected override string Info
        {
            get
            {
                if (typeof(T) == typeof(Color))
                {
                    List<string> colorStr = new List<string>();
                    foreach (T color in Items)
                    {
                        colorStr.Add(color + (color as Color?)?.ToLiteralString() + Color.White);
                    }
                    return string.Join("\n", colorStr);
                }
                else
                    return string.Join("\n", Items);
            }
        }

        /// <summary>
        ///     Occurs when a player responds to a dialog by either clicking a button, pressing ENTER/ESC or double-clicking a list
        ///     item.
        /// </summary>
        public new event EventHandler<DialogResponseEventArgs<T>> Response;

        /// <summary>
        ///     Raises the <see cref="Response" /> event.
        /// </summary>
        /// <param name="e">An <see cref="DialogResponseEventArgs" /> that contains the event data.</param>
        public override void OnResponse(DialogResponseEventArgs e)
        {
            if (OpenDialogs.ContainsKey(e.Player.Id))
                OpenDialogs.Remove(e.Player.Id);

            _aSyncWaiter.Fire(e.Player, e);

            DialogResponseEventArgs<T> args = new DialogResponseEventArgs<T>(e.Player, e.DialogId, (int)e.DialogButton, Items[e.ListItem], e.InputText);
            Response?.Invoke(this, args);
        }

        #endregion
    }
}