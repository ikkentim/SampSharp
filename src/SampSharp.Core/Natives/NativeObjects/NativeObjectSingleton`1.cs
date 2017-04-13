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

namespace SampSharp.Core.Natives.NativeObjects
{
    /// <summary>
    ///     Provides a singleton <see cref="Instance" /> property containing a single instance of the specified native object
    ///     type.
    /// </summary>
    /// <typeparam name="T">The native object type.</typeparam>
    public abstract class NativeObjectSingleton<T> where T : NativeObjectSingleton<T>
    {
        private static T _instance;

        /// <summary>
        ///     Gets the singleton instance of native object <typeparamref name="T" />.
        /// </summary>
        public static T Instance => _instance ?? (_instance = NativeObjectProxyFactory.CreateInstance<T>());
    }
}