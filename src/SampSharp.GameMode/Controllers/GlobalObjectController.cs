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
using SampSharp.GameMode.Tools;
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Controllers
{
    /// <summary>
    ///     A controller processing all global-object actions.
    /// </summary>
    public class GlobalObjectController : Disposable, IEventListener, ITypeProvider
    {
        /// <summary>
        ///     Registers the events this GlobalObjectController wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        public virtual void RegisterEvents(BaseMode gameMode)
        {
            gameMode.ObjectMoved += (sender, args) =>
            {
                GlobalObject obj = GlobalObject.Find(args.ObjectId);

                if (obj != null)
                    obj.OnMoved(args);
            };
            gameMode.PlayerEditObject += (sender, args) =>
            {
                if (args.ObjectType == ObjectType.GlobalObject)
                {
                    GlobalObject obj = GlobalObject.Find(args.ObjectId);

                    if (obj != null)
                        obj.OnEdited(args);
                }
            };
            gameMode.PlayerSelectObject += (sender, args) =>
            {
                if (args.ObjectType == ObjectType.GlobalObject)
                    if (args.ObjectType == ObjectType.GlobalObject)
                    {
                        GlobalObject obj = GlobalObject.Find(args.ObjectId);

                        if (obj != null)
                            obj.OnSelected(args);
                    }
            };
        }

        /// <summary>
        ///     Registers types this GlobalObjectController requires the system to use.
        /// </summary>
        public virtual void RegisterTypes()
        {
            GlobalObject.Register<GlobalObject>();
        }

        /// <summary>
        ///     Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Whether managed resources should be disposed.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (GlobalObject o in GlobalObject.All)
                {
                    o.Dispose();
                }
            }
        }
    }
}