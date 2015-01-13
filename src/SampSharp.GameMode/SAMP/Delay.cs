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
using SampSharp.GameMode.Natives;

namespace SampSharp.GameMode.SAMP
{
    /// <summary>
    ///     Contains methods for delaying an Action.
    /// </summary>
    public class Delay
    {
        private Delay(Action action)
        {
            Action = action;
        }

        /// <summary>
        ///     Gets the action performed after this Delay.
        /// </summary>
        public Action Action { get; private set; }

        /// <summary>
        ///     Runs an action after a given delay.
        /// </summary>
        /// <param name="delay">Delay in miliseconds.</param>
        /// <param name="action">Action to perform after the given <paramref name="delay" />.</param>
        public static void Run(int delay, Action action)
        {
            var instance = new Delay(action);

            int id = Native.SetTimer(delay, false, instance);
        }
    }
}