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
using SampSharp.GameMode.Tools;

namespace TestMode.Tests
{
    internal class MapAndreasTest : ITest
    {
        #region Implementation of ITest

        public void Start(GameMode gameMode)
        {
            Console.WriteLine("Load hmap");
            MapAndreas.Load(MapAndreasMode.Full);

            float x = 1700;
            float y = -1700;
            Console.WriteLine("Pos at 1700 -1700: {0}", MapAndreas.Find(x, y));
            Console.WriteLine("Save hmap as test.hmap");
            MapAndreas.Save("scriptfiles/test.hmap");

            Console.WriteLine("Set -2500 -25-- to 100");
            MapAndreas.SetZ(-2500, -2500, 100);

            Console.WriteLine("Save hmap as test.hmap");
            MapAndreas.Save("scriptfiles/test2.hmap");

            Console.WriteLine("Unload hmap");
            MapAndreas.Unload();
        }

        #endregion
    }
}