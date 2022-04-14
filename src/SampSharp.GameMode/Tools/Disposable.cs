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

namespace SampSharp.GameMode.Tools
{
    /// <summary>
    ///     Defines methods to release allocated resources and to check whether this resource has been disposed.
    /// </summary>
    public abstract class Disposable : IDisposable
    {
        private volatile bool _isDisposed;

        /// <summary>
        ///     Gets whether this resource has been disposed.
        /// </summary>
        public bool IsDisposed
        {
            get => _isDisposed;
            private set => _isDisposed = value;
        }

        /// <summary>
        ///     Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (IsDisposed)
            {
                //We've been disposed already. Abort further dispose call.
                return;
            }
            //Dispose all native and managed resources.
            OnDisposed(true);

            //Remember we've been disposed.
            IsDisposed = true;

            //Suppress finalization; We already disposed our  resources.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="Disposable" /> class.
        /// </summary>
        ~Disposable()
        {
            if (IsDisposed)
            {
                //We've been disposed already. Abort further dispose call.
                return;
            }

            OnDisposed(false);

            //We don't care to set IsDisposed value; Resource is being collected by GC anyways.
        }

        /// <summary>
        ///     Occurs when this instance has been disposed.
        /// </summary>
        public event EventHandler Disposed;

        /// <summary>
        ///     Checks whether this instance has been disposed. If it has, it throws an exception.
        /// </summary>
        /// <exception cref="ObjectDisposedException">Thrown when this instance has been disposed.</exception>
        protected void AssertNotDisposed()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(GetType().FullName);
        }


        /// <summary>
        ///     Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Whether managed resources should be disposed.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Code Smell", "S2953:Methods named \"Dispose\" should implement \"IDisposable.Dispose\"", Justification = "By design")]
        protected abstract void Dispose(bool disposing);

        private void OnDisposed(bool disposing)
        {
            Dispose(disposing);

            Disposed?.Invoke(this, EventArgs.Empty);
        }
    }
}