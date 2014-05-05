// SampSharp
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

using System;
using System.Collections.Generic;
using System.Linq;

namespace SampSharp.GameMode
{
    public static class LinqExtention
    {
        /// <summary>Finds the index of the first item matching an expression in an enumerable.</summary>
        /// <param name="items">The enumerable to search.</param>
        /// <param name="predicate">The expression to test the items against.</param>
        /// <returns>The index of the first matching item, or -1 if no items match.</returns>
        public static int FindIndex<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            if (items == null) throw new ArgumentNullException("items");
            if (predicate == null) throw new ArgumentNullException("predicate");

            int retVal = 0;
            foreach (var item in items)
            {
                if (predicate(item)) return retVal;
                retVal++;
            }
            return -1;
        }

        /// <summary>Finds the index of the last item matching an expression in an enumerable.</summary>
        /// <param name="items">The enumerable to search.</param>
        /// <param name="predicate">The expression to test the items against.</param>
        /// <returns>The index of the last matching item, or -1 if no items match.</returns>
        public static int FindLastIndex<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            if (items == null) throw new ArgumentNullException("items");
            if (predicate == null) throw new ArgumentNullException("predicate");

            for (int i = items.Count() - 1; i >= 0; i--)
            {
                if (predicate(items.ElementAt(i)))
                    return i;
            }
            return -1;
        }

        /// <summary>Finds the index of the first occurence of an item in an enumerable.</summary>
        /// <param name="items">The enumerable to search.</param>
        /// <param name="item">The item to find.</param>
        /// <returns>The index of the first matching item, or -1 if the item was not found.</returns>
        public static int IndexOf<T>(this IEnumerable<T> items, T item)
        {
            return items.FindIndex(i => EqualityComparer<T>.Default.Equals(item, i));
        }

        /// <summary>Finds the index of the last occurence of an item in an enumerable.</summary>
        /// <param name="items">The enumerable to search.</param>
        /// <param name="item">The item to find.</param>
        /// <returns>The index of the last matching item, or -1 if the item was not found.</returns>
        public static int LastIndexOf<T>(this IEnumerable<T> items, T item)
        {
            return items.FindLastIndex(i => EqualityComparer<T>.Default.Equals(item, i));
        }
    }
}