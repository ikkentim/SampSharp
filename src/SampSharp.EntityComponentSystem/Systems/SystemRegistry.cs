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

namespace SampSharp.EntityComponentSystem.Systems
{
    public class SystemRegistry : ISystemRegistry
    {
        private readonly List<Type> _configuringSystems = new List<Type>();
        private readonly List<Type> _systems = new List<Type>();

        public void Add(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!typeof(ISystem).IsAssignableFrom(type))
                throw new ArgumentException("Type must implement ISystem", nameof(type));

            _systems.Add(type);

            if (typeof(IConfiguringSystem).IsAssignableFrom(type))
                _configuringSystems.Add(type);
        }

        public IEnumerable<Type> ConfiguringSystems => _configuringSystems.AsReadOnly();

        public IEnumerable<Type> Systems => _systems.AsReadOnly();
    }
}