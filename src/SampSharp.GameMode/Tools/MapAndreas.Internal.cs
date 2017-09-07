// SampSharp
// Copyright 2017 Tim Potze
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
using SampSharp.Core.Natives.NativeObjects;

namespace SampSharp.GameMode.Tools
{
    internal static partial class MapAndreas
    {
        public class MapAndreasInternal : NativeObjectSingleton<MapAndreasInternal>
        {
            [NativeMethod(Function = "MapAndreas_Init")]
            public virtual bool Init(int mode, string filename, int length)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod(Function = "MapAndreas_Unload")]
            public virtual bool Unload()
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod(Function = "MapAndreas_FindZ_For2DCoord")]
            public virtual bool FindZ(float x, float y, out float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod(Function = "MapAndreas_FindAverageZ")]
            public virtual bool FindAverageZ(float x, float y, out float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod(Function = "MapAndreas_SetZ_For2DCoord")]
            public virtual bool SetZ(float x, float y, float z)
            {
                throw new NativeNotImplementedException();
            }

            [NativeMethod(Function = "MapAndreas_SaveCurrentHMap")]
            public virtual bool SaveCurrentHMap(string filename)
            {
                throw new NativeNotImplementedException();
            }
        }
    }
}