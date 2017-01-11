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

namespace SampSharp.GameMode.Helpers
{
    /// <summary>
    ///     Contains helper methods for nullable types.
    /// </summary>
    internal static class NullableHelper
    {
        /// <summary>
        ///     Invokes Does the specified action when the value is set.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="action">The action.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Do<T>(this T? value, Action<T> action) where T : struct
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            if (value.HasValue) action(value.Value);
        }
    }
}