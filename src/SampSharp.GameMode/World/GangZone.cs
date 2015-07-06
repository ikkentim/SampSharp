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
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.SAMP;

namespace SampSharp.GameMode.World
{
    /// <summary>
    ///     Represents a gang zone.
    /// </summary>
    public class GangZone : IdentifiedPool<GangZone>, IIdentifiable
    {
        /// <summary>
        ///     Identifier indicating the handle is invalid.
        /// </summary>
        public const int InvalidId = -1;

        /// <summary>
        ///     Maximum number of gang zones which can exist.
        /// </summary>
        public const int Max = 1024;

        /// <summary>
        ///     Initializes a new instance of the <see cref="GangZone" /> class.
        /// </summary>
        /// <param name="minX">The minimum x.</param>
        /// <param name="minY">The minimum y.</param>
        /// <param name="maxX">The maximum x.</param>
        /// <param name="maxY">The maximum y.</param>
        public GangZone(float minX, float minY, float maxX, float maxY)
        {
            Id = GangZoneCreate(minX, minY, maxX, maxY);

            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
        }

        /// <summary>
        ///     Gets the minimum x value for this <see cref="GangZone" />.
        /// </summary>
        public virtual float MinX { get; private set; }

        /// <summary>
        ///     Gets the minimum y value for this <see cref="GangZone" />.
        /// </summary>
        public virtual float MinY { get; private set; }

        /// <summary>
        ///     Gets the maximum x value for this <see cref="GangZone" />.
        /// </summary>
        public virtual float MaxX { get; private set; }

        /// <summary>
        ///     Gets the maximum y value for this <see cref="GangZone" />.
        /// </summary>
        public virtual float MaxY { get; private set; }

        /// <summary>
        ///     Gets or sets the color of this <see cref="GangZone" />.
        /// </summary>
        public virtual Color Color { get; set; }

        /// <summary>
        ///     Gets the Identity of this <see cref="GangZone" />.
        /// </summary>
        public virtual int Id { get; private set; }

        #region Natives

        private delegate int GangZoneCreateImpl(float minx, float miny, float maxx, float maxy);

        private delegate bool GangZoneDestroyImpl(int zone);

        private delegate bool GangZoneFlashForAllImpl(int zone, int flashcolor);

        private delegate bool GangZoneFlashForPlayerImpl(int playerid, int zone, int flashcolor);

        private delegate bool GangZoneHideForAllImpl(int zone);

        private delegate bool GangZoneHideForPlayerImpl(int playerid, int zone);

        private delegate bool GangZoneShowForAllImpl(int zone, int color);

        private delegate bool GangZoneShowForPlayerImpl(int playerid, int zone, int color);

        private delegate bool GangZoneStopFlashForAllImpl(int zone);

        private delegate bool GangZoneStopFlashForPlayerImpl(int playerid, int zone);

        [Native("GangZoneCreate")]
        private static readonly GangZoneCreateImpl GangZoneCreate = null;
        [Native("GangZoneDestroy")]
        private static readonly GangZoneDestroyImpl GangZoneDestroy = null;
        [Native("GangZoneShowForPlayer")]
        private static readonly GangZoneShowForPlayerImpl GangZoneShowForPlayer = null;
        [Native("GangZoneShowForAll")]
        private static readonly GangZoneShowForAllImpl GangZoneShowForAll = null;
        [Native("GangZoneHideForPlayer")]
        private static readonly GangZoneHideForPlayerImpl GangZoneHideForPlayer = null;
        [Native("GangZoneHideForAll")]
        private static readonly GangZoneHideForAllImpl GangZoneHideForAll = null;

        [Native("GangZoneFlashForPlayer")]
        private static readonly GangZoneFlashForPlayerImpl GangZoneFlashForPlayer =
            null;

        [Native("GangZoneFlashForAll")]
        private static readonly GangZoneFlashForAllImpl GangZoneFlashForAll = null;

        [Native("GangZoneStopFlashForPlayer")]
        private static readonly GangZoneStopFlashForPlayerImpl
            GangZoneStopFlashForPlayer = null;

        [Native("GangZoneStopFlashForAll")]
        private static readonly GangZoneStopFlashForAllImpl GangZoneStopFlashForAll =
            null;

        #endregion

        /// <summary>
        ///     Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Whether managed resources should be disposed.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            GangZoneDestroy(Id);
        }

        /// <summary>
        ///     Shows this <see cref="GangZone" />.
        /// </summary>
        public virtual void Show()
        {
            AssertNotDisposed();

            GangZoneShowForAll(Id, Color);
        }

        /// <summary>
        ///     Shows this <see cref="GangZone" /> to the specified <paramref name="player" />.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <exception cref="System.ArgumentNullException">player</exception>
        public virtual void Show(GtaPlayer player)
        {
            if (player == null) throw new ArgumentNullException("player");
            AssertNotDisposed();

            GangZoneShowForPlayer(player.Id, Id, Color);
        }

        /// <summary>
        ///     Hides this <see cref="GangZone" />.
        /// </summary>
        public virtual void Hide()
        {
            AssertNotDisposed();

            GangZoneHideForAll(Id);
        }

        /// <summary>
        ///     Hides this <see cref="GangZone" /> for the specified <paramref name="player" />.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <exception cref="System.ArgumentNullException">player</exception>
        public virtual void Hide(GtaPlayer player)
        {
            if (player == null) throw new ArgumentNullException("player");
            AssertNotDisposed();

            GangZoneHideForPlayer(player.Id, Id);
        }

        /// <summary>
        ///     Flashes this <see cref="GangZone" />.
        /// </summary>
        /// <param name="color">The color.</param>
        public virtual void Flash(Color color)
        {
            AssertNotDisposed();

            GangZoneFlashForAll(Id, color);
        }

        /// <summary>
        ///     Flashes this <see cref="GangZone" /> for the specified <paramref name="player" />.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <exception cref="System.ArgumentNullException">player</exception>
        public virtual void Flash(GtaPlayer player)
        {
            if (player == null) throw new ArgumentNullException("player");
            Flash(player, new Color());
        }

        /// <summary>
        ///     Flashes this <see cref="GangZone" /> for the specified <paramref name="player" />.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="color">The color.</param>
        /// <exception cref="System.ArgumentNullException">player</exception>
        public virtual void Flash(GtaPlayer player, Color color)
        {
            if (player == null) throw new ArgumentNullException("player");
            AssertNotDisposed();

            GangZoneFlashForPlayer(player.Id, Id, color);
        }

        /// <summary>
        ///     Stops this <see cref="GangZone" /> from flash.
        /// </summary>
        public virtual void StopFlash()
        {
            AssertNotDisposed();

            GangZoneStopFlashForAll(Id);
        }

        /// <summary>
        ///     Stops this <see cref="GangZone" /> from flash for the specified player.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <exception cref="System.ArgumentNullException">player</exception>
        public virtual void StopFlash(GtaPlayer player)
        {
            AssertNotDisposed();

            if (player == null)
                throw new ArgumentNullException("player");

            GangZoneStopFlashForPlayer(player.Id, Id);
        }
    }
}