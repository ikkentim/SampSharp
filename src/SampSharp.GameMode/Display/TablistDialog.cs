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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SampSharp.GameMode.Definitions;

namespace SampSharp.GameMode.Display
{
    /// <summary>
    ///     Represents a tablist (table) dialog.
    /// </summary>
    public class TablistDialog : Dialog, IList<IEnumerable<string>>
    {
        private readonly int _columnCount;
        private readonly string[] _columns;
        private readonly List<string> _rows = new List<string>();

        /// <summary>
        ///     Initializes a new instance of the Dialog class.
        /// </summary>
        /// <param name="caption">
        ///     The title at the top of the dialog. The length of the caption can not exceed more than 64
        ///     characters before it starts to cut off.
        /// </param>
        /// <param name="columnCount">The column count.</param>
        /// <param name="button1">The text on the left button.</param>
        /// <param name="button2">The text on the right button. Leave it blank to hide it.</param>
        public TablistDialog(string caption, int columnCount, string button1, string button2 = null)
        {
            if (caption == null) throw new ArgumentNullException(nameof(caption));
            if (button1 == null) throw new ArgumentNullException(nameof(button1));
            if (columnCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(columnCount), "must be greater than 0");

            Caption = caption;
            Button1 = button1;
            Button2 = button2;
            _columnCount = columnCount;
            Style = DialogStyle.Tablist;
        }

        /// <summary>
        ///     Initializes a new instance of the Dialog class.
        /// </summary>
        /// <param name="caption">
        ///     The title at the top of the dialog. The length of the caption can not exceed more than 64
        ///     characters before it starts to cut off.
        /// </param>
        /// <param name="columns">The columns.</param>
        /// <param name="button1">The text on the left button.</param>
        /// <param name="button2">The text on the right button. Leave it blank to hide it.</param>
        public TablistDialog(string caption, IEnumerable<string> columns, string button1, string button2 = null)
        {
            if (caption == null) throw new ArgumentNullException(nameof(caption));
            if (button1 == null) throw new ArgumentNullException(nameof(button1));

            Caption = caption;
            Button1 = button1;
            Button2 = button2;
            if (columns == null) throw new ArgumentNullException(nameof(columns));
            Style = DialogStyle.TablistHeaders;

            _columns = columns.ToArray();
            _columnCount = _columns.Length;
        }

        #region Overrides of Dialog

        /// <summary>
        ///     Gets the info displayed in the box.
        /// </summary>
        protected override string Info
        {
            get
            {
                return (_columns == null ? string.Empty : string.Join("\t", _columns) + "\n") +
                       string.Join("\n", _rows.Select(r => r ?? string.Empty));
            }
        }

        #endregion

        /// <summary>
        ///     Adds a row with the specified cells.
        /// </summary>
        /// <param name="cells">The cells of the row.</param>
        public void Add(params string[] cells)
        {
            Add(cells as IEnumerable<string>);
        }

        #region Implementation of IEnumerable

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<IEnumerable<string>> GetEnumerator()
        {
            return _rows.Select(row => row.Split('\t')).Cast<IEnumerable<string>>().GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<IEnumerable<string>>

        /// <summary>
        ///     Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="T:System.Collections.Generic.ICollection`1" /> is
        ///     read-only.
        /// </exception>
        public void Add(IEnumerable<string> item)
        {
            if (item == null)
            {
                _rows.Add(null);
                return;
            }

            var row = item.ToArray();

            if (row.Length != _columnCount)
                throw new ArgumentException($"Row must contain {_columnCount} cells.", nameof(item));

            _rows.Add(string.Join("\t", row));
        }

        /// <summary>
        ///     Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="T:System.Collections.Generic.ICollection`1" /> is
        ///     read-only.
        /// </exception>
        public void Clear()
        {
            _rows.Clear();
        }

        /// <summary>
        ///     Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
        /// </summary>
        /// <returns>
        ///     true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />;
        ///     otherwise, false.
        /// </returns>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        public bool Contains(IEnumerable<string> item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            return _rows.Contains(string.Join("\t", item));
        }

        /// <summary>
        ///     Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an
        ///     <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied
        ///     from <see cref="T:System.Collections.Generic.ICollection`1" />. The <see cref="T:System.Array" /> must have
        ///     zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="array" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex" /> is less than 0.</exception>
        /// <exception cref="T:System.ArgumentException">
        ///     The number of elements in the source
        ///     <see cref="T:System.Collections.Generic.ICollection`1" /> is greater than the available space from
        ///     <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.
        /// </exception>
        public void CopyTo(IEnumerable<string>[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), arrayIndex, "arrayIndex is less than 0.");

            if (array.Length < arrayIndex + _rows.Count)
                throw new ArgumentException(
                    "The number of elements in the collection is greater than the available space from arrayIndex to the end of the destination array.",
                    nameof(array));

            for (var i = 0; i < _rows.Count; i++)
                array[i + arrayIndex] = _rows[i].Split('\t');
        }

        /// <summary>
        ///     Removes the first occurrence of a specific object from the
        ///     <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <returns>
        ///     true if <paramref name="item" /> was successfully removed from the
        ///     <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if
        ///     <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </returns>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
        /// <exception cref="T:System.NotSupportedException">
        ///     The <see cref="T:System.Collections.Generic.ICollection`1" /> is
        ///     read-only.
        /// </exception>
        public bool Remove(IEnumerable<string> item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            return _rows.Remove(string.Join("\t", item));
        }

        /// <summary>
        ///     Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <returns>
        ///     The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </returns>
        public int Count => _rows.Count;

        /// <summary>
        ///     Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
        /// </summary>
        /// <returns>
        ///     true if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, false.
        /// </returns>
        public bool IsReadOnly => false;

        #endregion

        #region Implementation of IList<IEnumerable<string>>

        /// <summary>
        ///     Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1" />.
        /// </summary>
        /// <returns>
        ///     The index of <paramref name="item" /> if found in the list; otherwise, -1.
        /// </returns>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1" />.</param>
        public int IndexOf(IEnumerable<string> item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            return _rows.IndexOf(string.Join("\t", item));
        }

        /// <summary>
        ///     Inserts an item to the <see cref="T:System.Collections.Generic.IList`1" /> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref="T:System.Collections.Generic.IList`1" />.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> is not a valid index in the
        ///     <see cref="T:System.Collections.Generic.IList`1" />.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IList`1" /> is read-only.</exception>
        public void Insert(int index, IEnumerable<string> item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _rows.Insert(index, string.Join("\t", item));
        }

        /// <summary>
        ///     Removes the <see cref="T:System.Collections.Generic.IList`1" /> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> is not a valid index in the
        ///     <see cref="T:System.Collections.Generic.IList`1" />.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IList`1" /> is read-only.</exception>
        public void RemoveAt(int index)
        {
            _rows.RemoveAt(index);
        }

        /// <summary>
        ///     Gets or sets the element at the specified index.
        /// </summary>
        /// <returns>
        ///     The element at the specified index.
        /// </returns>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="index" /> is not a valid index in the
        ///     <see cref="T:System.Collections.Generic.IList`1" />.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        ///     The property is set and the
        ///     <see cref="T:System.Collections.Generic.IList`1" /> is read-only.
        /// </exception>
        public IEnumerable<string> this[int index]
        {
            get { return _rows[index].Split('\t'); }
            set
            {
                var row = value.ToArray();

                if (row.Length != _columnCount)
                    throw new ArgumentException($"Row must contain {_columnCount} cells.", "item");

                if (row.Any(cell => cell.Contains('\t')))
                    throw new ArgumentException("No cell can contain a TAB character.", "item");
                _rows[index] = string.Join("\t", row);
            }
        }

        #endregion
    }
}