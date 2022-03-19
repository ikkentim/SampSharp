// SampSharp
// Copyright 2020 Tim Potze
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
    /// <summary>
    /// Represents a registry which contains all enabled system types.
    /// </summary>
    /// <seealso cref="ISystemRegistry" />
    public class SystemRegistry : ISystemRegistry
    {
        private readonly IServiceProvider _serviceProvider;

        private Dictionary<Type, ISystem[]> _data;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemRegistry" /> class.
        /// </summary>
        public SystemRegistry(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public void SetAndLock(Type[] types)
        {
            if (types == null) throw new ArgumentNullException(nameof(types));
            if(_data != null) throw new SystemRegistryException("The system registry has been locked an cannot be modified.");

            var data = new Dictionary<Type, HashSet<ISystem>>();

            foreach (var type in types)
            {
                if(!(_serviceProvider.GetService(type) is ISystem instance))
                    throw new SystemRegistryException($"System of type {type} could not be found in the service provider.");

                var currentType = type;

                while (currentType != null && currentType != typeof(object))
                {
                    if (!data.TryGetValue(currentType, out var set))
                        data[currentType] = set = new HashSet<ISystem>();

                    set.Add(instance);

                    currentType = currentType.BaseType;
                }

                foreach (var interfaceType in type.GetInterfaces().Where(t => typeof(ISystem).IsAssignableFrom(t)))
                {
                    if (!data.TryGetValue(interfaceType, out var set))
                        data[interfaceType] = set = new HashSet<ISystem>();

                    set.Add(instance);
                }
            }
            
            // Convert hash sets to arrays.
            _data = new Dictionary<Type, ISystem[]>();
            foreach (var kv in data)
            {
                _data[kv.Key] = kv.Value.ToArray();
            }
        }
        
        /// <inheritdoc />
        public ISystem[] Get(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!_data.TryGetValue(type, out var value))
                return Array.Empty<ISystem>();

            var result = new ISystem[value.Length];
            Array.Copy(value, result, value.Length);
            return result;
        }
        
        /// <inheritdoc />
        public TSystem[] Get<TSystem>() where TSystem : ISystem
        {
            if (!_data.TryGetValue(typeof(TSystem), out var value))
                return Array.Empty<TSystem>();

            var result = new TSystem[value.Length];
            Array.Copy(value, result, value.Length);
            return result;
        }
    }
}