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

using System.Collections;

namespace SampSharp.Entities.SAMP;

/// <summary>Represents a dialog with a list of selectable rows.</summary>
/// <param name="caption">The caption.</param>
/// <param name="button1">The text on the left button.</param>
/// <param name="button2">The text on the right button. If the value is <see langword="null" />, the right button is hidden.</param>
public class ListDialog(string caption, string? button1, string? button2 = null) : IDialog<ListDialogResponse>, IEnumerable<ListDialogRow>
{

    /// <summary>Gets the rows of this dialog.</summary>
    public ListDialogRowCollection Rows { get; } = [];

    DialogStyle IDialog.Style => DialogStyle.List;

    string IDialog.Content => Rows.RawText;

    /// <summary>Gets or sets the caption of this list dialog.</summary>
    public string? Caption { get; set; } = caption;

    /// <summary>Gets or sets the text on the left button of this list dialog.</summary>
    public string? Button1 { get; set; } = button1;

    /// <summary>Gets or sets the text on the right button of this list dialog. If the value is <see langword="null" />, the right button is hidden.</summary>
    public string? Button2 { get; set; } = button2;

    ListDialogResponse IDialog<ListDialogResponse>.Translate(DialogResult dialogResult)
    {
        var index = dialogResult.ListItem;
        ListDialogRow? item;
        if (dialogResult.Response == DialogResponse.Disconnected || Rows.Count <= index || index < 0)
        {
            index = -1;
            item = null;
        }
        else
        {
            item = Rows.Get(index);
        }

        return new ListDialogResponse(dialogResult.Response, index, item);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the rows of this list dialog.
    /// </summary>
    /// <returns>The enumerator that can be used to iterate through the rows of this list dialog.</returns>
    public IEnumerator<ListDialogRow> GetEnumerator()
    {
        return Rows.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>Adds a row to the list with the specified <paramref name="text" />.</summary>
    /// <param name="text">The text of the row to add.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="text" /> is null.</exception>
    public void Add(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        Rows.Add(text);
    }

    /// <summary>Adds a row to the list with the specified <paramref name="text" /> and <paramref name="tag" />.</summary>
    /// <param name="text">The text of the row to add.</param>
    /// <param name="tag">
    /// The tag of the row to add. The tag can be used so associate data with this row which can be used retrieved when the user responds to the
    /// dialog.
    /// </param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="text" /> is null.</exception>
    public void Add(string text, object? tag)
    {
        ArgumentNullException.ThrowIfNull(text);

        Rows.Add(new ListDialogRow(text) { Tag = tag });
    }

    /// <summary>Adds the specified row to the list.</summary>
    /// <param name="row">The row to add.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="row" /> is null.</exception>
    public void Add(ListDialogRow row)
    {
        Rows.Add(row);
    }
}