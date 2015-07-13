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

using SampSharp.GameMode.API;

namespace SampSharp.GameMode.Tools
{
    public static partial class MapAndreas
    {
        private static class Internal
        {
            public delegate bool FindAverageZImpl(float x, float y, out float z);

            public delegate bool FindZImpl(float x, float y, out float z);

            public delegate bool InitImpl(int mode, string filename, int length);

            public delegate bool SaveCurrentHMapImpl(string filename);

            public delegate bool SetZImpl(float x, float y, float z);

            public delegate bool UnloadImpl();

            [Native("MapAndreas_Init")] public static readonly InitImpl Init = null;
            [Native("MapAndreas_Unload")] public static readonly UnloadImpl Unload = null;
            [Native("MapAndreas_FindZ_For2DCoord")] public static readonly FindZImpl FindZ = null;
            [Native("MapAndreas_FindAverageZ")] public static readonly FindAverageZImpl FindAverageZ = null;
            [Native("MapAndreas_SaveCurrentHMap")] public static readonly SetZImpl SetZ = null;

            public static readonly SaveCurrentHMapImpl SaveCurrentHMap = null;
        }
    }
}