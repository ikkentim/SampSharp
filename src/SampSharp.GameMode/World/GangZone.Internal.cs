using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampSharp.GameMode.Natives;

namespace SampSharp.GameMode.World
{
    public partial class GangZone
    {
        private static class Internal
        {
            public delegate int GangZoneCreateImpl(float minx, float miny, float maxx, float maxy);

            public delegate bool GangZoneDestroyImpl(int zone);

            public delegate bool GangZoneFlashForAllImpl(int zone, int flashcolor);

            public delegate bool GangZoneFlashForPlayerImpl(int playerid, int zone, int flashcolor);

            public delegate bool GangZoneHideForAllImpl(int zone);

            public delegate bool GangZoneHideForPlayerImpl(int playerid, int zone);

            public delegate bool GangZoneShowForAllImpl(int zone, int color);

            public delegate bool GangZoneShowForPlayerImpl(int playerid, int zone, int color);

            public delegate bool GangZoneStopFlashForAllImpl(int zone);

            public delegate bool GangZoneStopFlashForPlayerImpl(int playerid, int zone);

            [Native("GangZoneCreate")]
            public static readonly GangZoneCreateImpl GangZoneCreate = null;
            [Native("GangZoneDestroy")]
            public static readonly GangZoneDestroyImpl GangZoneDestroy = null;
            [Native("GangZoneShowForPlayer")]
            public static readonly GangZoneShowForPlayerImpl GangZoneShowForPlayer = null;
            [Native("GangZoneShowForAll")]
            public static readonly GangZoneShowForAllImpl GangZoneShowForAll = null;
            [Native("GangZoneHideForPlayer")]
            public static readonly GangZoneHideForPlayerImpl GangZoneHideForPlayer = null;
            [Native("GangZoneHideForAll")]
            public static readonly GangZoneHideForAllImpl GangZoneHideForAll = null;

            [Native("GangZoneFlashForPlayer")]
            public static readonly GangZoneFlashForPlayerImpl GangZoneFlashForPlayer =
                null;

            [Native("GangZoneFlashForAll")]
            public static readonly GangZoneFlashForAllImpl GangZoneFlashForAll = null;

            [Native("GangZoneStopFlashForPlayer")]
            public static readonly GangZoneStopFlashForPlayerImpl
                GangZoneStopFlashForPlayer = null;

            [Native("GangZoneStopFlashForAll")]
            public static readonly GangZoneStopFlashForAllImpl GangZoneStopFlashForAll =
                null;
        }
    }
}
