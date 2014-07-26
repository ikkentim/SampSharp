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

using SampSharp.GameMode.Controllers;
using SampSharp.Streamer.World;

namespace SampSharp.Streamer.Controllers
{
    public class StreamerController : ITypeProvider
    {
        public void RegisterTypes()
        {
            DynamicArea.Register<DynamicArea>();
            DynamicCheckpoint.Register<DynamicCheckpoint>();
            DynamicMapIcon.Register<DynamicMapIcon>();
            DynamicObject.Register<DynamicObject>();
            DynamicPickup.Register<DynamicPickup>();
            DynamicRaceCheckpoint.Register<DynamicRaceCheckpoint>();
            DynamicTextLabel.Register<DynamicTextLabel>();
        }

        public void RegisterStreamerEvents(Streamer streamer)
        {
            streamer.DynamicObjectMoved += (sender, args) => DynamicObject.FindOrCreate(args.ObjectId).OnMoved(args);
            streamer.PlayerEditDynamicObject += (sender, args) => DynamicObject.FindOrCreate(args.ObjectId).OnEdited(args);
            streamer.PlayerEnterDynamicArea += (sender, args) => DynamicArea.FindOrCreate(args.AreaId).OnEnter(args);
            streamer.PlayerEnterDynamicCheckpoint += (sender, args) => DynamicCheckpoint.FindOrCreate(args.CheckpointId).OnEnter(args);
            streamer.PlayerEnterDynamicRaceCheckpoint += (sender, args) => DynamicRaceCheckpoint.FindOrCreate(args.CheckpointId).OnEnter(args);
            streamer.PlayerLeaveDynamicArea += (sender, args) => DynamicArea.FindOrCreate(args.AreaId).OnLeave(args);
            streamer.PlayerLeaveDynamicCheckpoint += (sender, args) => DynamicCheckpoint.FindOrCreate(args.CheckpointId).OnLeave(args);
            streamer.PlayerLeaveDynamicRaceCheckpoint += (sender, args) => DynamicRaceCheckpoint.FindOrCreate(args.CheckpointId).OnLeave(args);
            streamer.PlayerPickUpDynamicPickup += (sender, args) => DynamicPickup.FindOrCreate(args.PickupId).OnPickedUp(args);
            streamer.PlayerSelectDynamicObject += (sender, args) => DynamicObject.FindOrCreate(args.ObjectId).OnSelected(args);
            streamer.PlayerShootDynamicObject += (sender, args) => DynamicObject.FindOrCreate(args.ObjectId).OnShot(args);

        }
    }
}