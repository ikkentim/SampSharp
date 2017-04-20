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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SampSharp.Core.Logging;
using SampSharp.GameMode.Tools;

namespace SampSharp.GameMode.Controllers
{
    /// <summary>
    ///     Represents a list of <see cref="IController" /> instances.
    /// </summary>
    public class ControllerCollection : Disposable, IEnumerable<IController>
    {
        private readonly List<IController> _controllers = new List<IController>();

        /// <summary>
        ///     Returns an enumerator that iterates through this collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.List`1.Enumerator" /> for this collection.
        /// </returns>
        public IEnumerator<IController> GetEnumerator()
        {
            return _controllers.GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through this collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.List`1.Enumerator" /> for this collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Adds a <see cref="IController" /> to this collection.
        /// </summary>
        /// <param name="controller">The <see cref="IController" /> to add to this collection.</param>
        public void Add(IController controller)
        {
            _controllers.Add(controller);
        }

        /// <summary>
        ///     Adds a <see cref="IController" /> to this collection and remove controllers it overrides.
        /// </summary>
        /// <param name="controller">The <see cref="IController" /> to add to this collection.</param>
        public void Override(IController controller)
        {
            var overrides = this.Where(c => c.GetType().GetTypeInfo().IsInstanceOfType(controller)).ToArray();

            if (overrides.Any())
                CoreLog.Log(CoreLogLevel.Debug,
                    $"{controller} overrides {string.Join(", ", (object[]) overrides)}");

            foreach (var c in overrides)
                Remove(c);

            _controllers.Add(controller);
        }

        /// <summary>
        ///     Removes a <see cref="IController" /> from this collection.
        /// </summary>
        /// <param name="controller">The <see cref="IController" /> to remove from this collection.</param>
        public void Remove(IController controller)
        {
            _controllers.RemoveAll(controller.Equals);
        }

        /// <summary>
        ///     Removes all <see cref="IController" /> instances of the given type from this collection.
        /// </summary>
        /// <typeparam name="T">The type to remove from this collection.</typeparam>
        public void Remove<T>() where T : IController
        {
            _controllers.RemoveAll(c => c is T);
        }

        /// <summary>
        ///     Get the first instance of <see cref="IController" /> of the given type.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="IController" /> to find.</typeparam>
        /// <returns>The found instance.</returns>
        public IController Get<T>() where T : IController
        {
            return _controllers.FirstOrDefault(c => c is T);
        }

        /// <summary>
        ///     Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Whether managed resources should be disposed.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var controller in this.OfType<IDisposable>())
                {
                    controller.Dispose();
                }
            }
        }
    }
}