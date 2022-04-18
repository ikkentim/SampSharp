// SampSharp
// Copyright 2022 Tim Potze
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
using System.Linq;
using System.Threading.Tasks;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Tools;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Display;

/// <summary>
///     Represents a list dialog.
/// </summary>
public class ListDialog<T> : Dialog
{
    private readonly List<T> _items = new();
    private readonly ASyncPlayerWaiter<DialogResponseEventArgs<T>> _aSyncWaiter = new();

    /// <summary>
    ///     Initializes a new instance of the Dialog class.
    /// </summary>
    /// <param name="caption">
    ///     The title at the top of the dialog. The length of the caption can not exceed more than 64
    ///     characters before it starts to cut off.
    /// </param>
    /// <param name="button1">The text on the left button.</param>
    /// <param name="button2">The text on the right button. Leave it blank to hide it.</param>
    public ListDialog(string caption, string button1, string button2 = null) : base(DialogStyle.List, caption,
        button1, button2)
    {
    }

    /// <summary>
    ///     Gets the list items.
    /// </summary>
    public IList<T> Items => _items.AsReadOnly();

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
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="items" /> is null.</exception>
    public void AddItems(IEnumerable<T> items)
    {
        if (items == null) throw new ArgumentNullException(nameof(items));

        _items.AddRange(items);
    }
        
    /// <summary>
    ///     Shows the dialog box to a Player asynchronously.
    /// </summary>
    /// <param name="player">The Player to show the dialog to.</param>
    public new async Task<DialogResponseEventArgs<T>> ShowAsync(BasePlayer player)
    {
        Show(player);
            
        return await _aSyncWaiter.Result(player);
    }

    /// <summary>
    ///     Gets the info displayed in the box.
    /// </summary>
    protected override string Info => string.Join("\n", Items.Select(x => x == null ? " " : x.ToString()));

    /// <summary>
    ///     Occurs when a player responds to a dialog by either clicking a button, pressing ENTER/ESC or double-clicking a list
    ///     item.
    /// </summary>
    public new event EventHandler<DialogResponseEventArgs<T>> Response;

    /// <inheritdoc />
    public override void OnResponse(DialogResponseEventArgs e)
    {
        base.OnResponse(e);

        var item = e.ListItem >= 0 && e.ListItem < _items.Count ? _items[e.ListItem] : default;
        var args = new DialogResponseEventArgs<T>(e.Player, e.DialogId, (int)e.DialogButton, e.ListItem, item, e.InputText);
        OnResponse(args);
    }
        
    /// <summary>
    ///     Raises the <see cref="Response" /> event.
    /// </summary>
    /// <param name="e">An <see cref="DialogResponseEventArgs{T}" /> that contains the event data.</param>
    protected virtual void OnResponse(DialogResponseEventArgs<T> e)
    {
        _aSyncWaiter.Fire(e.Player, e);

        Response?.Invoke(this, e);
    }
}