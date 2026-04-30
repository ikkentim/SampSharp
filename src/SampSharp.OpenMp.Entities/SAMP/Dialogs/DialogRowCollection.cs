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

/// <summary>Represents a collection of dialog rows.</summary>
/// <typeparam name="T">The type of the dialog rows.</typeparam>
public class DialogRowCollection<T> : IEnumerable<T> where T : IDialogRow
{
    private readonly List<T> _rows = [];
    
    /// <inheritdoc />
    public string RawText => string.Join("\n", _rows.Select(r => r.RawText));

    /// <summary>Gets the number of rows in the list.</summary>
    public int Count => _rows.Count;
    
    /// <inheritdoc />
    public IEnumerator<T> GetEnumerator()
    {
        return _rows.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>Adds the specified row to the list.</summary>
    /// <param name="row">The row to add.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="row" /> is null.</exception>
    public virtual void Add(T row)
    {
        if (row == null)
        {
            throw new ArgumentNullException(nameof(row));
        }

        _rows.Add(row);
    }

    /// <summary>Gets the row at the specified <paramref name="index" />.</summary>
    /// <param name="index">The index.</param>
    /// <returns>The row at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the <paramref name="index" /> is out of the range of the list.</exception>
    public virtual T Get(int index)
    {
        if (index < 0 || index >= _rows.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        return _rows[index];
    }

    /// <summary>Removes all rows from the list.</summary>
    public virtual void Clear()
    {
        _rows.Clear();
    }

    /// <summary>Removes the first occurrence of the specified row to the list.</summary>
    public virtual bool Remove(T row)
    {
        return _rows.Remove(row);
    }
}