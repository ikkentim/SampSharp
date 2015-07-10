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
using System.Linq;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Pools
{
    /// <summary>
    ///     Keeps track of a pool of owned and identified instances.
    /// </summary>
    /// <typeparam name="TInstance">Base type of instances to keep track of.</typeparam>
    /// <typeparam name="TOwner">Base type of the owner</typeparam>
    public abstract class IdentifiedOwnedPool<TInstance, TOwner> : Pool<TInstance>
        where TInstance : class, IIdentifiable, IOwnable<TOwner> where TOwner : IdentifiedPool<TOwner>, IIdentifiable
    {
        /// <summary>
        ///     The type to initialize when adding an instance to this pool by id.
        /// </summary>
        protected static Type InstanceType;

        /// <summary>
        ///     Registers the type to use when initializing new instances.
        /// </summary>
        /// <typeparam name="T2">The Type to use when initializing new instances.</typeparam>
        public static void Register<T2>()
        {
            InstanceType = typeof (T2);
        }

        /// <summary>
        ///     Finds an instance with the given <paramref name="owner" /> and <paramref name="id" />".
        /// </summary>
        /// <param name="owner">The owner of the instance to find.</param>
        /// <param name="id">The identity of the instance to find.</param>
        /// <returns>The found instance.</returns>
        public static TInstance Find(TOwner owner, int id)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");

            return All.FirstOrDefault(i => i.Owner == owner && i.Id == id);
        }

        /// <summary>
        ///     Initializes a new instance with the given <paramref name="owner" /> and <paramref name="id" />.
        /// </summary>
        /// <param name="owner">The owner of the instance to create.</param>
        /// <param name="id">The identity of the instance to create.</param>
        /// <returns>The initialized instance.</returns>
        public static TInstance Add(TOwner owner, int id)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");

            return (TInstance) Activator.CreateInstance(InstanceType, owner, id);
        }

        /// <summary>
        ///     Finds an instance with the given <paramref name="owner" /> and <paramref name="id" /> or initializes a new one.
        /// </summary>
        /// <param name="owner">The owner of the instance to find or create.</param>
        /// <param name="id">The identity of the instance to find or create.</param>
        /// <returns>The found instance.</returns>
        public static TInstance FindOrCreate(TOwner owner, int id)
        {
            return Find(owner, id) ?? Add(owner, id);
        }
    }
}