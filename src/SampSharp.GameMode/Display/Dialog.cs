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
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;
using SampSharp.GameMode.Tools;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Display;

/// <summary>Represents a SA:MP dialog.</summary>
[SuppressMessage("Minor Code Smell", "S2292:Trivial properties should be auto-implemented",
    Justification = "Used in order to prevent call to virtual members in constructor via direct assignment of backing field.")]
public abstract partial class Dialog
{
    private const int DialogId = 10000;
    private const int DialogHideId = -1;
    private static readonly Dictionary<int, Dialog> _openDialogs = new();
    private readonly ASyncPlayerWaiter<DialogResponseEventArgs> _aSyncWaiter = new();
    private DialogStyle _style;
    private string _button1;
    private string _button2;
    private string _caption;

    /// <summary>Initializes a new instance of the <see cref="Dialog" /> class.</summary>
    /// <param name="style">The style of the dialog.</param>
    /// <param name="caption">The caption of the dialog.</param>
    /// <param name="button1">The text of the left button.</param>
    /// <param name="button2">The text of the right button.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="caption" /> or <paramref name="button1" /> is null.</exception>
    protected Dialog(DialogStyle style, string caption, string button1, string button2)
    {
        _style = style;
        _caption = caption ?? throw new ArgumentNullException(nameof(caption));
        _button1 = button1 ?? throw new ArgumentNullException(nameof(button1));
        _button2 = button2;
    }

    /// <summary>Gets all opened dialogs.</summary>
    public static IEnumerable<Dialog> All => _openDialogs.Values;

    /// <summary>Hides all dialogs for the specified <paramref name="player" />.</summary>
    /// <param name="player">The Player to hide all dialogs from.</param>
    public static void Hide(BasePlayer player)
    {
        if (player == null)
            throw new ArgumentNullException(nameof(player));

        var openDialog = GetOpenDialog(player);

        if (openDialog == null) return;

        _openDialogs.Remove(player.Id);

        openDialog._aSyncWaiter.Cancel(player);

        DialogInternal.Instance.ShowPlayerDialog(player.Id, DialogHideId, (int)DialogStyle.MessageBox, " ", " ", " ", " ");
    }

    /// <summary>Gets the dialog currently being shown to the specified <paramref name="player" />.</summary>
    /// <param name="player">The player whose dialog to get.</param>
    /// <returns>The dialog currently being shown to the specified <paramref name="player" />.</returns>
    public static Dialog GetOpenDialog(BasePlayer player)
    {
        if (player == null)
            throw new ArgumentNullException(nameof(player));

        return _openDialogs.ContainsKey(player.Id)
            ? _openDialogs[player.Id]
            : null;
    }


    /// <summary>Gets or sets the style.</summary>
    public virtual DialogStyle Style
    {
        get => _style;
        set => _style = value;
    }

    /// <summary>Gets or sets the caption.</summary>
    /// <remarks>The length of the caption can not exceed more than 64 characters before it starts to cut off.</remarks>
    public virtual string Caption
    {
        get => _caption;
        set => _caption = value;
    }

    /// <summary>Gets the info displayed in the box.</summary>
    protected abstract string Info { get; }

    /// <summary>Gets or sets the text on the left button.</summary>
    public virtual string Button1
    {
        get => _button1;
        set => _button1 = value;
    }

    /// <summary>Gets or sets the text on the right button.</summary>
    /// <remarks>Leave it blank to hide it.</remarks>
    public virtual string Button2
    {
        get => _button2;
        set => _button2 = value;
    }

    /// <summary>Occurs when a player responds to a dialog by either clicking a button, pressing ENTER/ESC or double-clicking a list item.</summary>
    public event EventHandler<DialogResponseEventArgs> Response;

    /// <summary>Shows the dialog box to a Player.</summary>
    /// <param name="player">The Player to show the dialog to.</param>
    public void Show(BasePlayer player)
    {
        if (player == null)
            throw new ArgumentNullException(nameof(player));

        // Hide previously opened dialogs.
        Hide(player);

        // Store this dialog as the opened dialog.
        _openDialogs[player.Id] = this;


        // Show the dialog to the player.
        DialogInternal.Instance.ShowPlayerDialog(player.Id, DialogId, (int)Style, Caption, Info, Button1, Button2 ?? string.Empty);
    }

    /// <summary>Shows the dialog box to a Player asynchronously.</summary>
    /// <param name="player">The Player to show the dialog to.</param>
    public async Task<DialogResponseEventArgs> ShowAsync(BasePlayer player)
    {
        Show(player);

        return await _aSyncWaiter.Result(player);
    }

    /// <summary>Raises the <see cref="Response" /> event.</summary>
    /// <param name="e">An <see cref="DialogResponseEventArgs" /> that contains the event data. </param>
    public virtual void OnResponse(DialogResponseEventArgs e)
    {
        if (_openDialogs.ContainsKey(e.Player.Id))
            _openDialogs.Remove(e.Player.Id);

        _aSyncWaiter.Fire(e.Player, e);

        Response?.Invoke(this, e);
    }
}