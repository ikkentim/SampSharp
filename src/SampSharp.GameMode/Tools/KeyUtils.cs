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

using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Events;

namespace SampSharp.GameMode.Tools
{
    /// <summary>
    ///     Contains methods to check for keypresses.
    /// </summary>
    public static class KeyUtils
    {
        /// <summary>
        ///     Checks if <see cref="Keys" /> have been pressed.
        /// </summary>
        /// <param name="newKeys">New <see cref="Keys" />.</param>
        /// <param name="oldKeys">Old <see cref="Keys" />.</param>
        /// <param name="keys">The <see cref="Keys" /> to check for.</param>
        /// <returns>Whether the <see cref="Keys" /> have been pressed.</returns>
        public static bool HasPressed(Keys newKeys, Keys oldKeys, Keys keys)
        {
            return newKeys.HasFlag(keys) && !oldKeys.HasFlag(keys);
        }

        /// <summary>
        ///     Checks if <see cref="Keys" /> have been pressed.
        /// </summary>
        /// <param name="e">The <see cref="PlayerKeyStateChangedEventArgs" />.</param>
        /// <param name="keys">The <see cref="Keys" /> to check for.</param>
        /// <returns>Whether the <see cref="Keys" /> have been pressed.</returns>
        public static bool HasPressed(PlayerKeyStateChangedEventArgs e, Keys keys)
        {
            return HasPressed(e.NewKeys, e.OldKeys, keys);
        }

        /// <summary>
        ///     Checks if <see cref="Keys" /> have been released.
        /// </summary>
        /// <param name="newKeys">New <see cref="Keys" />.</param>
        /// <param name="oldKeys">Old <see cref="Keys" />.</param>
        /// <param name="keys">The <see cref="Keys" /> to check for.</param>
        /// <returns>Whether the <see cref="Keys" /> have been released.</returns>
        public static bool HasReleased(Keys newKeys, Keys oldKeys, Keys keys)
        {
            return !newKeys.HasFlag(keys) && oldKeys.HasFlag(keys);
        }

        /// <summary>
        ///     Checks if <see cref="Keys" /> have been released.
        /// </summary>
        /// <param name="e">The <see cref="PlayerKeyStateChangedEventArgs" />.</param>
        /// <param name="keys">The <see cref="Keys" /> to check for.</param>
        /// <returns>Whether the <see cref="Keys" /> have been released.</returns>
        public static bool HasReleased(PlayerKeyStateChangedEventArgs e, Keys keys)
        {
            return HasReleased(e.NewKeys, e.OldKeys, keys);
        }

        /// <summary>
        ///     Checks if <see cref="Keys" /> are being hold.
        /// </summary>
        /// <param name="newKeys">New <see cref="Keys" />.</param>
        /// <param name="oldKeys">Old <see cref="Keys" />.</param>
        /// <param name="keys">The <see cref="Keys" /> to check for.</param>
        /// <returns>Whether the <see cref="Keys" /> are being hold.</returns>
        public static bool IsHolding(Keys newKeys, Keys oldKeys, Keys keys)
        {
            return newKeys.HasFlag(keys);
        }

        /// <summary>
        ///     Checks if <see cref="Keys" /> are being hold.
        /// </summary>
        /// <param name="e">The <see cref="PlayerKeyStateChangedEventArgs" />.</param>
        /// <param name="keys">The <see cref="Keys" /> to check for.</param>
        /// <returns>Whether the <see cref="Keys" /> are being hold.</returns>
        public static bool IsHolding(PlayerKeyStateChangedEventArgs e, Keys keys)
        {
            return IsHolding(e.NewKeys, e.OldKeys, keys);
        }
    }
}