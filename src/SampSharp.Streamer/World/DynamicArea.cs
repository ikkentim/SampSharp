using System;
using System.Collections.Generic;
using System.Linq;
using SampSharp.GameMode.Pools;
using SampSharp.GameMode.World;
using SampSharp.Streamer.Definitions;
using SampSharp.Streamer.Natives;

namespace SampSharp.Streamer.World
{
    public class DynamicArea : IdentifiedPool<DynamicArea>, IIdentifyable
    {
        public DynamicArea(int id)
        {
            Id = id;

        }

        #region Factories

        public static DynamicArea CreateCircle(float x, float y, float size, int worldid = -1, int interiorid = -1,
            Player player = null)
        {
            return
                FindOrCreate(StreamerNative.CreateDynamicCircle(x, y, size, worldid, interiorid,
                    player == null ? -1 : player.Id));
        }

        public static DynamicArea CreateCircleEx(float x, float y, float size, int[] worlds = null, int[] interiors = null,
            Player[] players = null)
        {
            return
                FindOrCreate(StreamerNative.CreateDynamicCircleEx(x, y, size, worlds, interiors,
                    players == null ? null : players.Select(p => p.Id).ToArray()));
        }

        public static DynamicArea CreateCube(float minx, float miny, float minz, float maxx, float maxy, float maxz,
            int worldid = -1, int interiorid = -1, Player player = null)
        {
            return
                FindOrCreate(StreamerNative.CreateDynamicCube(minx, miny, minz, maxx, maxy, maxz, worldid, interiorid,
                    player == null ? -1 : player.Id));
        }


        public static DynamicArea CreateCube(Vector min, Vector max, int worldid = -1, int interiorid = -1,
            Player player = null)
        {
            return
                FindOrCreate(StreamerNative.CreateDynamicCube(min.X, min.Y, min.Z, max.X, max.Y, max.Z, worldid,
                    interiorid, player == null ? -1 : player.Id));
        }

        public static DynamicArea CreateCubeEx(float minx, float miny, float minz, float maxx, float maxy, float maxz,
            int[] worlds = null, int[] interiors = null, Player[] players = null)
        {
            return
                FindOrCreate(StreamerNative.CreateDynamicCubeEx(minx, miny, minz, maxx, maxy, maxz, worlds,
                    interiors, players == null ? null : players.Select(p => p.Id).ToArray()));
        }

        public static DynamicArea CreateCubeEx(Vector min, Vector max, int[] worlds = null, int[] interiors = null,
            Player[] players = null)
        {
            return
                FindOrCreate(StreamerNative.CreateDynamicCubeEx(min.X, min.Y, min.Z, max.X, max.Y, max.Z, worlds,
                    interiors, players == null ? null : players.Select(p => p.Id).ToArray()));
        }

        public static DynamicArea CreatePolygon(float[] points, float minz = float.NegativeInfinity,
            float maxz = float.PositiveInfinity, int worlid = -1, int interiorid = -1, Player player = null)
        {
            return
                FindOrCreate(StreamerNative.CreateDynamicPolygon(points, minz, maxz, -1, worlid, interiorid,
                    player == null ? -1 : player.Id));
        }

        public static DynamicArea CreatePolygon(Vector[] points, float minz, float maxz, int worlid = -1,
            int interiorid = -1, Player player = null)
        {
            return
                FindOrCreate(StreamerNative.CreateDynamicPolygon(points.SelectMany(p => new[] {p.X, p.Y}).ToArray(),
                    minz, maxz, -1, worlid, interiorid, player == null ? -1 : player.Id));
        }

        public static DynamicArea CreatePolygon(Vector[] points, int worlid = -1, int interiorid = -1,
            Player player = null)
        {
            return
                FindOrCreate(StreamerNative.CreateDynamicPolygon(points.SelectMany(p => new[] { p.X, p.Y }).ToArray(),
                    points.Min(p => p.Z), points.Max(p => p.Z), -1, worlid, interiorid, player == null ? -1 : player.Id));
        }

        public static DynamicArea CreatePolygonEx(float[] points, float minz = float.NegativeInfinity,
            float maxz = float.PositiveInfinity, int[] worlds = null, int[] interiors = null, Player[] players = null)
        {
            return
                FindOrCreate(StreamerNative.CreateDynamicPolygonEx(points, minz, maxz, -1, worlds, interiors,
                    players == null ? null : players.Select(p => p.Id).ToArray()));
        }

        public static DynamicArea CreatePolygonEx(Vector[] points, float minz = float.NegativeInfinity,
            float maxz = float.PositiveInfinity, int[] worlds = null, int[] interiors = null, Player[] players = null)
        {
            return
                FindOrCreate(StreamerNative.CreateDynamicPolygonEx(points.SelectMany(p => new[] { p.X, p.Y }).ToArray(),
                    minz, maxz, -1, worlds, interiors, players == null ? null : players.Select(p => p.Id).ToArray()));
        }

        public static DynamicArea CreatePolygonEx(Vector[] points, int[] worlds = null, int[] interiors = null,
            Player[] players = null)
        {
            return
                FindOrCreate(StreamerNative.CreateDynamicPolygonEx(points.SelectMany(p => new[] { p.X, p.Y }).ToArray(),
                    points.Min(p => p.Z), points.Max(p => p.Z), -1, worlds, interiors,
                    players == null ? null : players.Select(p => p.Id).ToArray()));
        }

        public static DynamicArea CreateRectangle(float minx, float miny, float maxx, float maxy, int worldid = -1,
            int interiorid = -1, Player player = null)
        {
            return FindOrCreate(StreamerNative.CreateDynamicRectangle(minx, miny, maxx, maxy, worldid, interiorid,
                player == null ? -1 : player.Id));
        }

        public static DynamicArea CreateRectangleEx(float minx, float miny, float maxx, float maxy, int[] worlds = null,
            int[] interiors = null, Player[] players = null)
        {
            return
                FindOrCreate(StreamerNative.CreateDynamicRectangleEx(minx, miny, maxx, maxy, worlds, interiors,
                    players == null ? null : players.Select(p => p.Id).ToArray()));
        }

        public static DynamicArea CreateSphere(Vector pos, float size, int worldid = -1, int interiorid = -1,
            Player player = null)
        {
            return
                FindOrCreate(StreamerNative.CreateDynamicSphere(pos.X, pos.Y, pos.Z, size, worldid, interiorid,
                    player == null ? -1 : player.Id));
        }

        public static DynamicArea CreateSphereEx(Vector pos, float size, int[] worlds = null, int[] interiors = null,
            Player[] players = null)
        {
            return
                FindOrCreate(StreamerNative.CreateDynamicSphereEx(pos.X, pos.Y, pos.Z, size, worlds, interiors,
                    players == null ? null : players.Select(p => p.Id).ToArray()));
        }

        #endregion

        public int Id { get; private set; }

        public bool IsValid
        {
            get { return StreamerNative.IsValidDynamicArea(Id); }
        }

        public void AttachTo(IGameObject obj)
        {
            CheckDisposure();

            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (!(obj is IIdentifyable))
            {
                throw new ArgumentException("obj must be IIdentifyable");
            }

            var playerid = Player.InvalidId;
            var objectid = (obj as IIdentifyable).Id;
            var type = StreamerObjectType.Global;

            if (obj is IOwnable)
            {
                playerid = (obj as IOwnable).Player.Id;
            }
            if (obj is PlayerObject)
            {
                type = StreamerObjectType.Player;
            }
            if (obj is DynamicObject)
            {
                type = StreamerObjectType.Dynamic;
            }

            StreamerNative.AttachDynamicAreaToObject(Id, objectid, type, playerid);
        }

        public void AttachTo(Player player)
        {
            CheckDisposure();

            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            StreamerNative.AttachDynamicAreaToPlayer(Id, player.Id);
        }

        public void AttachTo(Vehicle vehicle)
        {
            CheckDisposure();

            if (vehicle == null)
            {
                throw new ArgumentNullException("vehicle");
            }

            StreamerNative.AttachDynamicAreaToVehicle(Id, vehicle.Id);
        }

        public bool IsInArea(Player player, bool recheck = false)
        {
            CheckDisposure();

            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            return StreamerNative.IsPlayerInDynamicArea(player.Id, Id, recheck);
        }

        public bool IsInArea(IWorldObject obj)
        {
            CheckDisposure();

            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            return StreamerNative.IsPointInDynamicArea(Id, obj.Position);
        }

        public bool IsInArea(Vector point)
        {
            CheckDisposure();

            return StreamerNative.IsPointInDynamicArea(Id, point);
        }

        public bool IsAnyPlayerInArea(bool recheck = false)
        {
            CheckDisposure();

            return StreamerNative.IsAnyPlayerInDynamicArea(Id, recheck);
        }

        public static bool IsPlayerInAnyArea(Player player, bool recheck = false)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            return StreamerNative.IsPlayerInAnyDynamicArea(player.Id, recheck);
        }

        public static bool IsAnyPlayerInAnyArea(bool recheck = false)
        {
            return StreamerNative.IsAnyPlayerInAnyDynamicArea(recheck);
        }

        public static int GetAreaCountForPlayer(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            return StreamerNative.GetPlayerNumberDynamicAreas(player.Id);
        }

        public static IEnumerable<DynamicArea> GetAreasForPlayer(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            int[] areas;
            StreamerNative.GetPlayerDynamicAreas(player.Id, out areas, GetAreaCountForPlayer(player));

            return areas == null ? null : areas.Select(FindOrCreate);
        }

        public void ToggleAreaForPlayer(Player player, bool toggle)
        {
            CheckDisposure();

            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            StreamerNative.TogglePlayerDynamicArea(player.Id, Id, toggle);
        }

        public static void ToggleAllAreasForPlayer(Player player, bool toggle)
        {
            if (player == null)
            {
                throw new ArgumentNullException("player");
            }

            StreamerNative.TogglePlayerAllDynamicAreas(player.Id, toggle);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            StreamerNative.DestroyDynamicArea(Id);
        }
    }
}
