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
using SampSharp.Entities.Utilities;

namespace SampSharp.Entities
{
    internal static class EntityTypeNames
    {
        private static readonly Dictionary<Guid, string> Names = new Dictionary<Guid, string>();

        static EntityTypeNames()
        {
            var fields = AttributeScanner.Create()
                .IncludeAllAssemblies()
                .IncludeStatic(true)
                .IncludeAbstract()
                .IncludeNonPublicMembers()
                .ScanFields<EntityTypeAttribute>();

            Names[Guid.Empty] = "Empty";

            foreach (var (field, attribute) in fields)
            {
                var name = attribute.Name;
                if(string.IsNullOrWhiteSpace(name))
                {
                    name = field.Name;
                    if (name.Length > 4 && name.EndsWith("Type"))
                        name = name.Substring(0, name.Length - 4);
                }

                if (field.GetValue(null) is Guid g)
                    Names[g] = name;
            }
        }

        public static string GetTypeName(Guid type)
        {
            return Names.TryGetValue(type, out var name)
                ? name
                : type.ToString();
        }
    }
}