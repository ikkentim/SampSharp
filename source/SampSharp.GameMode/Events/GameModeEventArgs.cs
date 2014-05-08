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

namespace SampSharp.GameMode.Events
{
    /// <summary>
    ///     Provides data for the <see cref="BaseMode.Initialized" /> or <see cref="BaseMode.Exited" />
    ///     event.
    /// </summary>
    public class GameModeEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the GameModeEventArgs class.
        /// </summary>
        public GameModeEventArgs() : this(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the GameModeEventArgs class.
        /// </summary>
        /// <param name="success">Whether the event has been handled successfully.</param>
        public GameModeEventArgs(bool success)
        {
            Success = success;
        }

        /// <summary>
        /// Gets or sets whether this event has been handled sucessfully.
        /// </summary>
        public bool Success { get; set; }
    }
}