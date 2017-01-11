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

namespace SampSharp.GameMode.Pools
{
    /// <summary>
    ///     Represents the contents of a pool.
    /// </summary>
    /// <typeparam name="TInstance">The type of the instance.</typeparam>
    public sealed class PoolContainer<TInstance> : IEnumerable<TInstance> where TInstance : class
    {
        /// <summary>
        ///     The identifier of an unidentified instance.
        /// </summary>
        public const int UnidentifiedId = -1;

        private readonly Dictionary<int, TInstance> _identifiedItems = new Dictionary<int, TInstance>();
        private readonly List<TInstance> _unidentifiedItems = new List<TInstance>();

        /// <summary>
        ///     Gets the unidentified items.
        /// </summary>
        public IEnumerable<TInstance> UnidentifiedItems => _unidentifiedItems.AsReadOnly();

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<TInstance> GetEnumerator()
        {
            return _identifiedItems.Values.Concat(_unidentifiedItems).GetEnumerator();
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

        /// <summary>
        ///     Adds the specified item with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="item">The item.</param>
        /// <exception cref="System.ArgumentException">duplicate key;key</exception>
        public void Add(int key, TInstance item)
        {
            if (key == UnidentifiedId)
                _unidentifiedItems.Add(item);
            else
            {
                if (_identifiedItems.ContainsKey(key)) throw new ArgumentException("duplicate key", nameof(key));

                _identifiedItems.Add(key, item);
            }
        }

        /// <summary>
        ///     Gets the item associated with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The item associated with the specified key</returns>
        public TInstance Get(int key)
        {
            if (key == UnidentifiedId) return _unidentifiedItems.FirstOrDefault();

            TInstance result;
            _identifiedItems.TryGetValue(key, out result);
            return result;
        }

        /// <summary>
        ///     Removes the item associated with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>True on success; False otherwise.</returns>
        public bool Remove(int key)
        {
            return _identifiedItems.Remove(key);
        }

        /// <summary>
        ///     Removes the specified unidentified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>True on success; False otherwise.</returns>
        public bool RemoveUnidentified(TInstance item)
        {
            if (item == null) return false;

            return _unidentifiedItems.Remove(item);
        }

        /// <summary>
        ///     Moves the item associated with the specified old key to the specified new key.
        /// </summary>
        /// <param name="oldKey">The old key.</param>
        /// <param name="newKey">The new key.</param>
        /// <exception cref="System.ArgumentException">unidentified id cannot be moved;oldKey or key not found;oldKey</exception>
        public void Move(int oldKey, int newKey)
        {
            if (oldKey == newKey) return;
            if (oldKey == UnidentifiedId)
                throw new ArgumentException("unidentified id cannot be moved", nameof(oldKey));

            var item = Get(oldKey);

            if (item == null) throw new ArgumentException("key not found", nameof(oldKey));
            Remove(oldKey);
            Add(newKey, item);
        }

        /// <summary>
        ///     Moves the specified unidentified item to the specified new key.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="newKey">The new key.</param>
        /// <exception cref="System.ArgumentNullException">item</exception>
        /// <exception cref="System.ArgumentException">item not found;item</exception>
        public void MoveUnidentified(TInstance item, int newKey)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (UnidentifiedId == newKey) return;

            if (!RemoveUnidentified(item))
                throw new ArgumentException("item not found", nameof(item));
            Add(newKey, item);
        }

        /// <summary>
        ///     Determines whether the specified key exists.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>True if the specified key exists; False otherwise.</returns>
        public bool Contains(int key)
        {
            return _identifiedItems.ContainsKey(key);
        }

        /// <summary>
        ///     Determines whether the specified unidentified item exists within this pool.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>True if the specified unidentified item exists; False otherwise.</returns>
        public bool ContainsUnidentified(TInstance item)
        {
            return _unidentifiedItems.Contains(item);
        }
    }
}