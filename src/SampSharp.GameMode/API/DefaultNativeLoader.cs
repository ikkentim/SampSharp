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
using System.Collections.Generic;
using System.Linq;

namespace SampSharp.GameMode.API
{
    /// <summary>
    ///     Represents the default SA-MP natives loader.
    /// </summary>
    public class DefaultNativeLoader : INativeLoader
    {
        private readonly Dictionary<int,INative> _handles = new Dictionary<int, INative>(); 

        #region Implementation of INativeLoader

        /// <summary>
        ///     Loads a native with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="sizes">The references to the parameter which contains the size of array parameters.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <returns>
        ///     The loaded native.
        /// </returns>
        public INative Load(string name, int[] sizes, Type[] parameterTypes)
        {
            try
            {
                if (parameterTypes == null || parameterTypes.Length == 0)
                {
                    var handle = InteropProvider.LoadNative(name, string.Empty, null);
                    var native = new DefaultNative(name, handle, parameterTypes);
                    _handles[handle] = native;
                    return native;
                }
                else
                {
                    // Compute the parameter format string.
                    string format;
                    var lengthIndices = ComputeFormatString(parameterTypes, out format);

                    var handle = InteropProvider.LoadNative(name, format, (sizes?.Length ?? 0) == 0 ? lengthIndices : sizes);
                    var native = new DefaultNative(name, handle, parameterTypes);
                    _handles[handle] = native;
                    return native;
                }
            }
            catch (Exception e)
            {
                FrameworkLog.WriteLine(FrameworkMessageLevel.Debug, $"Native load failure ({name}): {e?.Message}");
                return null;
            }
        }
        
        /// <summary>
        /// Gets the native with the specified handle.
        /// </summary>
        /// <param name="handle">The handle of the native.</param>
        /// <returns>The native.</returns>
        public INative Get(int handle)
        {
            INative native;
            return _handles.TryGetValue(handle, out native) ? native : null;
        }
        
        /// <summary>
        ///     Checks whether a native with the specified <paramref name="name" /> exists.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        ///     True if a native with the specified name exists; False otherwise.
        /// </returns>
        public bool Exists(string name)
        {
            return InteropProvider.NativeExists(name);
        }

        #endregion

        private static char GetTypeFormatChar(Type type)
        {
            if (type == typeof(int) || type == typeof(bool) || type == typeof(float))
                return 'd';
            if (type == typeof(int[]) || type == typeof(bool[]) || type == typeof(float[]))
                return 'a';
            if (type == typeof(string))
                return 's';

            throw new ApplicationException("Invalid native delegate argument type");
        }

        private static int[] ComputeFormatString(Type[] types, out string format)
        {
            format =
                types.Select(t => t.IsByRef
                    ? char.ToUpper(GetTypeFormatChar(t.GetElementType()))
                    : GetTypeFormatChar(t))
                    .Aggregate(string.Empty, (current, c) => current + c);

            var sizeReqs = new [] {'S', 'a', 'A'};

            var ir = format.Select((c, i) => sizeReqs.Contains(c) ? i : -1)
                .Where(i => i != -1)
                .ToArray();
            var ie = Enumerable.Range(0, format.Length).Except(ir).ToList();

            var result = new List<int>();
            foreach (var i in ir)
            {
                var j = i + 1;
                while (!ie.Contains(j) && j < format.Length)
                    j++;

                ie.Remove(j);
                result.Add(j);
            }
            return result.ToArray();
        }
    }
}