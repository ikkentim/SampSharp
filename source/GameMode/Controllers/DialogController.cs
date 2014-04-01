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

using GameMode.Display;
using GameMode.World;

namespace GameMode.Controllers
{
    /// <summary>
    ///     A controller processing all dialog actions.
    /// </summary>
    public class DialogController : IController
    {
        /// <summary>
        ///     Registers the events this DialogController wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        public void RegisterEvents(BaseMode gameMode)
        {
            gameMode.DialogResponse += (sender, args) =>
            {
                Dialog dialog = Dialog.GetOpenDialog(args.Player);

                if (dialog != null)
                    dialog.OnResponse(args);
            };

            gameMode.PlayerDisconnected += (sender, args) => Dialog.Hide(args.Player);
        }
    }
}