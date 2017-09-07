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
using System.Reflection;
using SampSharp.Core.Logging;
using SampSharp.GameMode.Tools;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Pools
{
    /// <summary>
    ///     Keeps track of a pool of identifyable instances.
    /// </summary>
    /// <typeparam name="TInstance">Base type of instances to keep track of.</typeparam>
    public abstract class IdentifiedPool<TInstance> : Disposable, IIdentifiable
        where TInstance : IdentifiedPool<TInstance>
    {
        private static readonly PoolContainer<TInstance> Container = new PoolContainer<TInstance>();
        private int _id;


        /// <summary>
        ///     Initializes a new instance of the <see cref="IdentifiedPool{TInstance}" /> class.
        /// </summary>
        protected IdentifiedPool()
        {
            _id = PoolContainer<TInstance>.UnidentifiedId;
            Container.Add(_id, (TInstance) this);
        }

        /// <summary>
        ///     The type to initialize when adding an instance to this pool by id.
        /// </summary>
        // ReSharper disable once StaticMemberInGenericType
        public static Type InstanceType { get; private set; }

        /// <summary>
        ///     Gets a collection containing all instances.
        /// </summary>
        public static IEnumerable<TInstance> All => Container.ToArray();

        /// <summary>
        ///     Gets the identifier of this instance.
        /// </summary>
        public int Id
        {
            get { return _id; }
            protected set
            {
                if (_id == PoolContainer<TInstance>.UnidentifiedId)
                    Container.MoveUnidentified((TInstance) this, value);
                else
                    Container.Move(_id, value);
                _id = value;
            }
        }

        /// <summary>
        ///     Removes this instance from the pool.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (_id == PoolContainer<TInstance>.UnidentifiedId)
                Container.RemoveUnidentified((TInstance) this);
            else
                Container.Remove(_id);
        }

        /// <summary>
        ///     Gets whether the given instance is present in the pool.
        /// </summary>
        /// <param name="item">The instance to check the presence of.</param>
        /// <returns>Whether the given instance is present in the pool.</returns>
        public static bool Contains(TInstance item)
        {
            if (item == null) return false;
            return item.Id == PoolContainer<TInstance>.UnidentifiedId
                ? Container.ContainsUnidentified(item)
                : Container.Contains(item.Id);
        }

        /// <summary>
        ///     Gets a <see cref="IReadOnlyCollection{T}" /> containing all instances of the given type within this
        ///     <see cref="IdentifiedPool{T}" />.
        /// </summary>
        /// <typeparam name="T2">The <see cref="Type" /> of instances to get.</typeparam>
        /// <returns>All instances of the given type within this <see cref="IdentifiedPool{T}" />.</returns>
        public static IEnumerable<T2> GetAll<T2>()
        {
            return All.OfType<T2>();
        }

        /// <summary>
        ///     Registers the type to use when initializing new instances.
        /// </summary>
        /// <typeparam name="TRegister">The <see cref="Type" /> to use when initializing new instances.</typeparam>
        public static void Register<TRegister>() where TRegister : TInstance
        {
            Register(typeof (TRegister));
        }

        /// <summary>
        ///     Registers the type to use when initializing new instances.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if type is null</exception>
        /// <exception cref="System.ArgumentException">type must be of type TInstance;type</exception>
        public static void Register(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (!typeof (TInstance).GetTypeInfo().IsAssignableFrom(type))
                throw new ArgumentException("type must be of type " + typeof (TInstance), nameof(type));

            CoreLog.Log(CoreLogLevel.Debug, $"Type {type} registered to pool.");
            InstanceType = type;
        }

        /// <summary>
        ///     Finds an instance with the given <paramref name="id" />".
        /// </summary>
        /// <param name="id">The identity of the instance to find.</param>
        /// <returns>The found instance.</returns>
        public static TInstance Find(int id)
        {
            return Container.Get(id);
        }

        /// <summary>
        ///     Initializes a new instance with the given id.
        /// </summary>
        /// <param name="id">The identity of the instance to create.</param>
        /// <returns>The initialized instance.</returns>
        public static TInstance Create(int id)
        {
            if (InstanceType == null)
                throw new Exception($"No instance type has yet been registered to the {typeof(IdentifiedPool<TInstance>)} pool.");

            var instance = (TInstance)Activator.CreateInstance(InstanceType);
            instance.Id = id;
            instance.Initialize();
            return instance;
        }

        /// <summary>
        ///     An overloadable point for initialization logic which requires the <see cref="Id"/> to be set.
        /// </summary>
        protected virtual void Initialize()
        {

        }

        /// <summary>
        ///     Finds an instance with the given <paramref name="id" /> or initializes a new one.
        /// </summary>
        /// <param name="id">The identity of the instance to find or create.</param>
        /// <returns>The found instance.</returns>
        public static TInstance FindOrCreate(int id)
        {
            return Find(id) ?? Create(id);
        }
    }
}