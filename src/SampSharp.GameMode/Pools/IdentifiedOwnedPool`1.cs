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

using System;
using System.Linq;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Pools
{
    /// <summary>
    ///     Keeps track of a pool of owned and identified instances.
    /// </summary>
    /// <typeparam name="T">Base type of instances to keep track of.</typeparam>
    public abstract class IdentifiedOwnedPool<T> : Pool<T> where T : class, IIdentifiable, IOwnable<GtaPlayer>
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
        public static T Find(GtaPlayer owner, int id)
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
        public static T Add(GtaPlayer owner, int id)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");

            return (T) Activator.CreateInstance(InstanceType, owner, id);
        }

        /// <summary>
        ///     Finds an instance with the given <paramref name="owner" /> and <paramref name="id" /> or initializes a new one.
        /// </summary>
        /// <param name="owner">The owner of the instance to find or create.</param>
        /// <param name="id">The identity of the instance to find or create.</param>
        /// <returns>The found instance.</returns>
        public static T FindOrCreate(GtaPlayer owner, int id)
        {
            return Find(owner, id) ?? Add(owner, id);
        }
    }
}