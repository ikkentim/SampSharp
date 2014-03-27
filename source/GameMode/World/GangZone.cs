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
using GameMode.Definitions;

namespace GameMode.World
{
    public class GangZone : IDisposable
    {
        /// <summary>
        ///     Gets an ID commonly returned by methods to point out that no GangZone matched the requirements.
        /// </summary>
        public const int InvalidId = Misc.InvalidGangZone;

        public GangZone(float minX, float minY, float maxX, float maxY)
        {
            GangZoneId = Native.GangZoneCreate(minX, minY, maxX, maxY);
        }

        public virtual float MinX { get; private set; }

        public virtual float MinY { get; private set; }

        public virtual float MaxX { get; private set; }

        public virtual float MaxY { get; private set; }

        public virtual Color Color { get; set; }

        public virtual int GangZoneId { get; private set; }

        public virtual void Dispose()
        {
            Native.GangZoneDestroy(GangZoneId);
        }

        public virtual void Show()
        {
            Native.GangZoneShowForAll(GangZoneId, Color);
        }

        public virtual void Show(Player player)
        {
            Native.GangZoneShowForPlayer(player.PlayerId, GangZoneId, Color);
        }

        public virtual void Show(Color color)
        {
            Native.GangZoneShowForAll(GangZoneId, color);
        }

        public virtual void Show(Player player, Color color)
        {
            Native.GangZoneShowForPlayer(player.PlayerId, GangZoneId, color);
        }

        public virtual void Hide()
        {
            Native.GangZoneHideForAll(GangZoneId);
        }

        public virtual void Hide(Player player)
        {
            Native.GangZoneHideForPlayer(player.PlayerId, GangZoneId);
        }

        public virtual void Flash()
        {
            Native.GangZoneFlashForAll(GangZoneId, Color);
        }

        public virtual void Flash(Color color)
        {
            Native.GangZoneFlashForAll(GangZoneId, color);
        }

        public virtual void Flash(Player player)
        {
            Native.GangZoneFlashForPlayer(player.PlayerId, GangZoneId, Color);
        }

        public virtual void Flash(Player player, Color color)
        {
            Native.GangZoneFlashForPlayer(player.PlayerId, GangZoneId, color);
        }

        public virtual void StopFlash()
        {
            Native.GangZoneStopFlashForAll(GangZoneId);
        }

        public virtual void StopFlash(Player player)
        {
            Native.GangZoneStopFlashForPlayer(player.PlayerId, GangZoneId);
        }
    }
}