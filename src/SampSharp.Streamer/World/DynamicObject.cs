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

using System;
using System.Linq;
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.SAMP;
using SampSharp.GameMode.World;
using SampSharp.Streamer.Definitions;
using SampSharp.Streamer.Events;
using SampSharp.Streamer.Natives;

namespace SampSharp.Streamer.World
{
    public class DynamicObject : DynamicWorldObject<DynamicObject>, IGameObject
    {
        public DynamicObject(int id)
        {
            Id = id;
        }

        public DynamicObject(int modelid, Vector position, Vector rotation = new Vector(), int worldid = -1,
            int interiorid = -1, GtaPlayer player = null, float streamdistance = 200.0f, float drawdistance = 0.0f)
        {
            Id = StreamerNative.CreateDynamicObject(modelid, position.X, position.Y, position.Z, rotation.X, rotation.Y,
                rotation.Z, worldid, interiorid, player == null ? -1 : player.Id, streamdistance, drawdistance);
        }

        public DynamicObject(int modelid, Vector position, Vector rotation, float streamdistance, int[] worlds = null,
            int[] interiors = null, GtaPlayer[] players = null, float drawdistance = 0.0f)
        {
            Id = StreamerNative.CreateDynamicObjectEx(modelid, position.X, position.Y, position.Z, rotation.X,
                rotation.Y, rotation.Z, drawdistance, streamdistance, worlds, interiors,
                players.Select(p => p.Id).ToArray());
        }

        public override StreamType StreamType
        {
            get { return StreamType.Object; }
        }

        public bool IsMoving
        {
            get { return StreamerNative.IsDynamicObjectMoving(Id); }
        }

        public bool IsValid
        {
            get { return StreamerNative.IsValidDynamicObject(Id); }
        }

        public int ModelId
        {
            get { return GetInteger(StreamerDataType.ModelId); }
            set { SetInteger(StreamerDataType.ModelId, value); }
        }

        public float DrawDistance
        {
            get { return GetFloat(StreamerDataType.DrawDistance); }
            set { SetFloat(StreamerDataType.DrawDistance, value); }
        }

        public Vector Rotation
        {
            get { return StreamerNative.GetDynamicObjectRot(Id); }
            set { StreamerNative.SetDynamicObjectRot(Id, value); }
        }

        public int Move(Vector position, float speed, Vector rotation)
        {
            return StreamerNative.MoveDynamicObject(Id, position, speed, rotation);
        }

        public int Move(Vector position, float speed)
        {
            return StreamerNative.MoveDynamicObject(Id, position, speed);
        }

        public void Stop()
        {
            StreamerNative.StopDynamicObject(Id);
        }

        public void SetMaterial(int materialindex, int modelid, string txdname, string texturename,
            Color materialcolor = new Color())
        {
            StreamerNative.SetDynamicObjectMaterial(Id, materialindex, modelid, txdname, texturename, materialcolor);
        }

        public void SetMaterialText(int materialindex, string text,
            ObjectMaterialSize materialsize = ObjectMaterialSize.X256X128, string fontface = "Arial", int fontsize = 24,
            bool bold = true, Color fontcolor = new Color(), Color backcolor = new Color(),
            ObjectMaterialTextAlign textalignment = ObjectMaterialTextAlign.Center)
        {
            StreamerNative.SetDynamicObjectMaterialText(Id, materialindex, text, materialsize, fontface, fontsize, bold,
                fontcolor, backcolor, textalignment);
        }

        public event EventHandler<DynamicObjectEventArgs> Moved;
        public event EventHandler<PlayerSelectDynamicObjectEventArgs> Selected;
        public event EventHandler<PlayerEditDynamicObjectEventArgs> Edited;
        public event EventHandler<PlayerShootDynamicObjectEventArgs> Shot;

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            StreamerNative.DestroyDynamicObject(Id);
        }

        public void GetMaterial(int materialindex, out int modelid, out string txdname, out string texturename,
            out Color materialcolor)
        {
            StreamerNative.GetDynamicObjectMaterial(Id, materialindex, out modelid, out txdname, out texturename,
                out materialcolor, 64, 64);
        }

        public void GetMaterialText(int materialindex, out string text, out ObjectMaterialSize materialSize,
            out string fontface, out int fontsize, out bool bold, out Color fontcolor, out Color backcolor,
            out ObjectMaterialTextAlign textalignment)
        {
            StreamerNative.GetDynamicObjectMaterialText(Id, materialindex, out text, out materialSize, out fontface,
                out fontsize, out bold, out fontcolor, out backcolor, out textalignment, 1024, 64);
        }

        public virtual void Edit(GtaPlayer player)
        {
            CheckDisposure();

            Native.EditPlayerObject(player.Id, Id);
        }

        public static void Select(GtaPlayer player)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            Native.SelectObject(player.Id);
        }

        public virtual void AttachTo(GtaVehicle vehicle, Vector offset, Vector rotation)
        {
            CheckDisposure();

            if (vehicle == null)
                throw new ArgumentNullException("vehicle");

            StreamerNative.AttachDynamicObjectToVehicle(Id, vehicle.Id, offset, rotation);
        }

        public virtual void AttachCameraToObject(GtaPlayer player)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            CheckDisposure();

            StreamerNative.AttachCameraToDynamicObject(player.Id, Id);
        }

        public virtual void OnMoved(DynamicObjectEventArgs e)
        {
            if (Moved != null)
                Moved(this, e);
        }

        public virtual void OnSelected(PlayerSelectDynamicObjectEventArgs e)
        {
            if (Selected != null)
                Selected(this, e);
        }

        public virtual void OnEdited(PlayerEditDynamicObjectEventArgs e)
        {
            if (Edited != null)
                Edited(this, e);
        }

        public virtual void OnShot(PlayerShootDynamicObjectEventArgs e)
        {
            if (Shot != null)
                Shot(this, e);
        }
    }
}