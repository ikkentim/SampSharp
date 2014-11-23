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

namespace SampSharp.GameMode.Tools
{
    /// <summary>
    ///     Defines methods to release allocated resources and to check whether this resource has been disposed.
    /// </summary>
    public abstract class Disposable : IDisposable
    {
        /// <summary>
        ///     Gets whether this resource has been disposed.
        /// </summary>
        public bool Disposed { get; private set; }

        /// <summary>
        ///     Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (Disposed)
            {
                //We've been desposed already. Abort further disposure.
                return;
            }
            //Dispose all native and managed resources.
            Dispose(true);

            //Remember we've been disposed.
            Disposed = true;

            //Suppress finalisation; We already disposed our  resources.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Deintializes this instance of Disposable.
        /// </summary>
        ~Disposable()
        {
            if (Disposed)
            {
                //We've been desposed already. Abort further disposure.
                return;
            }

            Dispose(false);

            //We don't care to set Disposed value; Resource is being collected by GC anyways.
        }

        /// <summary>
        /// Checks whether this instance has been disposed. If it has, it throws an exception.
        /// </summary>
        /// <exception cref="ObjectDisposedException">Thrown when this instance has been disposed.</exception>
        protected void CheckDisposure()
        {
            if (Disposed)
                throw new ObjectDisposedException("Cannot access disposed resource.");
        }

        /// <summary>
        ///     Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Whether managed resources should be disposed.</param>
        protected abstract void Dispose(bool disposing);
    }
}