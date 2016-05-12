// SampSharp
// Copyright 2016 Tim Potze
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

using SampSharp.GameMode.API;

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

            [Native("GangZoneCreate")] public static readonly GangZoneCreateImpl GangZoneCreate = null;
            [Native("GangZoneDestroy")] public static readonly GangZoneDestroyImpl GangZoneDestroy = null;

            [Native("GangZoneShowForPlayer")] public static readonly GangZoneShowForPlayerImpl GangZoneShowForPlayer =
                null;

            [Native("GangZoneShowForAll")] public static readonly GangZoneShowForAllImpl GangZoneShowForAll = null;

            [Native("GangZoneHideForPlayer")] public static readonly GangZoneHideForPlayerImpl GangZoneHideForPlayer =
                null;

            [Native("GangZoneHideForAll")] public static readonly GangZoneHideForAllImpl GangZoneHideForAll = null;

            [Native("GangZoneFlashForPlayer")] public static readonly GangZoneFlashForPlayerImpl GangZoneFlashForPlayer
                =
                null;

            [Native("GangZoneFlashForAll")] public static readonly GangZoneFlashForAllImpl GangZoneFlashForAll = null;

            [Native("GangZoneStopFlashForPlayer")] public static readonly GangZoneStopFlashForPlayerImpl
                GangZoneStopFlashForPlayer = null;

            [Native("GangZoneStopFlashForAll")] public static readonly GangZoneStopFlashForAllImpl
                GangZoneStopFlashForAll =
                    null;
        }
    }
}