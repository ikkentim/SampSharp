// SampSharp
// Copyright (C) 04 Tim Potze
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

using System.Collections;
using System.Collections.Generic;

namespace SampSharp.GameMode.Controllers
{
    /// <summary>
    ///     Represents a list of <see cref="IController" /> instances.
    /// </summary>
    public class ControllerCollection : IEnumerable<IController>
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
        public void Remove<T>()
        {
            _controllers.RemoveAll(c => c is T);
        }
    }
}