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

namespace SampSharp.Entities.SAMP;

/// <summary>Represents a collection of dialog rows of type <see cref="TablistDialogRow" />.</summary>
public class TablistDialogRowCollection : DialogRowCollection<TablistDialogRow>
{
    private readonly int _columnCount;

    /// <summary>Initializes a new instance of the <see cref="TablistDialogRowCollection" /> class.</summary>
    /// <param name="columnCount">The required number of columns.</param>
    public TablistDialogRowCollection(int columnCount)
    {
        _columnCount = columnCount;
    }

    /// <summary>Adds a row to the list with the specified <paramref name="columns" />.</summary>
    /// <param name="columns">The columns of the row to add.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="columns" /> is null.</exception>
    public void Add(params string[] columns)
    {
        ArgumentNullException.ThrowIfNull(columns);

        Add(new TablistDialogRow(columns));
    }

    /// <summary>Adds the specified row to the list.</summary>
    /// <param name="row">The row to add.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="row" /> is null.</exception>
    /// <exception cref="ArgumentException">Thrown if the row does not contain the required number of columns.</exception>
    public override void Add(TablistDialogRow row)
    {
        ArgumentNullException.ThrowIfNull(row);

        if (row.ColumnCount != _columnCount)
        {
            throw new ArgumentException($"The row must contain exactly {_columnCount} columns.", nameof(row));
        }

        base.Add(row);
    }
}