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

using SampSharp.GameMode.Tools;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Controllers
{
    public class PickupController : Disposable, IEventListener, ITypeProvider
    {
        /// <summary>
        ///     Registers the events this TextDrawController wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        public void RegisterEvents(BaseMode gameMode)
        {
            gameMode.PlayerPickUpPickup += (sender, args) =>
            {
                Pickup obj = Pickup.Find(args.PickupId);

                if (obj != null)
                {
                    obj.OnPickup(args);
                }
            };
        }

        /// <summary>
        ///     Registers types this PickupController requires the system to use.
        /// </summary>
        public void RegisterTypes()
        {
            Pickup.Register<Pickup>();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var pickup in Pickup.All)
                {
                    pickup.Dispose();
                }
            }
        }
    }
}