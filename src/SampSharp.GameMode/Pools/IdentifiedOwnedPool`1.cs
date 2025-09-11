﻿// SampSharp
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
    ///     Keeps track of a pool of owned and identified instances.
    /// </summary>
    /// <typeparam name="TInstance">Base type of instances to keep track of.</typeparam>
    /// <typeparam name="TOwner">Base type of the owner</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1000:Do not declare static members on generic types", Justification = "By design")]
    public abstract class IdentifiedOwnedPool<TInstance, TOwner> : Disposable, IIdentifiable, IOwnable<TOwner>
        where TInstance : IdentifiedOwnedPool<TInstance, TOwner>
        where TOwner : class, IIdentifiable
    {
        private static readonly PoolContainer<TInstance> _unownedContainer = new();
        private static readonly Dictionary<TOwner, PoolContainer<TInstance>> _containers = new();
        private int _id;
        private TOwner _owner;

        /// <summary>
        ///     Initializes a new instance of the <see cref="IdentifiedOwnedPool{TInstance, TOwner}" /> class.
        /// </summary>
        protected IdentifiedOwnedPool()
        {
            _id = PoolContainer<TInstance>.UnidentifiedId;
            _unownedContainer.Add(_id, (TInstance) this);
        }


        /// <summary>
        ///     The type to initialize when adding an instance to this pool by id.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S2743:Static fields should not be used in generic types", Justification = "By design")]
        protected static Type InstanceType { get; private set; }


        /// <summary>
        ///     Gets a collection containing all instances.
        /// </summary>
        public static IEnumerable<TInstance> All
            => _containers.SelectMany(c => c.Value).Concat(_unownedContainer).ToArray();

        /// <summary>
        ///     Gets the identifier of this instance.
        /// </summary>
        public int Id
        {
            get => _id;
            protected set
            {
                var pool = GetPool(Owner);
                if (_id == PoolContainer<TInstance>.UnidentifiedId)
                    pool.MoveUnidentified((TInstance) this, value);
                else
                    pool.Move(_id, value);

                _id = value;
            }
        }

        /// <summary>
        ///     Gets the owner of this instance.
        /// </summary>
        public TOwner Owner
        {
            get => _owner;
            protected set
            {
                if (_owner == value)
                    return;

                if (_owner == null)
                {
                    if (_id == PoolContainer<TInstance>.UnidentifiedId)
                        _unownedContainer.RemoveUnidentified((TInstance) this);
                    else
                        _unownedContainer.Remove(_id);
                }
                else
                {
                    if (_id == PoolContainer<TInstance>.UnidentifiedId)
                        _containers[_owner].RemoveUnidentified((TInstance) this);
                    else
                        _containers[_owner].Remove(_id);

                    if (!_containers[_owner].Any())
                        _containers.Remove(_owner);
                }

                GetPool(value).Add(_id, (TInstance) this);

                _owner = value;
            }
        }

        private static PoolContainer<TInstance> GetPool(TOwner owner, bool createIfNotExists = true)
        {
            if (owner == null) return _unownedContainer;

            if (!_containers.TryGetValue(owner, out var pool) && createIfNotExists)
                pool = _containers[owner] = new PoolContainer<TInstance>();

            return pool;
        }

        /// <summary>
        ///     Removes this instance from the pool.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            var pool = GetPool(_owner, false);

            if (pool == null) return;
            if (_id == PoolContainer<TInstance>.UnidentifiedId)
                pool.RemoveUnidentified((TInstance) this);
            else
                pool.Remove(_id);

            if (_id != PoolContainer<TInstance>.UnidentifiedId && !pool.Any())
                _containers.Remove(_owner);
        }

        /// <summary>
        ///     Gets whether the given instance is present in the pool.
        /// </summary>
        /// <param name="item">The instance to check the presence of.</param>
        /// <returns>Whether the given instance is present in the pool.</returns>
        public static bool Contains(TInstance item)
        {
            if (item == null) return false;

            var pool = GetPool(item.Owner, false);
            if (pool == null) return false;

            return item.Id == PoolContainer<TInstance>.UnidentifiedId
                ? pool.ContainsUnidentified(item)
                : pool.Contains(item.Id);
        }

        /// <summary>
        ///     Gets a <see cref="IReadOnlyCollection{T}" /> containing all instances of the given type within this
        ///     <see cref="IdentifiedOwnedPool{TInstance,TOwner}" />.
        /// </summary>
        /// <typeparam name="T2">The <see cref="Type" /> of instances to get.</typeparam>
        /// <returns>All instances of the given type within this <see cref="IdentifiedOwnedPool{TInstance,TOwner}" />.</returns>
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
        ///     Gets a collection of instanced owned by the specified owner.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <returns>A collection of instanced owned by the specified owner</returns>
        public static IEnumerable<TInstance> Of(TOwner owner)
        {
            return (IEnumerable<TInstance>) GetPool(owner, false) ?? new TInstance[0];
        }

        /// <summary>
        ///     Finds an instance with the given <paramref name="id" />".
        /// </summary>
        /// <param name="owner">The owner of the isntance to find.</param>
        /// <param name="id">The identity of the instance to find.</param>
        /// <returns>The found instance.</returns>
        public static TInstance Find(TOwner owner, int id)
        {
            return GetPool(owner, false)?.Get(id);
        }

        /// <summary>
        ///     Initializes a new instance with the given id.
        /// </summary>
        /// <param name="owner">The owner of the instance to create.</param>
        /// <param name="id">The identity of the instance to create.</param>
        /// <returns>
        ///     The initialized instance.
        /// </returns>
        public static TInstance Create(TOwner owner, int id)
        {
            if(InstanceType == null)
                throw new InvalidOperationException($"No instance type has yet been registered to the {typeof(IdentifiedOwnedPool<TInstance,TOwner>)} pool.");

            var instance = (TInstance) Activator.CreateInstance(InstanceType);
            instance.Owner = owner;
            instance.Id = id;
            instance.Initialize();
            return instance;
        }

        /// <summary>
        ///     An overloadable point for initialization logic which requires the <see cref="Id" /> and the <see cref="Owner" /> to be set.
        /// </summary>
        protected virtual void Initialize()
        {

        }

        /// <summary>
        ///     Finds an instance with the given <paramref name="id" /> or initializes a new one.
        /// </summary>
        /// <param name="owner">The owner of the isntance to find or create.</param>
        /// <param name="id">The identity of the instance to find or create.</param>
        /// <returns>
        ///     The found instance.
        /// </returns>
        public static TInstance FindOrCreate(TOwner owner, int id)
        {
            return Find(owner, id) ?? Create(owner, id);
        }

    }
}