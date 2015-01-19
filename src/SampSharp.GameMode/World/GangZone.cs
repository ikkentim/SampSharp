// SampSharp
// Copyright 2015 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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

        /// <summary>
        ///     Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Whether managed resources should be disposed.</param>
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