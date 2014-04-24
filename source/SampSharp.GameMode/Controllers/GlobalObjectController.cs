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
using SampSharp.GameMode.World;

namespace SampSharp.GameMode.Controllers
{
    /// <summary>
    ///     A controller processing all global-object actions.
    /// </summary>
    public class GlobalObjectController : IEventListener, ITypeProvider, IController
    {
        /// <summary>
        ///     Registers the events this GlobalObjectController wants to listen to.
        /// </summary>
        /// <param name="gameMode">The running GameMode.</param>
        public virtual void RegisterEvents(BaseMode gameMode)
        {
            gameMode.ObjectMoved += (sender, args) => GlobalObject.Find(args.ObjectId).OnMoved(args);
            gameMode.PlayerEditObject += (sender, args) =>
            {
                if (!args.PlayerObject)
                    GlobalObject.Find(args.ObjectId).OnEdited(args);
            };
            gameMode.PlayerSelectObject += (sender, args) =>
            {
                if (args.ObjectType == ObjectType.GlobalObject)
                    GlobalObject.Find(args.ObjectId).OnSelected(args);
            };
        }

        /// <summary>
        ///     Registers types this GlobalObjectController requires the system to use.
        /// </summary>
        public virtual void RegisterTypes()
        {
            GlobalObject.Register<GlobalObject>();
        }
    }
}