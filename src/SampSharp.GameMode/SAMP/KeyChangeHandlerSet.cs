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

using SampSharp.GameMode.Events;
using SampSharp.GameMode.Tools;

namespace SampSharp.GameMode.SAMP
{
    /// <summary>
    ///     Contains a set of KeyHandlers for different keystates.
    /// </summary>
    public class KeyChangeHandlerSet
    {
        /// <summary>
        ///     Initializes a new instance of the KeyChangeHandlerSet class.
        /// </summary>
        public KeyChangeHandlerSet()
        {
            Pressed = new KeyHandlerSet(KeyUtils.HasPressed);
            Released = new KeyHandlerSet(KeyUtils.HasReleased);
        }

        /// <summary>
        ///     Gets a set of KeyHandlers which are triggered once a key has been pressed.
        /// </summary>
        public KeyHandlerSet Pressed { get; private set; }

        /// <summary>
        ///     Gets a set of KeyHandlers which are triggered once a key has been released.
        /// </summary>
        public KeyHandlerSet Released { get; private set; }

        /// <summary>
        ///     Handles a change in PlayerKeyState.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Object containing information about the event.</param>
        public void Handle(object sender, PlayerKeyStateChangedEventArgs e)
        {
            Pressed.Handle(sender, e);
            Released.Handle(sender, e);
        }
    }
}