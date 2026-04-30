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

/// <summary>Represents a dialog with a list of selectable rows with columns.</summary>
public class TablistDialog : IDialog<TablistDialogResponse>, IEnumerable<TablistDialogRow>
{
    private TablistDialogRow? _header;

    /// <summary>Initializes a new instance of the <see cref="TablistDialog" /> class.</summary>
    /// <param name="caption">The caption.</param>
    /// <param name="button1">The text on the left button.</param>
    /// <param name="button2">The text on the right button. If the value is <see langword="null" />, the right button is hidden.</param>
    /// <param name="columnCount">The number of columns in this dialog.</param>
    public TablistDialog(string? caption, string? button1, string? button2, int columnCount)
    {
        if (columnCount < 1)
        {
            throw new ArgumentException("Dialog must contain at least 1 column.", nameof(columnCount));
        }

        Caption = caption;
        Button1 = button1;
        Button2 = button2;
        ColumnCount = columnCount;
        Rows = new TablistDialogRowCollection(columnCount);
    }

    /// <summary>Initializes a new instance of the <see cref="TablistDialog" /> class.</summary>
    /// <param name="caption">The caption.</param>
    /// <param name="button1">The text on the left button.</param>
    /// <param name="button2">The text on the right button. If the value is <see langword="null" />, the right button is hidden.</param>
    /// <param name="columnHeaders">The column headers.</param>
    public TablistDialog(string? caption, string? button1, string? button2, string[] columnHeaders)
    {
        ArgumentNullException.ThrowIfNull(columnHeaders);

        if (columnHeaders.Length < 1)
        {
            throw new ArgumentException("Dialog must contain at least 1 column.", nameof(columnHeaders));
        }

        Caption = caption;
        Button1 = button1;
        Button2 = button2;
        ColumnCount = columnHeaders.Length;
        Rows = new TablistDialogRowCollection(columnHeaders.Length);
        Header = new TablistDialogRow(columnHeaders);
    }

    /// <summary>Initializes a new instance of the <see cref="TablistDialog" /> class.</summary>
    /// <param name="caption">The caption.</param>
    /// <param name="button1">The text on the left button.</param>
    /// <param name="button2">The text on the right button. If the value is <see langword="null" />, the right button is hidden.</param>
    /// <param name="columnHeader1">The first column header.</param>
    public TablistDialog(string? caption, string? button1, string? button2, string columnHeader1) : this(caption, button1, button2, [columnHeader1])
    {
    }

    /// <summary>Initializes a new instance of the <see cref="TablistDialog" /> class.</summary>
    /// <param name="caption">The caption.</param>
    /// <param name="button1">The text on the left button.</param>
    /// <param name="button2">The text on the right button. If the value is <see langword="null" />, the right button is hidden.</param>
    /// <param name="columnHeader1">The first column header.</param>
    /// <param name="columnHeader2">The second column header.</param>
    public TablistDialog(string? caption, string? button1, string? button2, string columnHeader1, string columnHeader2) : this(caption, button1, button2, [columnHeader1, columnHeader2
    ])
    {
    }

    /// <summary>Initializes a new instance of the <see cref="TablistDialog" /> class.</summary>
    /// <param name="caption">The caption.</param>
    /// <param name="button1">The text on the left button.</param>
    /// <param name="button2">The text on the right button. If the value is <see langword="null" />, the right button is hidden.</param>
    /// <param name="columnHeader1">The first column header.</param>
    /// <param name="columnHeader2">The second column header.</param>
    /// <param name="columnHeader3">The third column header.</param>
    public TablistDialog(string? caption, string? button1, string? button2, string columnHeader1, string columnHeader2, string columnHeader3) : this(caption,
        button1, button2, [columnHeader1, columnHeader2, columnHeader3])
    {
    }

    /// <summary>Initializes a new instance of the <see cref="TablistDialog" /> class.</summary>
    /// <param name="caption">The caption.</param>
    /// <param name="button1">The text on the left button.</param>
    /// <param name="button2">The text on the right button. If the value is <see langword="null" />, the right button is hidden.</param>
    /// <param name="columnHeader1">The first column header.</param>
    /// <param name="columnHeader2">The second column header.</param>
    /// <param name="columnHeader3">The third column header.</param>
    /// <param name="columnHeader4">The third column header.</param>
    public TablistDialog(string caption, string button1, string button2, string columnHeader1, string columnHeader2, string columnHeader3, string columnHeader4)
        : this(caption, button1, button2, [columnHeader1, columnHeader2, columnHeader3, columnHeader4])
    {
    }

    /// <summary>Gets the rows of this dialog.</summary>
    public TablistDialogRowCollection Rows { get; }

    /// <summary>Gets the number of columns in this tablist dialog.</summary>
    public int ColumnCount { get; }

    /// <summary>Gets or sets the header of this tablist dialog.</summary>
    public TablistDialogRow? Header
    {
        get => _header;
        set
        {
            if (value != null && value.ColumnCount != ColumnCount)
            {
                throw new ArgumentException($"The row must contain exactly {ColumnCount} columns.", nameof(value));
            }

            _header = value;
        }
    }

    DialogStyle IDialog.Style => _header == null
        ? DialogStyle.Tablist
        : DialogStyle.TablistHeaders;

    string IDialog.Content => _header == null
        ? Rows.RawText
        : $"{((IDialogRow)_header).RawText}\n{Rows.RawText}";

    /// <summary>Gets or sets the caption of this tablist dialog.</summary>
    public string? Caption { get; set; }

    /// <summary>Gets or sets the text on the left button of this tablist dialog.</summary>
    public string? Button1 { get; set; }

    /// <summary>Gets or sets the text on the right button of this tablist dialog. If the value is <see langword="null" />, the right button is hidden.</summary>
    public string? Button2 { get; set; }

    TablistDialogResponse IDialog<TablistDialogResponse>.Translate(DialogResult dialogResult)
    {
        var index = dialogResult.ListItem;
        TablistDialogRow? item;
        if (dialogResult.Response == DialogResponse.Disconnected || Rows.Count <= index || index < 0)
        {
            index = -1;
            item = null;
        }
        else
        {
            item = Rows.Get(index);
        }

        return new TablistDialogResponse(dialogResult.Response, index, item);
    }
    
    /// <summary>
    /// Returns an enumerator that iterates through the rows of this tablist dialog.
    /// </summary>
    /// <returns>The enumerator that can be used to iterate through the rows of this tablist dialog.</returns>
    public IEnumerator<TablistDialogRow> GetEnumerator()
    {
        return Rows.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>Adds a row to the list with the specified <paramref name="columns" />.</summary>
    /// <param name="columns">The columns of the row to add.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="columns" /> is null.</exception>
    public void Add(params string[] columns)
    {
        ArgumentNullException.ThrowIfNull(columns);

        Rows.Add(columns);
    }

    /// <summary>Adds a row to the list with the specified <paramref name="columns" /> and <paramref name="tag" />.</summary>
    /// <param name="columns">The columns of the row to add.</param>
    /// <param name="tag">
    /// The tag of the row to add. The tag can be used so associate data with this row which can be used retrieved when the user responds to the
    /// dialog.
    /// </param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="columns" /> is null.</exception>
    public void Add(string[] columns, object tag)
    {
        ArgumentNullException.ThrowIfNull(columns);

        Rows.Add(new TablistDialogRow(columns) { Tag = tag });
    }

    /// <summary>Adds the specified row to the list.</summary>
    /// <param name="row">The row to add.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="row" /> is null.</exception>
    public void Add(TablistDialogRow row)
    {
        Rows.Add(row);
    }
}