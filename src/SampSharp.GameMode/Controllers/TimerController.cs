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

using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.Tools;

namespace SampSharp.GameMode.Controllers
{
    /// <summary>
    ///     A controller processing all timer actions.
    /// </summary>
    public class TimerController : Disposable, IEventListener, ITypeProvider
    {
        /// <summary>
        ///     Registers the events this TimerController wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        public virtual void RegisterEvents(BaseMode gameMode)
        {
            gameMode.TimerTick += (sender, args) =>
            {
                var timer = sender as Timer;

                if (timer != null)
                    timer.OnTick(args);
            };
        }

        /// <summary>
        ///     Registers types this TimerController requires the system to use.
        /// </summary>
        public virtual void RegisterTypes()
        {
            Timer.Register<Timer>();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (Timer t in Timer.All)
                {
                    t.Dispose();
                }
            }
        }
    }
}