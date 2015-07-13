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

namespace SampSharp.GameMode.API
{
    [AttributeUsage(AttributeTargets.Field)]
    public class NativeAttribute : Attribute
    {
        public NativeAttribute(string name) : this(name, null)
        {
        }

        public NativeAttribute(string name, params int[] sizes)
        {
            if (name == null) throw new ArgumentNullException("name");
            Name = name;
            Sizes = sizes;
        }

        public string Name { get; private set; }
        public int[] Sizes { get; private set; }
    }
}