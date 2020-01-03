// SampSharp
// Copyright 2019 Tim Potze
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

namespace SampSharp.Entities
{
    public class SystemRegistry : ISystemRegistry
    {
        private readonly Dictionary<Type, CacheEntry> _data = new Dictionary<Type, CacheEntry>
        {
            {typeof(ISystem), new CacheEntry()}
        };

        /// <inheritdoc />
        public void Add(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!typeof(ISystem).IsAssignableFrom(type))
                throw new ArgumentException("Type must implement ISystem", nameof(type));

            foreach (var kv in _data)
                if (kv.Key.IsAssignableFrom(type))
                    kv.Value.All.Add(type);
        }

        /// <inheritdoc />
        public IEnumerable<Type> Get(Type type, bool cache = false)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (_data.TryGetValue(type, out var entry))
                return entry.ReadOnly;

            if (!cache)
                return _data[typeof(ISystem)].All.Where(type.IsAssignableFrom);

            entry = new CacheEntry();
            entry.All.AddRange(_data[typeof(ISystem)].All.Where(type.IsAssignableFrom));
            _data[type] = entry;

            return entry.ReadOnly;
        }

        private class CacheEntry
        {
            public CacheEntry()
            {
                ReadOnly = All.AsReadOnly();
            }

            public List<Type> All { get; } = new List<Type>();

            public IReadOnlyCollection<Type> ReadOnly { get; }
        }
    }
}