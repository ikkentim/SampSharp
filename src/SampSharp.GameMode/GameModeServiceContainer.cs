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
using System.Reflection;

namespace SampSharp.GameMode
{
    /// <summary>
    ///     A collection of game mode services.
    /// </summary>
    public class GameModeServiceContainer : IServiceProvider
    {
        private readonly Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

        #region Implementation of IGameServiceProvider

        /// <summary>
        ///     Gets the service object of the specified type.
        /// </summary>
        /// <param name="serviceType">An object that specifies the type of service object to get.</param>
        /// <returns>
        ///     A service object of type <paramref name="serviceType" />.-or- null if there is no service object of type
        ///     <paramref name="serviceType" />.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">serviceType</exception>
        public object GetService(Type serviceType)
        {
            if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));

            return _services.ContainsKey(serviceType) ? _services[serviceType] : null;
        }

        #endregion

        /// <summary>
        ///     Adds the service of the specified <paramref name="serviceType" />.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="service">The service.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="serviceType"/> or <paramref name="service"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="serviceType"/> must be of type IService</exception>
        public void AddService(Type serviceType, IService service)
        {
            if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));
            if (service == null) throw new ArgumentNullException(nameof(service));
            if (!typeof (IService).GetTypeInfo().IsAssignableFrom(serviceType))
                throw new ArgumentException("serviceType must be of type IService");
            if (!serviceType.GetTypeInfo().IsInstanceOfType(service))
                throw new ArgumentException("service must be instance of type serviceType");

            _services[serviceType] = service;
        }

        /// <summary>
        ///     Adds the service of the specified <typeparamref name="TServiceType" />.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <typeparam name="TServiceType">Type of the service.</typeparam>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="service"/> is null.</exception>
        /// <exception cref="System.ArgumentException">serviceType must be of type IService</exception>
        public void AddService<TServiceType>(TServiceType service) where TServiceType : IService
        {
            AddService(typeof (TServiceType), service);
        }

        /// <summary>
        ///     Gets the service object of the specified type.
        /// </summary>
        /// <typeparam name="TServiceType">The type of service object to get.</typeparam>
        /// <returns>
        ///     A service object of type <typeparamref name="TServiceType" />.-or- null if there is no service object of type
        ///     <typeparamref name="TServiceType" />
        /// </returns>
        public TServiceType GetService<TServiceType>() where TServiceType : IService
        {
            return (TServiceType) GetService(typeof (TServiceType));
        }
    }
}