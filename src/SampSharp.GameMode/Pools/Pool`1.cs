// SampSharp
// Copyright (C) 2015 Tim Potze
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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SampSharp.GameMode.Tools;

namespace SampSharp.GameMode.Pools
{
    /// <summary>
    ///     Keeps track of a pool of instances.
    /// </summary>
    /// <typeparam name="T">Base type of instances to keep track of.</typeparam>
    public abstract class Pool<T> : Disposable
    {
        /// <summary>
        ///     The instances alive in this pool.
        /// </summary>
        protected static readonly List<object> Instances = new List<object>();

        /// <summary>
        ///     A readonly collection of the instances in this pool.
        /// </summary>
        protected static ReadOnlyCollection<T> ReadOnly = new ReadOnlyCollection<T>(new List<T>());

        /// <summary>
        ///     A Locker for tread-saving this pool.
        /// </summary>
        protected static object Lock = new object();

        /// <summary>
        ///     Initializes a new instance of the <see cref="Pool{T}" /> class.
        /// </summary>
        protected Pool()
        {
            lock (Lock)
            {
                Instances.Add(this);
                ReadOnly = Instances.OfType<T>().ToList().AsReadOnly();
            }
        }

        /// <summary>
        ///     Gets a <see cref="ReadOnlyCollection{T}" /> containing all instances of type.
        /// </summary>
        public static ReadOnlyCollection<T> All
        {
            get
            {
                lock (Lock)
                {
                    return ReadOnly;
                }
            }
        }

        /// <summary>
        ///     Removes this instance from the pool.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            lock (Lock)
            {
                Instances.Remove(this);
                ReadOnly = Instances.OfType<T>().ToList().AsReadOnly();
            }
        }

        /// <summary>
        ///     Gets whether the given instance is present in the pool.
        /// </summary>
        /// <param name="item">The instance to check the presence of.</param>
        /// <returns>Whether the given instance is present in the pool.</returns>
        public static bool Contains(T item)
        {
            lock (Lock)
            {
                return Instances.Contains(item);
            }
        }

        /// <summary>
        ///     Gets a <see cref="ReadOnlyCollection{T}" /> containing all instances of type
        ///     <typeparam name="T2" />
        ///     within this pool.
        /// </summary>
        public static ReadOnlyCollection<T2> GetAll<T2>()
        {
            lock (Lock)
            {
                return Instances.OfType<T2>().ToList().AsReadOnly();
            }
        }
    }
}