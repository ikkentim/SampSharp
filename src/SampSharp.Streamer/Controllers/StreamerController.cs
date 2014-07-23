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

using SampSharp.GameMode;
using SampSharp.GameMode.Controllers;
using SampSharp.GameMode.SAMP;
using SampSharp.Streamer.World;

namespace SampSharp.Streamer.Controllers
{
    public class StreamerController : ITypeProvider, IEventListener
    {
        public void RegisterTypes()
        {
            DynamicObject.Register<DynamicObject>();
            DynamicArea.Register<DynamicArea>();
        }

        public void RegisterEvents(BaseMode gameMode)
        {
            gameMode.PlayerSpawned += (sender, args) => {
                                                            args.Player.SendClientMessage(Color.Red, "Spawning at {0}", args.Player.Position);
                                                            Streamer.Update(args.Player, args.Player.Position); };
            gameMode.PlayerConnected += (sender, args) => Streamer.Update(args.Player);
        }
    }
}