// SampSharp
// Copyright 2015 Tim Potze
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

namespace SampSharp.GameMode.Helpers
{
    /// <summary>
    ///     Contains helper methods for LINQ queries.
    /// </summary>
    public static class LinqHelper
    {
        /// <summary>Finds the index of the first item matching an expression in an enumerable.</summary>
        /// <typeparam name="T">The <see cref="Type" /> of the collection.</typeparam>
        /// <param name="items">The enumerable to search.</param>
        /// <param name="predicate">The expression to test the items against.</param>
        /// <returns>The index of the first matching item, or -1 if no items match.</returns>
        public static int FindIndex<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            var retVal = 0;
            foreach (var item in items)
            {
                if (predicate(item)) return retVal;
                retVal++;
            }
            return -1;
        }

        /// <summary>Finds the index of the last item matching an expression in an enumerable.</summary>
        /// <typeparam name="T">The <see cref="Type" /> of the collection.</typeparam>
        /// <param name="items">The enumerable to search.</param>
        /// <param name="predicate">The expression to test the items against.</param>
        /// <returns>The index of the last matching item, or -1 if no items match.</returns>
        public static int FindLastIndex<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            for (var i = items.Count() - 1; i >= 0; i--)
            {
                if (predicate(items.ElementAt(i)))
                    return i;
            }
            return -1;
        }

        /// <summary>Finds the index of the first occurrence of an item in an enumerable.</summary>
        /// <typeparam name="T">The <see cref="Type" /> of the collection.</typeparam>
        /// <param name="items">The enumerable to search.</param>
        /// <param name="item">The item to find.</param>
        /// <returns>The index of the first matching item, or -1 if the item was not found.</returns>
        public static int IndexOf<T>(this IEnumerable<T> items, T item)
        {
            return items.FindIndex(i => EqualityComparer<T>.Default.Equals(item, i));
        }

        /// <summary>Finds the index of the last occurrence of an item in an enumerable.</summary>
        /// <typeparam name="T">The <see cref="Type" /> of the collection.</typeparam>
        /// <param name="items">The enumerable to search.</param>
        /// <param name="item">The item to find.</param>
        /// <returns>The index of the last matching item, or -1 if the item was not found.</returns>
        public static int LastIndexOf<T>(this IEnumerable<T> items, T item)
        {
            return items.FindLastIndex(i => EqualityComparer<T>.Default.Equals(item, i));
        }
    }
}