// SampSharp
// Copyright (C) 2014 Tim Potze
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.SAMP
{
    /// <summary>
    ///     Contains methods for keeping track of instances.
    /// </summary>
    /// <typeparam name="T">Base type of instances to keep track of.</typeparam>
    public abstract class InstanceKeeper<T> : IDisposable where T : IIdentifyable
    {
        private static readonly List<IIdentifyable> InstanceList = new List<IIdentifyable>();
        private static Type _type;

        /// <summary>
        ///     Initializes a new instance of the <see cref="InstanceKeeper{T}" /> class.
        /// </summary>
        protected InstanceKeeper()
        {
            InstanceList.Add(this as IIdentifyable);
        }

        /// <summary>
        ///     Gets a <see cref="ReadOnlyCollection{T}" /> containing all Instances of type
        ///     <typeparam name="T" />
        ///     .
        /// </summary>
        public static ReadOnlyCollection<T> All
        {
            get { return InstanceList.Cast<T>().ToList().AsReadOnly(); }
        }

        /// <summary>
        ///     Removes this instance from the known instances list.
        /// </summary>
        public virtual void Dispose()
        {
            InstanceList.Remove(this as IIdentifyable);
        }

        /// <summary>
        ///     Finds an instance with the given <paramref name="id" /> or initializes a new one.
        /// </summary>
        /// <param name="id">The identity of the instance to find.</param>
        /// <returns>The found instance.</returns>
        public static T Find(int id)
        {
            return
                (T)
                    (InstanceList.FirstOrDefault(i => i.Id == id) ??
                     Activator.CreateInstance(_type, id));
        }

        /// <summary>
        ///     Finds an instance with the given <paramref name="id" />.
        /// </summary>
        /// <param name="id">The identity of the instance to find.</param>
        /// <returns>The found instance.</returns>
        public static T FindExisting(int id)
        {
            return (T) InstanceList.FirstOrDefault(i => i.Id == id);
        }

        /// <summary>
        ///     Registers the type to use when initializing new instances.
        /// </summary>
        /// <typeparam name="T2">The Type to use when initializing new instances.</typeparam>
        public static void Register<T2>()
        {
            _type = typeof (T2);
        }

        /// <summary>
        ///     Gets a <see cref="ReadOnlyCollection{T}" /> containing all Instances of type
        ///     <typeparam name="T2" />
        /// </summary>
        public static ReadOnlyCollection<T2> GetAll<T2>()
        {
            return InstanceList.OfType<T2>().ToList().AsReadOnly();
        }
    }
}