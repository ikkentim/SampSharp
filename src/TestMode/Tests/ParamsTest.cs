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
using SampSharp.GameMode;
using SampSharp.GameMode.Natives;
using SampSharp.GameMode.World;

namespace TestMode.Tests
{
    public class ParamsTest : ITest
    {
        #region Implementation of ITest

        public void Start(GameMode gameMode)
        {
            var test = GtaVehicle.Create(400, Vector3.Zero, 0, 0, 0);

            int a, b, c, d, e, f, g;
            Native.GetVehicleParamsEx(test.Id, out a, out b, out c, out d, out e, out f, out g);
            Console.WriteLine("The default parameters of GetVehicleParamsEx are: ({0}, {1}, {2}, {3}, {4}, {5}, {6})", a,
                b, c, d, e, f, g);

            Native.GetVehicleParamsCarDoors(test.Id, out a, out b, out c, out d);
            Console.WriteLine("The default parameters of GetVehicleParamsCarDoors are: ({0}, {1}, {2}, {3})", a, b, c, d);

            Native.GetVehicleParamsCarWindows(test.Id, out a, out b, out c, out d);
            Console.WriteLine("The default parameters of GetVehicleParamsCarWindows are: ({0}, {1}, {2}, {3})", a, b, c,
                d);
        }

        #endregion
    }
}