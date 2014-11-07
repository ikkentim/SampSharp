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
using SampSharp.GameMode.Definitions;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.SAMP;

namespace SampSharp.GameMode.World
{
    public class GangZone : IdentifiedPool<GangZone>, IIdentifiable
    {
        /// <summary>
        ///     Gets an ID commonly returned by methods to point out that no GangZone matched the requirements.
        /// </summary>
        public const int InvalidId = Misc.InvalidGangZone;

        public GangZone(float minX, float minY, float maxX, float maxY)
        {
            Id = Native.GangZoneCreate(minX, minY, maxX, maxY);

            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
        }

        public virtual float MinX { get; private set; }

        public virtual float MinY { get; private set; }

        public virtual float MaxX { get; private set; }

        public virtual float MaxY { get; private set; }

        public virtual Color Color { get; set; }

        public virtual int Id { get; private set; }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Native.GangZoneDestroy(Id);
        }

        public virtual void Show()
        {
            CheckDisposure();

            Native.GangZoneShowForAll(Id, Color);
        }

        public virtual void Show(GtaPlayer player)
        {
            CheckDisposure();

            Show(player, Color);
        }

        public virtual void Show(Color color)
        {
            CheckDisposure();

            Native.GangZoneShowForAll(Id, color);
        }

        public virtual void Show(GtaPlayer player, Color color)
        {
            CheckDisposure();

            if (player == null)
                throw new ArgumentNullException("player");

            Native.GangZoneShowForPlayer(player.Id, Id, color);
        }

        public virtual void Hide()
        {
            CheckDisposure();

            Native.GangZoneHideForAll(Id);
        }

        public virtual void Hide(GtaPlayer player)
        {
            CheckDisposure();

            Native.GangZoneHideForPlayer(player.Id, Id);
        }

        public virtual void Flash()
        {
            CheckDisposure();

            Native.GangZoneFlashForAll(Id, Color);
        }

        public virtual void Flash(Color color)
        {
            CheckDisposure();

            Native.GangZoneFlashForAll(Id, color);
        }

        public virtual void Flash(GtaPlayer player)
        {
            CheckDisposure();

            Flash(player, Color);
        }

        public virtual void Flash(GtaPlayer player, Color color)
        {
            CheckDisposure();

            if (player == null)
                throw new ArgumentNullException("player");

            Native.GangZoneFlashForPlayer(player.Id, Id, color);
        }

        public virtual void StopFlash()
        {
            CheckDisposure();

            Native.GangZoneStopFlashForAll(Id);
        }

        public virtual void StopFlash(GtaPlayer player)
        {
            CheckDisposure();

            if (player == null)
                throw new ArgumentNullException("player");

            Native.GangZoneStopFlashForPlayer(player.Id, Id);
        }
    }
}