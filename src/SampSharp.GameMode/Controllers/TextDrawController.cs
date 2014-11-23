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

using SampSharp.GameMode.Display;
using SampSharp.GameMode.Tools;

namespace SampSharp.GameMode.Controllers
{
    /// <summary>
    ///     A controller processing all textdraw actions.
    /// </summary>
    public class TextDrawController : Disposable, IEventListener, ITypeProvider
    {
        /// <summary>
        ///     Registers the events this TextDrawController wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        public void RegisterEvents(BaseMode gameMode)
        {
            gameMode.PlayerClickTextDraw += (sender, args) =>
            {
                TextDraw obj = TextDraw.Find(args.TextDrawId);

                if (obj != null)
                    obj.OnClick(args);
            };
        }

        /// <summary>
        ///     Registers types this TextDrawController requires the system to use.
        /// </summary>
        public void RegisterTypes()
        {
            TextDraw.Register<TextDraw>();
        }

        /// <summary>
        ///     Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Whether managed resources should be disposed.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (PlayerTextDraw td in PlayerTextDraw.All)
                {
                    td.Dispose();
                }
            }
        }
    }
}